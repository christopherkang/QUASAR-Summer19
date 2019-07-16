namespace ConvertFileToGates {
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Chemistry.JordanWigner;

    // Hamiltonian type
    // The String is the name of the term, the Double is the angle
    // the tuple is of the control, target qubits, and the last Int[] is of the ops
    newtype CompressedHamiltonian = (String, Double, (Int[], Int[]), Int[]);

    // see below for the spec - for use in CompleteHamiltonian
    newtype CompressedConstants = (Int, Double, Double, Int);

    // Complete Hamiltonian storing all data needed to emulate. In the following format:
    // ((nSpinOrbitals, energyOffset, trotterStepSize, trotterOrder), InputStateData, TermData)
    newtype CompleteHamiltonian = (CompressedConstants, (Int, JordanWignerInputState[]), CompressedHamiltonian[]);
}
