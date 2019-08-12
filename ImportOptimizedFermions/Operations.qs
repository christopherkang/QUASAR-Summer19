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

    operation EstimateEnergyLevel(data : PackagedHamiltonian, nBitsPrecision : Int) : (Double, Double) {
        let (constants, fermionTerms, statePrepData) = data!;
        let (nSpinOrbitals, energyOffset, trotterStep, trotterOrder) = constants!;


        let oracle = ApplyFermionTerms(fermionTerms, trotterStep, _);
        let rescaleFactor = 1.0 / trotterStep;
        
        // The operation that creates the trial state is defined below.
        // By default, greedy filling of spin-orbitals is used.
        let stateData = (statePrepData!);
        let statePrep = PrepareTrialState(stateData, _);
        
        // We use the Robust Phase Estimation algorithm
        // of Kimmel, Low and Yoder.
        let phaseEstAlgorithm = RobustPhaseEstimation(nBitsPrecision, _, _);

        let estPhase = EstimateEnergy(nSpinOrbitals, statePrep, oracle, phaseEstAlgorithm);
        let energyLevel = estPhase * rescaleFactor + energyOffset;
        return (energyLevel, estPhase);
    }

    operation ApplyFermionTerms (fermionTerms : GeneratorIndex[], trotterStep : Double, qubits : Qubit[]) : Unit is Adj+Ctl {
        // input: Fermion Terms
        // output: Qubit array modified

        // for each fermion
        // mutable energyOffset = 0.0;
        for (i in 0..Length(fermionTerms) - 1) {
            let ((idxTermType, coeff), idxFermions) = fermionTerms[i]!;
            // Message($"{idxTermType} | {idxFermions}");
            if (idxTermType[0] == -2) {
                // SWAP
                ApplySWAPS(fermionTerms[i], qubits);
            } elif (idxTermType[0] == -1) {
                // add up the identities
                // set energyOffset += coeff[0];
            } else {
                // Apply the normal JWES
                JordanWignerFermionImpl(fermionTerms[i], trotterStep, qubits);
            }
        }
    }

    operation ApplySWAPS(swapData : GeneratorIndex, qubits : Qubit[]) : Unit is Adj+Ctl{
        // unpack the data
        let ((idxTermType, coeff), idxFermions) = swapData!;
        FermionicSWAP(qubits[idxFermions[0]], qubits[idxFermions[1]]);
    }

    operation FermionicSWAP(qubit1 : Qubit, qubit2 : Qubit) : Unit is Adj+Ctl {
        // temporary until FermionicSWAP pulled into quantum libraries 
        SWAP(qubit1, qubit2);
        CZ(qubit1, qubit2);
    }
}
