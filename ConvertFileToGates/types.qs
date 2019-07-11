namespace ConvertFileToGates {
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;

    // Hamiltonian type
    // The String is the name of the term, the Double is the angle
    // the tuple is of the control, target qubits, and the last Int[] is of the ops
    newtype CompressedHamiltonian = (String, Double, (Int[], Int[]), Int[]);
}
