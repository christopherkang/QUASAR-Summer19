namespace ConvertFileToGates
{
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Chemistry.JordanWigner;  
	open Microsoft.Quantum.Simulation;	
    open Microsoft.Quantum.Characterization;
    open Microsoft.Quantum.Convert;
    open Microsoft.Quantum.Math;
    open Microsoft.Quantum.Arrays;

    // This is a diagonstic methods and also experiment in generics
    operation AcceptArray<'T> (array : 'T[]) : Unit {
        for (i in 0..Length(array) - 1) {
            Message($"{array[i]}");
        }
    }

    // converts type index in CompressedHamiltonian into a qubit gate
    // input: index found in CompressedHamiltonian under "ops"
    // output: Pauli gate to use
    function FindPauli (index : Int) : Pauli {
        let ops = [PauliI, PauliX, PauliY, PauliZ];
        // This should be modeled like the evolution set where the ops can be folded in
        return ops[index];
    }


    // operation that takes in CompressedHamiltonian data and applies selected term
    // input: index, index of data that should be applied
    // data, array of CompressedHamiltonians to be applied
    // register, FULL register of qubits on which to apply the exponentiated gates
    // output: applies the term onto the register. Should be controllable and adjointable
    operation ApplyTerm (index : Int, data : CompressedHamiltonian[], register : Qubit[]) : Unit is Ctl+Adj {
        body (...) {
            let gate_data = data[index];
            let (name, angle, (control, target), ops) = gate_data!;
            Exp(Mapped(FindPauli, ops), angle, Subarray(target, register));
        }
        controlled auto;
        adjoint auto;
        controlled adjoint auto;
    }


    // converts a JSON into an oracle to be applied onto a register
    // input: data, produced by Auxiliary, register: qubit register
    // output: no output, simply applies gates to register
    operation OracleFromJSON (data : CompressedHamiltonian[], register : Qubit[]) : Unit is Ctl+Adj {
        body (...) {
            for (index in 0..Length(data) - 1) {
                ApplyTerm(index, data, register);
            }
        }
        controlled auto;
        adjoint auto;
        controlled adjoint auto;
    }


    // operation modified from sample on Trotterization; allows for energy level estimate
    // input: data, produced by Auxiliary class, nBitsPrecision, number of bits of precision
    // for the energy level estimate
    // output: (phase, energy)
    operation GetEnergyByTrotterization (data : CompleteHamiltonian, nBitsPrecision : Int) : (Double, Double) {

        // Decompress data:
        let (constants, initialState, termData) = data!;
        let (nSpinOrbitals, energyOffset, trotterStepSize, trotterOrder) = constants!;

        // We use a Product formula, also known as `Trotterization` to
        // simulate the Hamiltonian.
        let rescaleFactor = 1.0 / trotterStepSize;
        let oracle = OracleFromJSON(termData, _);
        
        // The operation that creates the trial state is defined below.
        // By default, greedy filling of spin-orbitals is used.
        let statePrep = PrepareTrialState(initialState, _);
        
        // We use the Robust Phase Estimation algorithm
        // of Kimmel, Low and Yoder.
        let phaseEstAlgorithm = RobustPhaseEstimation(nBitsPrecision, _, _);
        
        // This runs the quantum algorithm and returns a phase estimate.
        let estPhase = EstimateEnergy(nSpinOrbitals, statePrep, oracle, phaseEstAlgorithm);
        
        // We obtain the energy estimate by rescaling the phase estimate
        // with the trotterStepSize. We also add the constant energy offset
        // to the estimated energy.
        let estEnergy = estPhase * rescaleFactor + energyOffset;
        
        // We return both the estimated phase, and the estimated energy.
        return (estPhase, estEnergy);
    }
}
