namespace ProduceSampleEnergy
{
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Chemistry.JordanWigner;  
	open Microsoft.Quantum.Simulation;	
    open Microsoft.Quantum.Characterization;
    open Microsoft.Quantum.Convert;
    open Microsoft.Quantum.Math;

    operation GetEnergyByTrotterization (qSharpData : JordanWignerEncodingData, nBitsPrecision : Int, trotterStepSize : Double, trotterOrder : Int) : (Double, Double) {
        
        // The data describing the Hamiltonian for all these steps is contained in
        // `qSharpData`
        let (nSpinOrbitals, fermionTermData, statePrepData, energyOffset) = qSharpData!;
        
        // We use a Product formula, also known as `Trotterization` to
        // simulate the Hamiltonian.
        let (nQubits, (rescaleFactor, oracle)) = TrotterStepOracle(qSharpData, trotterStepSize, trotterOrder);
        
        // The operation that creates the trial state is defined below.
        // By default, greedy filling of spin-orbitals is used.
        let statePrep = PrepareTrialState(statePrepData, _);
        
        // We use the Robust Phase Estimation algorithm
        // of Kimmel, Low and Yoder.
        let phaseEstAlgorithm = RobustPhaseEstimation(nBitsPrecision, _, _);
        
        // This runs the quantum algorithm and returns a phase estimate.
        let estPhase = EstimateEnergy(nQubits, statePrep, oracle, phaseEstAlgorithm);
        
        // We obtain the energy estimate by rescaling the phase estimate
        // with the trotterStepSize. We also add the constant energy offset
        // to the estimated energy.
        let estEnergy = estPhase * rescaleFactor + energyOffset;
        
        // We return both the estimated phase, and the estimated energy.
        return (estPhase, estEnergy);
    }

    operation ApplyTrotterOracleOnce(qSharpData : JordanWignerEncodingData, trotterStepSize : Double, trotterOrder : Int) : Unit {
        // applies a single iteration of the Trotter oracle for resource estimation purposes

        let (nSpinOrbitals, fermionTermData, statePrepData, energyOffset) = qSharpData!;
        using (register = Qubit[nSpinOrbitals]) {
            // prepare trotter oracle
            let (nQubits, (rescaleFactor, oracle)) = TrotterStepOracle(qSharpData, trotterStepSize, trotterOrder);

            // apply oracle
            oracle(register);
        }
    }
}
