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
    // open Microsoft.Quantum.Canon;
    // open Microsoft.Quantum.Intrinsic;
    // open Microsoft.Quantum.Arrays;
    // open Microsoft.Quantum.Chemistry;
    // open Microsoft.Quantum.Chemistry.JordanWigner;
	// open Microsoft.Quantum.Simulation;	
    // open Microsoft.Quantum.Characterization;
    // open Microsoft.Quantum.Convert;
    // open Microsoft.Quantum.Math;

    // This is a diagonstic methods and also experiment in generics
    operation AcceptArray<'T> (array : 'T[]) : Unit {
        for (i in 0..Length(array) - 1) {
            Message($"{array[i]}");
        }
    }

    function FindPauli (index : Int) : Pauli {
        let ops = [PauliI, PauliX, PauliY, PauliZ];
        // This should be modeled like the evolution set where the ops can be folded in
        return ops[index];
    }

    operation OracleFromJSON (data : CompressedHamiltonian[], register : Qubit[]) : Unit {
        body (...) {
            for (index in 0..Length(data) - 1) {
                ApplyTerm(index, data, register);
            }
        }
        controlled auto;
        adjoint auto;
        controlled adjoint auto;
    }

    // operation that takes in CompressedHamiltonian data and applies selected term
    // index : index of data that should be applied
    // data : array of CompressedHamiltonians to be applied
    // register : FULL register of qubits on which to apply the exponentiated gates
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

    // operation ConvertToGates (termSpecs : CompressedGateForm, qubits : Qubit[]) : Unit {
    //     let (termType, (target1, target2, target3, target4), angle) = termSpecs!;
    //     if (termType == "Zterm") 
    //     {
    //         Exp([PauliZ], angle, [qubits[target1]]);
    //     }
    //     elif (termType == "ZZterm")
    //     {
    //         let qubitsZZ = Subarray([target1, target2], qubits);
    //         Exp([PauliZ, PauliZ], angle, qubitsZZ);
    //     }
    //     elif (termType == "PQterm") 
    //     {
    //         let qubitsPQ = Subarray([target1, target2], qubits);
    //         let qubitsJW = qubits[target1 + 1 .. target2 - 1];
    //         let ops = [[PauliX, PauliX], [PauliY, PauliY]];
            
    //         for (idxOp in IndexRange(ops)) {
    //             Exp(ops[idxOp] + ConstantArray(Length(qubitsJW), PauliZ), angle, (qubitsPQ + qubitsJW));
    //         }
    //     }
    //     elif (termType == "PQQRterm") 
    //     {
    //         // targetInfo = (Int32.Parse(qubitInfo[1]), -1, -1, -1);
    //         // FLAG TO DO 
    //     }
    //     else {
    //         let qubitsPQ = Subarray([target1, target2], qubits);
    //         let qubitsRS = Subarray([target3, target4], qubits);
    //         let qubitsPQJW = qubits[target1 + 1 .. target2 - 1];
    //         let qubitsRSJW = qubits[target3 + 1 .. target4 - 1];
    //         let ops = [[PauliX, PauliX, PauliX, PauliX], [PauliX, PauliX, PauliY, PauliY], [PauliX, PauliY, PauliX, PauliY], [PauliY, PauliX, PauliX, PauliY], [PauliY, PauliY, PauliY, PauliY], [PauliY, PauliY, PauliX, PauliX], [PauliY, PauliX, PauliY, PauliX], [PauliX, PauliY, PauliY, PauliX]];
    //         // let idxOp = termType[9];
    //         // Exp(ops[idxOp] + ConstantArray(Length(qubitsPQJW) + Length(qubitsRSJW), PauliZ), angle, ((qubitsPQ + qubitsRS) + qubitsPQJW) + qubitsRSJW);
    //     }
    // }

    // operation ApplyFromFile (nSpinOrbitals : Int, data : CompressedGateForm[]) : Unit {
    //     using (qubits = Qubit[nSpinOrbitals]) {
    //         for (index in 0..Length(data) - 1) {
    //             ConvertToGates(data[index], qubits);
    //         }   
    //     }
    // }
}
