namespace ImportOptimizedFermions
{
    open Microsoft.Quantum.Simulation;
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Chemistry;
    open Microsoft.Quantum.Arrays;
    open Microsoft.Quantum.Chemistry.JordanWigner;

    operation EstimateEnergy() : (Double, Double) {
        // 
        return (0.0, 0.0);
    }

    operation ApplyFermionTerms (fermionTerms : GeneratorIndex[], qubits : Qubit[]) : Unit {
        // input: Fermion Terms
        // output: Qubit array modified

        // for each fermion
        mutable energyOffset = 0.0;
        let trotterStep = 1.0;
        for (i in 0..Length(fermionTerms)) {
            let ((idxTermType, coeff), idxFermions) = fermionTerms[i]!;
            if (idxTermType[0] == -2) {
                // SWAP
                ApplySWAPS(fermionTerms[i], qubits);
            } elif (idxTermType[0] == -1) {
                // add up the identities
                set energyOffset += coeff[0];
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
