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

    // Produce a Fermionic SWAP enhanced Hamiltonian energy level estimate
    // Input: PackagedHamiltonian format describing applicable Fermionic terms + swaps
    // nBitsPrecision - number of bits of precision to use with QPE
    // Output: estimated phase and estimated energy
    operation EstimateEnergyLevel(data : PackagedHamiltonian, nBitsPrecision : Int) : (Double, Double) {
        // unpack the data
        let (constants, fermionTerms, statePrepData) = data!;
        let (nSpinOrbitals, energyOffset, trotterStep, trotterOrder) = constants!;

        let oracle = ApplyFermionTerms(fermionTerms, trotterStep, _);
        let rescaleFactor = 1.0 / trotterStep;
        
        let stateData = (statePrepData!);
        let statePrep = PrepareTrialState(stateData, _);

        let phaseEstAlgorithm = RobustPhaseEstimation(nBitsPrecision, _, _);

        let estPhase = EstimateEnergy(nSpinOrbitals, statePrep, oracle, phaseEstAlgorithm);
        let energyLevel = estPhase * rescaleFactor + energyOffset;
        return (estPhase, energyLevel);
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
