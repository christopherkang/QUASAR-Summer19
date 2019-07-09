#pragma warning disable 1591
using System;
using Microsoft.Quantum.Core;
using Microsoft.Quantum.Intrinsic;
using Microsoft.Quantum.Simulation.Core;

[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"TypeConstructor\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedGateForm\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":7,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":9},\"Item2\":{\"Line\":1,\"Column\":27}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"__Item1__\"]},\"Type\":{\"Case\":\"String\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":1}}}]},{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"__Item2__\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":1}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"__Item3__\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":1}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"__Item4__\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":1}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"__Item5__\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":1}}}]}]]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"__Item6__\"]},\"Type\":{\"Case\":\"Double\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":1}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"String\"},{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"Int\"},{\"Case\":\"Int\"},{\"Case\":\"Int\"},{\"Case\":\"Int\"}]]},{\"Case\":\"Double\"}]]},\"ReturnType\":{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedGateForm\",\"Range\":{\"Case\":\"Null\"}}]},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":true}}},\"Documentation\":[\"type constructor for user defined type\"]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":true}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedGateForm\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":7,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":9},\"Item2\":{\"Line\":1,\"Column\":27}},\"Documentation\":[]}")]
[assembly: TypeDeclaration("{\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedGateForm\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":7,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":9},\"Item2\":{\"Line\":1,\"Column\":27}},\"Type\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"String\"},{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"Int\"},{\"Case\":\"Int\"},{\"Case\":\"Int\"},{\"Case\":\"Int\"}]]},{\"Case\":\"Double\"}]]},\"TypeItems\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"Case\":\"Anonymous\",\"Fields\":[{\"Case\":\"String\"}]}]},{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"Case\":\"Anonymous\",\"Fields\":[{\"Case\":\"Int\"}]}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"Case\":\"Anonymous\",\"Fields\":[{\"Case\":\"Int\"}]}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"Case\":\"Anonymous\",\"Fields\":[{\"Case\":\"Int\"}]}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"Case\":\"Anonymous\",\"Fields\":[{\"Case\":\"Int\"}]}]}]]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"Case\":\"Anonymous\",\"Fields\":[{\"Case\":\"Double\"}]}]}]]},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"HelloQ\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":9,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":17}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"UnitType\"},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"HelloQ\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":9,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":17}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"AcceptArray\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":13,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":22}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"array\"]},\"Type\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"TypeParameter\",\"Fields\":[{\"Origin\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"AcceptArray\"},\"TypeName\":\"T\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":36},\"Item2\":{\"Line\":1,\"Column\":40}}]}}]}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":28},\"Item2\":{\"Line\":1,\"Column\":33}}}]}]]},\"Signature\":{\"TypeParameters\":[{\"Case\":\"ValidName\",\"Fields\":[\"T\"]}],\"ArgumentType\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"TypeParameter\",\"Fields\":[{\"Origin\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"AcceptArray\"},\"TypeName\":\"T\",\"Range\":{\"Case\":\"Null\"}}]}]},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"AcceptArray\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":13,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":22}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"ConvertToGates\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":19,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":25}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"termSpecs\"]},\"Type\":{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedGateForm\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":39},\"Item2\":{\"Line\":1,\"Column\":57}}]}}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":27},\"Item2\":{\"Line\":1,\"Column\":36}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"qubits\"]},\"Type\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"Qubit\"}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":59},\"Item2\":{\"Line\":1,\"Column\":65}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedGateForm\",\"Range\":{\"Case\":\"Null\"}}]},{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"Qubit\"}]}]]},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"ConvertToGates\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":19,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":25}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"ApplyFromFile\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":56,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":24}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"nSpinOrbitals\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":26},\"Item2\":{\"Line\":1,\"Column\":39}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"data\"]},\"Type\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedGateForm\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":54},\"Item2\":{\"Line\":1,\"Column\":74}}]}}]}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":47},\"Item2\":{\"Line\":1,\"Column\":51}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"Int\"},{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedGateForm\",\"Range\":{\"Case\":\"Null\"}}]}]}]]},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"ApplyFromFile\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":56,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":24}},\"Documentation\":[]}")]
#line hidden
namespace ConvertFileToGates
{
    public class CompressedGateForm : UDTBase<(String,(Int64,Int64,Int64,Int64),Double)>, IApplyData
    {
        public CompressedGateForm() : base(default((String,(Int64,Int64,Int64,Int64),Double)))
        {
        }

