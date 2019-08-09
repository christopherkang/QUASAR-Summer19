namespace ImportOptimizedFermions
{
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Intrinsic;

    operation ApplyFermionTerms () : Unit {
        // 
    }

    operation ApplySWAPS(swap_data : SWAPFORMAT, qubits : Qubit[]) : Unit {
        // 
        for (i in 0..Length(swap_data)) {
            // we need to somehow select two qubits
            let qubit1 = 0;
            let qubit2 = 0;

            // and swap them
            FermionicSWAP(qubits[qubit1], qubits[qubit2]);
        }
    }

    operation FermionicSWAP(qubit1 : Qubit, qubit2 : Qubit) : Unit is Adj+Ctl {
        // temporary until FermionicSWAP pulled into quantum libraries 
        body (...) {
            SWAP(qubit1, qubit2);
            CZ(qubit1, qubit2);
        }
    }
}
