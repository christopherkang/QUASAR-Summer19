namespace ImportOptimizedFermions
{
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Chemistry.JordanWigner;  
	open Microsoft.Quantum.Simulation;	
    open Microsoft.Quantum.Characterization;
    open Microsoft.Quantum.Convert;
    open Microsoft.Quantum.Math;
    open Microsoft.Quantum.Diagnostics;

    operation EstimateEnergyLevel(data : CompleteHamiltonian, nBitsPrecision : Int) : (Double, Double) {
        let (constants, statePrepData, swapTerms, interactionTerms) = data!;
        let (nSpinOrbitals, energyOffset, trotterStep, trotterOrder) = constants!;
        
        // Get crucial constants
        let rescaleFactor = 1.0 / trotterStep;

        // Get combined oracle
        let oracle = ApplyHamiltonianTerm(swapTerms, interactionTerms, constants, _);

        let stateData = (statePrepData!);

        // Prep important unitaries
        let statePrep = PrepareTrialState(stateData, _);
        let phaseEstAlgorithm = RobustPhaseEstimation(nBitsPrecision, _, _);

        // Estimate energy
        let estPhase = EstimateEnergy(nSpinOrbitals, statePrep, oracle, phaseEstAlgorithm);
        let energyLevel = estPhase * rescaleFactor + energyOffset;

        return (estPhase, energyLevel);
    }

    // Applies all of the SWAPs and Interaction Rounds, then brings the qubits back 
    // to their original orientation (canonical form)
    operation ApplyHamiltonianTerm(swapList : SWAPRound[], interactionList : JordanWignerEncodingData[], constants : HamiltonianConstants, register : Qubit[]) : Unit is Adj+Ctl {
        let iterationLength = Length(interactionList);

        let (nSpinOrbitals, energyOffset, trotterStep, trotterOrder) = constants!;

        EqualityFactI(Length(swapList), Length(interactionList), "The rounds of swaps and interactions do not match");

        // We'll apply all of the SWAPs and Interaction, alternating
        for (round in 0..iterationLength - 1) {
            Message($"{round}");
            ApplySWAPRound(swapList[round], register);
            Message($"interaction: {round}");
            ApplyInteractionRound(interactionList[round], trotterStep, trotterOrder, register);
        }
    }

    // Applies an entire round of SWAPS onto a register
    operation ApplySWAPRound(data : SWAPRound, register : Qubit[]) : Unit is Adj+Ctl {
        let series = data!;
        let numberOfSWAPSeries = Length(series);
        
        for (index in 0..numberOfSWAPSeries - 1) {
            ApplySWAPSeries(series[index], register);
            Message($"SWAP: {index}");
        }
    }

    // Applies the SWAPSeries onto a register
    operation ApplySWAPSeries(data : SWAPSeries, register : Qubit[]) : Unit is Adj+Ctl {
        let (qubitsLeft, qubitsRight) = data!;
        let numberOfSWAPs = Length(qubitsLeft);

        for (index in 0..numberOfSWAPs - 1) {
            FermionicSWAP(register[qubitsLeft[index]], register[qubitsRight[index]]);
        }
    }

    // Applies a single round of the interactions
    // Input: JWED, trotter step size, order, and the qubit register
    // Output: Unit
    operation ApplyInteractionRound(data : JordanWignerEncodingData, trotterStepSize : Double, trotterOrder : Int, register : Qubit[]) : Unit is Adj+Ctl {
        let (nSpinOrbitals, fermionTermData, statePrepData, energyOffset) = data!;

        let (h1, h2, h3, h4) = data!;

        Message($"{h1}");
        Message($"{h2}");
        Message($"{h3}");
        Message($"{h4}");

        let (nQubits, (rescaleFactor, oracle)) = TrotterStepOracle(data, trotterStepSize, trotterOrder);

        oracle(register);
    }

    // Produce a Fermionic SWAP enhanced Hamiltonian energy level estimate
    // Input: PackagedHamiltonian format describing applicable Fermionic terms + swaps
    // nBitsPrecision - number of bits of precision to use with QPE
    // Output: estimated phase and estimated energy
    // operation EstimateEnergyLevel(data : PackagedHamiltonian, nBitsPrecision : Int) : (Double, Double) {
    //     // unpack the data
    //     let (constants, fermionTerms, statePrepData) = data!;
    //     let (nSpinOrbitals, energyOffset, trotterStep, trotterOrder) = constants!;

    //     let oracle = ApplyFermionTerms(fermionTerms, trotterStep, _);
    //     let rescaleFactor = 1.0 / trotterStep;
        
    //     let stateData = (statePrepData!);
    //     let statePrep = PrepareTrialState(stateData, _);

    //     let phaseEstAlgorithm = RobustPhaseEstimation(nBitsPrecision, _, _);

    //     let estPhase = EstimateEnergy(nSpinOrbitals, statePrep, oracle, phaseEstAlgorithm);
    //     let energyLevel = estPhase * rescaleFactor + energyOffset;
    //     return (estPhase, energyLevel);
    // }

    operation ApplyTrotterOracleOnce(data : CompleteHamiltonian) : Unit {
        // Apply the Trotter oracle once for resource estimation
        let (constants, statePrepData, swapTerms, interactionTerms) = data!;
        let (nSpinOrbitals, energyOffset, trotterStep, trotterOrder) = constants!;

        // prep and apply oracle
        let oracle = ApplyHamiltonianTerm(swapTerms, interactionTerms, constants, _);
        using (register = Qubit[nSpinOrbitals]) {
            oracle(register);
        }
    }

    // Apply fermion terms described in an array
    // Input: GeneratorIndex[] - array of terms + SWAPS to apply
    // trotterStep - step size to use 
    // qubits - qubit array to apply terms to
    // Output: Terms applied to qubit array
    operation ApplyFermionTerms (fermionTerms : GeneratorIndex[], trotterStep : Double, qubits : Qubit[]) : Unit is Adj+Ctl {
        // for each fermion
        for (i in 0..Length(fermionTerms) - 1) {
            let ((idxTermType, coeff), idxFermions) = fermionTerms[i]!;
            // Message($"{idxTermType}, {coeff}, {idxFermions}");
            if (idxTermType[0] == -2) {
                // SWAP
                ApplySWAPS(fermionTerms[i], qubits);
            } elif (idxTermType[0] == -1) {
                // skip - these terms set the energy offset
            } else {
                // Apply the normal JWES
                JordanWignerFermionImpl(fermionTerms[i], trotterStep, qubits);
            }
        }
    }

    // Apply a fermionic swap
    // Input: data describing fermionic SWAP in GeneratorIndex form
    // qubit array where swap should be applied
    // Output: SWAP applied, meaning that orbitals are reordered
    operation ApplySWAPS(swapData : GeneratorIndex, qubits : Qubit[]) : Unit is Adj+Ctl{
        let ((idxTermType, coeff), idxFermions) = swapData!;
        FermionicSWAP(qubits[idxFermions[0]], qubits[idxFermions[1]]);
    }

    // Apply a fermionic SWAP
    // temporary until pulled into Quantum Libraries github
    operation FermionicSWAP(qubit1 : Qubit, qubit2 : Qubit) : Unit is Adj+Ctl {
        // temporary until FermionicSWAP pulled into quantum libraries 
        SWAP(qubit1, qubit2);
        CZ(qubit1, qubit2);
    }
}