        public CompressedGateForm((String,(Int64,Int64,Int64,Int64),Double) data) : base(data)
        {
        }

        public String Item1 => Data.Item1;
        public (Int64,Int64,Int64,Int64) Item2 => Data.Item2;
        public Double Item3 => Data.Item3;
        System.Collections.Generic.IEnumerable<Qubit> IApplyData.Qubits => null;
        public void Deconstruct(out String item1, out (Int64,Int64,Int64,Int64) item2, out Double item3)
        {
            item1 = Data.Item1;
            item2 = Data.Item2;
            item3 = Data.Item3;
        }
    }

    public partial class HelloQ : Operation<QVoid, QVoid>, ICallable
    {
        public HelloQ(IOperationFactory m) : base(m)
        {
        }

        String ICallable.Name => "HelloQ";
        String ICallable.FullName => "ConvertFileToGates.HelloQ";
        protected ICallable<String, QVoid> MicrosoftQuantumIntrinsicMessage
        {
            get;
            set;
        }

        public override Func<QVoid, QVoid> Body => (__in__) =>
        {
#line 11 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            MicrosoftQuantumIntrinsicMessage.Apply("Hello quantum world!");
#line hidden
            return QVoid.Instance;
        }

        ;
        public override void Init()
        {
            this.MicrosoftQuantumIntrinsicMessage = this.Factory.Get<ICallable<String, QVoid>>(typeof(Microsoft.Quantum.Intrinsic.Message));
        }

