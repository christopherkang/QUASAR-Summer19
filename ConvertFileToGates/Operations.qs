namespace ConvertFileToGates
{
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Arrays;
    open Microsoft.Quantum.Chemistry;

    newtype CompressedGateForm = (String, (Int, Int, Int, Int), Double);

    operation HelloQ () : Unit {
        Message("Hello quantum world!");
    }

    operation AcceptArray<'T> (array : 'T[]) : Unit {
        for (i in 0..Length(array) - 1) {
            Message($"{array[i]}");
        }
    }

    operation ConvertToGates (termSpecs : CompressedGateForm, qubits : Qubit[]) : Unit {
        let (termType, (target1, target2, target3, target4), angle) = termSpecs!;
        if (termType == "Zterm") 
        {
            Exp([PauliZ], angle, [qubits[target1]]);
        }
        elif (termType == "ZZterm")
        {
            let qubitsZZ = Subarray([target1, target2], qubits);
            Exp([PauliZ, PauliZ], angle, qubitsZZ);
        }
        elif (termType == "PQterm") 
        {
            let qubitsPQ = Subarray([target1, target2], qubits);
            let qubitsJW = qubits[target1 + 1 .. target2 - 1];
            let ops = [[PauliX, PauliX], [PauliY, PauliY]];
            
            for (idxOp in IndexRange(ops)) {
                Exp(ops[idxOp] + ConstantArray(Length(qubitsJW), PauliZ), angle, (qubitsPQ + qubitsJW));
            }
        }
        elif (termType == "PQQRterm") 
        {
            // targetInfo = (Int32.Parse(qubitInfo[1]), -1, -1, -1);
            // FLAG TO DO 
        }
        else {
            let qubitsPQ = Subarray([target1, target2], qubits);
            let qubitsRS = Subarray([target3, target4], qubits);
            let qubitsPQJW = qubits[target1 + 1 .. target2 - 1];
            let qubitsRSJW = qubits[target3 + 1 .. target4 - 1];
            let ops = [[PauliX, PauliX, PauliX, PauliX], [PauliX, PauliX, PauliY, PauliY], [PauliX, PauliY, PauliX, PauliY], [PauliY, PauliX, PauliX, PauliY], [PauliY, PauliY, PauliY, PauliY], [PauliY, PauliY, PauliX, PauliX], [PauliY, PauliX, PauliY, PauliX], [PauliX, PauliY, PauliY, PauliX]];
            // let idxOp = termType[9];
            // Exp(ops[idxOp] + ConstantArray(Length(qubitsPQJW) + Length(qubitsRSJW), PauliZ), angle, ((qubitsPQ + qubitsRS) + qubitsPQJW) + qubitsRSJW);
        }
    }

    operation ApplyFromFile (nSpinOrbitals : Int, data : CompressedGateForm[]) : Unit {
        using (qubits = Qubit[nSpinOrbitals]) {
            for (index in 0..Length(data) - 1) {
                ConvertToGates(data[index], qubits);
            }   
        }
    }
}
