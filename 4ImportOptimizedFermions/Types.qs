namespace ImportOptimizedFermions
{
    open Microsoft.Quantum.Simulation;
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Chemistry;
    open Microsoft.Quantum.Arrays;
    open Microsoft.Quantum.Chemistry.JordanWigner;

    // State prep data as described in the JSON
    // Tuple / Int[] / Int
    newtype StatePrepData = (Int, JordanWignerInputState[]);

    // Constants
    // nSpinOrbitals, energyOffset, trotterStep, trotterOrder
    newtype HamiltonianConstants = (Int, Double, Double, Int);

    // Hamiltonian wrapped up
    newtype PackagedHamiltonian = (HamiltonianConstants, GeneratorIndex[], StatePrepData);
}