        public override IApplyData __dataIn(QVoid data) => data;
        public override IApplyData __dataOut(QVoid data) => data;
        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__)
        {
            return __m__.Run<HelloQ, QVoid, QVoid>(QVoid.Instance);
        }
    }

    public partial class AcceptArray<__T__> : Operation<IQArray<__T__>, QVoid>, ICallable
    {
        public AcceptArray(IOperationFactory m) : base(m)
        {
        }

        String ICallable.Name => "AcceptArray";
        String ICallable.FullName => "ConvertFileToGates.AcceptArray";
        protected ICallable Length
        {
            get;
            set;
        }

        protected ICallable<String, QVoid> MicrosoftQuantumIntrinsicMessage
        {
            get;
            set;
        }

        public override Func<IQArray<__T__>, QVoid> Body => (__in__) =>
        {
            var array = __in__;
#line 15 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            foreach (var i in new Range(0L, (array.Length - 1L)))
#line hidden
            {
#line 16 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                MicrosoftQuantumIntrinsicMessage.Apply(String.Format("{0}", array[i]));
            }

#line hidden
            return QVoid.Instance;
        }

        ;
        public override void Init()
        {
            this.Length = this.Factory.Get<ICallable>(typeof(Microsoft.Quantum.Core.Length<>));
            this.MicrosoftQuantumIntrinsicMessage = this.Factory.Get<ICallable<String, QVoid>>(typeof(Microsoft.Quantum.Intrinsic.Message));
        }

        public override IApplyData __dataIn(IQArray<__T__> data) => data;
        public override IApplyData __dataOut(QVoid data) => data;
        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, IQArray<__T__> array)
        {
            return __m__.Run<AcceptArray<__T__>, IQArray<__T__>, QVoid>(array);
        }
    }

    public partial class ConvertToGates : Operation<(CompressedGateForm,IQArray<Qubit>), QVoid>, ICallable
    {
        public ConvertToGates(IOperationFactory m) : base(m)
        {
        }

        public class In : QTuple<(CompressedGateForm,IQArray<Qubit>)>, IApplyData
        {
            public In((CompressedGateForm,IQArray<Qubit>) data) : base(data)
            {
            }

            System.Collections.Generic.IEnumerable<Qubit> IApplyData.Qubits => ((IApplyData)Data.Item2)?.Qubits;
        }

        String ICallable.Name => "ConvertToGates";
        String ICallable.FullName => "ConvertFileToGates.ConvertToGates";
        protected ICallable MicrosoftQuantumArraysConstantArray
        {
            get;
            set;
        }

        protected ICallable MicrosoftQuantumArraysIndexRange
        {
            get;
            set;
        }

        protected ICallable MicrosoftQuantumArraysSubarray
        {
            get;
            set;
        }

        protected ICallable Length
        {
            get;
            set;
        }

        protected IUnitary<(IQArray<Pauli>,Double,IQArray<Qubit>)> MicrosoftQuantumIntrinsicExp
        {
            get;
            set;
        }

        public override Func<(CompressedGateForm,IQArray<Qubit>), QVoid> Body => (__in__) =>
        {
            var (termSpecs,qubits) = __in__;
#line 21 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var (termType,(target1,target2,target3,target4),angle) = termSpecs.Data;
#line 22 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            if ((termType == "Zterm"))
            {
#line 24 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                MicrosoftQuantumIntrinsicExp.Apply((new QArray<Pauli>(Pauli.PauliZ), angle, new QArray<Qubit>(qubits[target1])));
            }
            else if ((termType == "ZZterm"))
            {
#line 28 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var qubitsZZ = (IQArray<Qubit>)MicrosoftQuantumArraysSubarray.Apply<IQArray<Qubit>>((new QArray<Int64>(target1, target2), qubits));
#line 29 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                MicrosoftQuantumIntrinsicExp.Apply((new QArray<Pauli>(Pauli.PauliZ, Pauli.PauliZ), angle, qubitsZZ));
            }
            else if ((termType == "PQterm"))
            {
#line 33 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var qubitsPQ = (IQArray<Qubit>)MicrosoftQuantumArraysSubarray.Apply<IQArray<Qubit>>((new QArray<Int64>(target1, target2), qubits));
#line 34 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var qubitsJW = (IQArray<Qubit>)qubits?.Slice(new Range((target1 + 1L), (target2 - 1L)));
#line 35 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var ops = (IQArray<IQArray<Pauli>>)new QArray<IQArray<Pauli>>(new QArray<Pauli>(Pauli.PauliX, Pauli.PauliX), new QArray<Pauli>(Pauli.PauliY, Pauli.PauliY));
#line 37 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                foreach (var idxOp in MicrosoftQuantumArraysIndexRange.Apply<Range>(ops))
#line hidden
                {
#line 38 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                    MicrosoftQuantumIntrinsicExp.Apply((QArray<Pauli>.Add(ops[idxOp], MicrosoftQuantumArraysConstantArray.Apply<IQArray<Pauli>>((qubitsJW.Length, Pauli.PauliZ))), angle, QArray<Qubit>.Add(qubitsPQ, qubitsJW)));
                }
            }
            else if ((termType == "PQQRterm"))
            {
            }
            else
            {
#line 47 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var qubitsPQ = (IQArray<Qubit>)MicrosoftQuantumArraysSubarray.Apply<IQArray<Qubit>>((new QArray<Int64>(target1, target2), qubits));
#line 48 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var qubitsRS = (IQArray<Qubit>)MicrosoftQuantumArraysSubarray.Apply<IQArray<Qubit>>((new QArray<Int64>(target3, target4), qubits));
#line 49 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var qubitsPQJW = (IQArray<Qubit>)qubits?.Slice(new Range((target1 + 1L), (target2 - 1L)));
#line 50 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var qubitsRSJW = (IQArray<Qubit>)qubits?.Slice(new Range((target3 + 1L), (target4 - 1L)));
#line 51 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var ops = (IQArray<IQArray<Pauli>>)new QArray<IQArray<Pauli>>(new QArray<Pauli>(Pauli.PauliX, Pauli.PauliX, Pauli.PauliX, Pauli.PauliX), new QArray<Pauli>(Pauli.PauliX, Pauli.PauliX, Pauli.PauliY, Pauli.PauliY), new QArray<Pauli>(Pauli.PauliX, Pauli.PauliY, Pauli.PauliX, Pauli.PauliY), new QArray<Pauli>(Pauli.PauliY, Pauli.PauliX, Pauli.PauliX, Pauli.PauliY), new QArray<Pauli>(Pauli.PauliY, Pauli.PauliY, Pauli.PauliY, Pauli.PauliY), new QArray<Pauli>(Pauli.PauliY, Pauli.PauliY, Pauli.PauliX, Pauli.PauliX), new QArray<Pauli>(Pauli.PauliY, Pauli.PauliX, Pauli.PauliY, Pauli.PauliX), new QArray<Pauli>(Pauli.PauliX, Pauli.PauliY, Pauli.PauliY, Pauli.PauliX));
            }

#line hidden
            return QVoid.Instance;
        }

        ;
        public override void Init()
        {
            this.MicrosoftQuantumArraysConstantArray = this.Factory.Get<ICallable>(typeof(Microsoft.Quantum.Arrays.ConstantArray<>));
            this.MicrosoftQuantumArraysIndexRange = this.Factory.Get<ICallable>(typeof(Microsoft.Quantum.Arrays.IndexRange<>));
            this.MicrosoftQuantumArraysSubarray = this.Factory.Get<ICallable>(typeof(Microsoft.Quantum.Arrays.Subarray<>));
            this.Length = this.Factory.Get<ICallable>(typeof(Microsoft.Quantum.Core.Length<>));
            this.MicrosoftQuantumIntrinsicExp = this.Factory.Get<IUnitary<(IQArray<Pauli>,Double,IQArray<Qubit>)>>(typeof(Microsoft.Quantum.Intrinsic.Exp));
        }

        public override IApplyData __dataIn((CompressedGateForm,IQArray<Qubit>) data) => new In(data);
        public override IApplyData __dataOut(QVoid data) => data;
        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, CompressedGateForm termSpecs, IQArray<Qubit> qubits)
        {
            return __m__.Run<ConvertToGates, (CompressedGateForm,IQArray<Qubit>), QVoid>((termSpecs, qubits));
        }
    }

    public partial class ApplyFromFile : Operation<(Int64,IQArray<CompressedGateForm>), QVoid>, ICallable
    {
        public ApplyFromFile(IOperationFactory m) : base(m)
        {
        }

        public class In : QTuple<(Int64,IQArray<CompressedGateForm>)>, IApplyData
        {
            public In((Int64,IQArray<CompressedGateForm>) data) : base(data)
            {
            }

            System.Collections.Generic.IEnumerable<Qubit> IApplyData.Qubits => null;
        }

        String ICallable.Name => "ApplyFromFile";
        String ICallable.FullName => "ConvertFileToGates.ApplyFromFile";
        protected ICallable<(CompressedGateForm,IQArray<Qubit>), QVoid> ConvertToGates
        {
            get;
            set;
        }

        protected ICallable Length
        {
            get;
            set;
        }

        protected Allocate Allocate
        {
            get;
            set;
        }

        protected Release Release
        {
            get;
            set;
        }

        public override Func<(Int64,IQArray<CompressedGateForm>), QVoid> Body => (__in__) =>
        {
            var (nSpinOrbitals,data) = __in__;
#line hidden
            {
#line 58 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                var qubits = Allocate.Apply(nSpinOrbitals);
#line hidden
                Exception __arg1__ = null;
                try
                {
#line 59 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                    foreach (var index in new Range(0L, (data.Length - 1L)))
#line hidden
                    {
#line 60 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                        ConvertToGates.Apply((data[index], qubits));
                    }
                }
#line hidden
                catch (Exception __arg2__)
                {
                    __arg1__ = __arg2__;
                    throw __arg1__;
                }
#line hidden
                finally
                {
                    if (__arg1__ != null)
                    {
                        throw __arg1__;
                    }

#line hidden
                    Release.Apply(qubits);
                }
            }

#line hidden
            return QVoid.Instance;
        }

        ;
        public override void Init()
        {
            this.ConvertToGates = this.Factory.Get<ICallable<(CompressedGateForm,IQArray<Qubit>), QVoid>>(typeof(ConvertToGates));
            this.Length = this.Factory.Get<ICallable>(typeof(Microsoft.Quantum.Core.Length<>));
            this.Allocate = this.Factory.Get<Allocate>(typeof(Microsoft.Quantum.Intrinsic.Allocate));
            this.Release = this.Factory.Get<Release>(typeof(Microsoft.Quantum.Intrinsic.Release));
        }

        public override IApplyData __dataIn((Int64,IQArray<CompressedGateForm>) data) => new In(data);
        public override IApplyData __dataOut(QVoid data) => data;
        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, Int64 nSpinOrbitals, IQArray<CompressedGateForm> data)
        {
            return __m__.Run<ApplyFromFile, (Int64,IQArray<CompressedGateForm>), QVoid>((nSpinOrbitals, data));
        }
    }
}