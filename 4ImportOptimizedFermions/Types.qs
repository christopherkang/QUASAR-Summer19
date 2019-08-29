namespace ImportOptimizedFermions
{
    open Microsoft.Quantum.Simulation;
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Chemistry;
    open Microsoft.Quantum.Arrays;
    open Microsoft.Quantum.Chemistry.JordanWigner;

    //
    newtype CompleteHamiltonian = (HamiltonianConstants, StatePrepData, SWAPRound[], JordanWignerEncodingData[]);

    // State prep data as described in the JSON
    // Tuple / Int[] / Int
    newtype StatePrepData = (Int, JordanWignerInputState[]);

    // nSpinOrbitals, energyOffset, trotterStep, trotterOrder
    newtype HamiltonianConstants = (Int, Double, Double, Int);

    // Series of SWAPS to be applied; the qubits to be swapped should be popped off the top
    // These SWAPs should be able to be applied simultaneously; i.e. none use the same qubits
    newtype SWAPSeries = (Int[], Int[]);

    // Rounds of SWAPs to be applied. SWAPRound and Interaction should be applied in an alternating fashion
    newtype SWAPRound = SWAPSeries[];
}
