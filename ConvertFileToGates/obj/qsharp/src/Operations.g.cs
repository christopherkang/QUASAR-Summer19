#pragma warning disable 1591
using System;
using Microsoft.Quantum.Core;
using Microsoft.Quantum.Intrinsic;
using Microsoft.Quantum.Simulation.Core;

[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"AcceptArray\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":21,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":22}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"array\"]},\"Type\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"TypeParameter\",\"Fields\":[{\"Origin\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"AcceptArray\"},\"TypeName\":\"T\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":36},\"Item2\":{\"Line\":1,\"Column\":40}}]}}]}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":28},\"Item2\":{\"Line\":1,\"Column\":33}}}]}]]},\"Signature\":{\"TypeParameters\":[{\"Case\":\"ValidName\",\"Fields\":[\"T\"]}],\"ArgumentType\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"TypeParameter\",\"Fields\":[{\"Origin\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"AcceptArray\"},\"TypeName\":\"T\",\"Range\":{\"Case\":\"Null\"}}]}]},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"AcceptArray\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":21,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":22}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Function\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"FindPauli\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":27,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":10},\"Item2\":{\"Line\":1,\"Column\":19}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"index\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":21},\"Item2\":{\"Line\":1,\"Column\":26}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"Int\"},\"ReturnType\":{\"Case\":\"Pauli\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"FindPauli\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":27,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":10},\"Item2\":{\"Line\":1,\"Column\":19}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"OracleFromJSON\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":33,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":25}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"data\"]},\"Type\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedHamiltonian\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":34},\"Item2\":{\"Line\":1,\"Column\":57}}]}}]}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":27},\"Item2\":{\"Line\":1,\"Column\":31}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"register\"]},\"Type\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"Qubit\"}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":59},\"Item2\":{\"Line\":1,\"Column\":67}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedHamiltonian\",\"Range\":{\"Case\":\"Null\"}}]}]},{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"Qubit\"}]}]]},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"OracleFromJSON\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":34,\"Item2\":8},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":5}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsControlled\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"OracleFromJSON\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":39,\"Item2\":8},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":11}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsAdjoint\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"OracleFromJSON\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":40,\"Item2\":8},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":8}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsControlledAdjoint\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"OracleFromJSON\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":41,\"Item2\":8},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":19}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"ApplyTerm\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":48,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":20}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"index\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":22},\"Item2\":{\"Line\":1,\"Column\":27}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"data\"]},\"Type\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedHamiltonian\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":42},\"Item2\":{\"Line\":1,\"Column\":65}}]}}]}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":35},\"Item2\":{\"Line\":1,\"Column\":39}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"register\"]},\"Type\":{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"Qubit\"}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":67},\"Item2\":{\"Line\":1,\"Column\":75}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"Int\"},{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompressedHamiltonian\",\"Range\":{\"Case\":\"Null\"}}]}]},{\"Case\":\"ArrayType\",\"Fields\":[{\"Case\":\"Qubit\"}]}]]},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"ApplyTerm\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":49,\"Item2\":8},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":5}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsControlled\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"ApplyTerm\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":54,\"Item2\":8},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":11}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsAdjoint\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"ApplyTerm\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":55,\"Item2\":8},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":8}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsControlledAdjoint\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"Union\",\"Fields\":[{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Controllable\"}]},{\"Case\":\"SimpleSet\",\"Fields\":[{\"Case\":\"Adjointable\"}]}]},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"ApplyTerm\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":56,\"Item2\":8},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":1},\"Item2\":{\"Line\":1,\"Column\":19}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"GetEnergyByTrotterization\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":59,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":36}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"data\"]},\"Type\":{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompleteHamiltonian\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":45},\"Item2\":{\"Line\":1,\"Column\":64}}]}}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":38},\"Item2\":{\"Line\":1,\"Column\":42}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"nBitsPrecision\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":66},\"Item2\":{\"Line\":1,\"Column\":80}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"CompleteHamiltonian\",\"Range\":{\"Case\":\"Null\"}}]},{\"Case\":\"Int\"}]]},\"ReturnType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"Double\"},{\"Case\":\"Double\"}]]},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ConvertFileToGates\",\"Name\":\"GetEnergyByTrotterization\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs\",\"Position\":{\"Item1\":59,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":36}},\"Documentation\":[]}")]
#line hidden
namespace ConvertFileToGates
{
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
#line 23 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            foreach (var i in new Range(0L, (array.Length - 1L)))
#line hidden
            {
#line 24 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
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

    public partial class FindPauli : Function<Int64, Pauli>, ICallable
    {
        public FindPauli(IOperationFactory m) : base(m)
        {
        }

        String ICallable.Name => "FindPauli";
        String ICallable.FullName => "ConvertFileToGates.FindPauli";
        public override Func<Int64, Pauli> Body => (__in__) =>
        {
            var index = __in__;
#line 29 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var ops = (IQArray<Pauli>)new QArray<Pauli>(Pauli.PauliI, Pauli.PauliX, Pauli.PauliY, Pauli.PauliZ);
#line 31 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            return ops[index];
        }

        ;
        public override void Init()
        {
        }

        public override IApplyData __dataIn(Int64 data) => new QTuple<Int64>(data);
        public override IApplyData __dataOut(Pauli data) => new QTuple<Pauli>(data);
        public static System.Threading.Tasks.Task<Pauli> Run(IOperationFactory __m__, Int64 index)
        {
            return __m__.Run<FindPauli, Int64, Pauli>(index);
        }
    }

    public partial class OracleFromJSON : Unitary<(IQArray<CompressedHamiltonian>,IQArray<Qubit>)>, ICallable
    {
        public OracleFromJSON(IOperationFactory m) : base(m)
        {
        }

        public class In : QTuple<(IQArray<CompressedHamiltonian>,IQArray<Qubit>)>, IApplyData
        {
            public In((IQArray<CompressedHamiltonian>,IQArray<Qubit>) data) : base(data)
            {
            }

            System.Collections.Generic.IEnumerable<Qubit> IApplyData.Qubits => ((IApplyData)Data.Item2)?.Qubits;
        }

        String ICallable.Name => "OracleFromJSON";
        String ICallable.FullName => "ConvertFileToGates.OracleFromJSON";
        protected IUnitary<(Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>)> ApplyTerm
        {
            get;
            set;
        }

        protected ICallable Length
        {
            get;
            set;
        }

        protected ICallable<Range, Range> RangeReverse
        {
            get;
            set;
        }

        public override Func<(IQArray<CompressedHamiltonian>,IQArray<Qubit>), QVoid> Body => (__in__) =>
        {
            var (data,register) = __in__;
#line 36 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            foreach (var index in new Range(0L, (data.Length - 1L)))
#line hidden
            {
#line 37 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
                ApplyTerm.Apply((index, data, register));
            }

#line hidden
            return QVoid.Instance;
        }

        ;
        public override Func<(IQArray<Qubit>,(IQArray<CompressedHamiltonian>,IQArray<Qubit>)), QVoid> ControlledBody => (__in__) =>
        {
            var (__controlQubits__,(data,register)) = __in__;
#line hidden
            foreach (var index in new Range(0L, (data.Length - 1L)))
#line hidden
            {
#line hidden
                ApplyTerm.Controlled.Apply((__controlQubits__, (index, data, register)));
            }

#line hidden
            return QVoid.Instance;
        }

        ;
        public override Func<(IQArray<CompressedHamiltonian>,IQArray<Qubit>), QVoid> AdjointBody => (__in__) =>
        {
            var (data,register) = __in__;
#line hidden
            foreach (var index in RangeReverse.Apply(new Range(0L, (data.Length - 1L))))
#line hidden
            {
#line hidden
                ApplyTerm.Adjoint.Apply((index, data, register));
            }

#line hidden
            return QVoid.Instance;
        }

        ;
        public override Func<(IQArray<Qubit>,(IQArray<CompressedHamiltonian>,IQArray<Qubit>)), QVoid> ControlledAdjointBody => (__in__) =>
        {
            var (__controlQubits__,(data,register)) = __in__;
#line hidden
            foreach (var index in RangeReverse.Apply(new Range(0L, (data.Length - 1L))))
#line hidden
            {
#line hidden
                ApplyTerm.Adjoint.Controlled.Apply((__controlQubits__, (index, data, register)));
            }

#line hidden
            return QVoid.Instance;
        }

        ;
        public override void Init()
        {
            this.ApplyTerm = this.Factory.Get<IUnitary<(Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>)>>(typeof(ApplyTerm));
            this.Length = this.Factory.Get<ICallable>(typeof(Microsoft.Quantum.Core.Length<>));
            this.RangeReverse = this.Factory.Get<ICallable<Range, Range>>(typeof(Microsoft.Quantum.Core.RangeReverse));
        }

        public override IApplyData __dataIn((IQArray<CompressedHamiltonian>,IQArray<Qubit>) data) => new In(data);
        public override IApplyData __dataOut(QVoid data) => data;
        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, IQArray<CompressedHamiltonian> data, IQArray<Qubit> register)
        {
            return __m__.Run<OracleFromJSON, (IQArray<CompressedHamiltonian>,IQArray<Qubit>), QVoid>((data, register));
        }
    }

    public partial class ApplyTerm : Unitary<(Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>)>, ICallable
    {
        public ApplyTerm(IOperationFactory m) : base(m)
        {
        }

        public class In : QTuple<(Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>)>, IApplyData
        {
            public In((Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>) data) : base(data)
            {
            }

            System.Collections.Generic.IEnumerable<Qubit> IApplyData.Qubits => ((IApplyData)Data.Item3)?.Qubits;
        }

        String ICallable.Name => "ApplyTerm";
        String ICallable.FullName => "ConvertFileToGates.ApplyTerm";
        protected ICallable<Int64, Pauli> FindPauli
        {
            get;
            set;
        }

        protected ICallable MicrosoftQuantumArraysMapped
        {
            get;
            set;
        }

        protected ICallable MicrosoftQuantumArraysSubarray
        {
            get;
            set;
        }

        protected IUnitary<(IQArray<Pauli>,Double,IQArray<Qubit>)> MicrosoftQuantumIntrinsicExp
        {
            get;
            set;
        }

        public override Func<(Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>), QVoid> Body => (__in__) =>
        {
            var (index,data,register) = __in__;
#line 51 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var gate_data = data[index];
#line 52 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var (name,angle,(control,target),ops) = ((String,Double,(IQArray<Int64>,IQArray<Int64>),IQArray<Int64>))gate_data.Data;
#line 53 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            MicrosoftQuantumIntrinsicExp.Apply((MicrosoftQuantumArraysMapped.Apply<IQArray<Pauli>>((FindPauli, ops)), angle, MicrosoftQuantumArraysSubarray.Apply<IQArray<Qubit>>((target, register))));
#line hidden
            return QVoid.Instance;
        }

        ;
        public override Func<(IQArray<Qubit>,(Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>)), QVoid> ControlledBody => (__in__) =>
        {
            var (__controlQubits__,(index,data,register)) = __in__;
#line hidden
            var gate_data = data[index];
#line hidden
            var (name,angle,(control,target),ops) = ((String,Double,(IQArray<Int64>,IQArray<Int64>),IQArray<Int64>))gate_data.Data;
#line hidden
            MicrosoftQuantumIntrinsicExp.Controlled.Apply((__controlQubits__, (MicrosoftQuantumArraysMapped.Apply<IQArray<Pauli>>((FindPauli, ops)), angle, MicrosoftQuantumArraysSubarray.Apply<IQArray<Qubit>>((target, register)))));
#line hidden
            return QVoid.Instance;
        }

        ;
        public override Func<(Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>), QVoid> AdjointBody => (__in__) =>
        {
            var (index,data,register) = __in__;
#line hidden
            var gate_data = data[index];
#line hidden
            var (name,angle,(control,target),ops) = ((String,Double,(IQArray<Int64>,IQArray<Int64>),IQArray<Int64>))gate_data.Data;
#line hidden
            MicrosoftQuantumIntrinsicExp.Adjoint.Apply((MicrosoftQuantumArraysMapped.Apply<IQArray<Pauli>>((FindPauli, ops)), angle, MicrosoftQuantumArraysSubarray.Apply<IQArray<Qubit>>((target, register))));
#line hidden
            return QVoid.Instance;
        }

        ;
        public override Func<(IQArray<Qubit>,(Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>)), QVoid> ControlledAdjointBody => (__in__) =>
        {
            var (__controlQubits__,(index,data,register)) = __in__;
#line hidden
            var gate_data = data[index];
#line hidden
            var (name,angle,(control,target),ops) = ((String,Double,(IQArray<Int64>,IQArray<Int64>),IQArray<Int64>))gate_data.Data;
#line hidden
            MicrosoftQuantumIntrinsicExp.Adjoint.Controlled.Apply((__controlQubits__, (MicrosoftQuantumArraysMapped.Apply<IQArray<Pauli>>((FindPauli, ops)), angle, MicrosoftQuantumArraysSubarray.Apply<IQArray<Qubit>>((target, register)))));
#line hidden
            return QVoid.Instance;
        }

        ;
        public override void Init()
        {
            this.FindPauli = this.Factory.Get<ICallable<Int64, Pauli>>(typeof(FindPauli));
            this.MicrosoftQuantumArraysMapped = this.Factory.Get<ICallable>(typeof(Microsoft.Quantum.Arrays.Mapped<,>));
            this.MicrosoftQuantumArraysSubarray = this.Factory.Get<ICallable>(typeof(Microsoft.Quantum.Arrays.Subarray<>));
            this.MicrosoftQuantumIntrinsicExp = this.Factory.Get<IUnitary<(IQArray<Pauli>,Double,IQArray<Qubit>)>>(typeof(Microsoft.Quantum.Intrinsic.Exp));
        }

        public override IApplyData __dataIn((Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>) data) => new In(data);
        public override IApplyData __dataOut(QVoid data) => data;
        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, Int64 index, IQArray<CompressedHamiltonian> data, IQArray<Qubit> register)
        {
            return __m__.Run<ApplyTerm, (Int64,IQArray<CompressedHamiltonian>,IQArray<Qubit>), QVoid>((index, data, register));
        }
    }

    public partial class GetEnergyByTrotterization : Operation<(CompleteHamiltonian,Int64), (Double,Double)>, ICallable
    {
        public GetEnergyByTrotterization(IOperationFactory m) : base(m)
        {
        }

        public class In : QTuple<(CompleteHamiltonian,Int64)>, IApplyData
        {
            public In((CompleteHamiltonian,Int64) data) : base(data)
            {
            }

            System.Collections.Generic.IEnumerable<Qubit> IApplyData.Qubits => null;
        }

        public class Out : QTuple<(Double,Double)>, IApplyData
        {
            public Out((Double,Double) data) : base(data)
            {
            }

            System.Collections.Generic.IEnumerable<Qubit> IApplyData.Qubits => null;
        }

        String ICallable.Name => "GetEnergyByTrotterization";
        String ICallable.FullName => "ConvertFileToGates.GetEnergyByTrotterization";
        protected IUnitary<(IQArray<CompressedHamiltonian>,IQArray<Qubit>)> OracleFromJSON
        {
            get;
            set;
        }

        protected ICallable<(Int64,Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>), Double> MicrosoftQuantumCharacterizationRobustPhaseEstimation
        {
            get;
            set;
        }

        protected ICallable<((Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),IQArray<Qubit>), QVoid> MicrosoftQuantumChemistryJordanWignerPrepareTrialState
        {
            get;
            set;
        }

        protected ICallable<(Int64,ICallable,IUnitary,ICallable), Double> MicrosoftQuantumSimulationEstimateEnergy
        {
            get;
            set;
        }

        public override Func<(CompleteHamiltonian,Int64), (Double,Double)> Body => (__in__) =>
        {
            var (data,nBitsPrecision) = __in__;
#line 63 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var (constants,initialState,termData) = ((CompressedConstants,(Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),IQArray<CompressedHamiltonian>))data.Data;
#line 64 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var (nSpinOrbitals,energyOffset,trotterStepSize,trotterOrder) = constants.Data;
#line 68 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var rescaleFactor = (1D / trotterStepSize);
#line 69 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var oracle = OracleFromJSON.Partial(new Func<IQArray<Qubit>, (IQArray<CompressedHamiltonian>,IQArray<Qubit>)>((__arg1__) => (termData, __arg1__)));
#line 73 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var statePrep = MicrosoftQuantumChemistryJordanWignerPrepareTrialState.Partial(new Func<IQArray<Qubit>, ((Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),IQArray<Qubit>)>((__arg2__) => (initialState, __arg2__)));
#line 77 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var phaseEstAlgorithm = MicrosoftQuantumCharacterizationRobustPhaseEstimation.Partial(new Func<(Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>), (Int64,Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>)>((__arg3__) => (nBitsPrecision, __arg3__.Item1, __arg3__.Item2)));
#line 80 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var estPhase = MicrosoftQuantumSimulationEstimateEnergy.Apply((nSpinOrbitals, statePrep, oracle, phaseEstAlgorithm));
#line 85 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            var estEnergy = ((estPhase * rescaleFactor) + energyOffset);
#line 88 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ConvertFileToGates/Operations.qs"
            return (estPhase, estEnergy);
        }

        ;
        public override void Init()
        {
            this.OracleFromJSON = this.Factory.Get<IUnitary<(IQArray<CompressedHamiltonian>,IQArray<Qubit>)>>(typeof(OracleFromJSON));
            this.MicrosoftQuantumCharacterizationRobustPhaseEstimation = this.Factory.Get<ICallable<(Int64,Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>), Double>>(typeof(Microsoft.Quantum.Characterization.RobustPhaseEstimation));
            this.MicrosoftQuantumChemistryJordanWignerPrepareTrialState = this.Factory.Get<ICallable<((Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),IQArray<Qubit>), QVoid>>(typeof(Microsoft.Quantum.Chemistry.JordanWigner.PrepareTrialState));
            this.MicrosoftQuantumSimulationEstimateEnergy = this.Factory.Get<ICallable<(Int64,ICallable,IUnitary,ICallable), Double>>(typeof(Microsoft.Quantum.Simulation.EstimateEnergy));
        }

        public override IApplyData __dataIn((CompleteHamiltonian,Int64) data) => new In(data);
        public override IApplyData __dataOut((Double,Double) data) => new Out(data);
        public static System.Threading.Tasks.Task<(Double,Double)> Run(IOperationFactory __m__, CompleteHamiltonian data, Int64 nBitsPrecision)
        {
            return __m__.Run<GetEnergyByTrotterization, (CompleteHamiltonian,Int64), (Double,Double)>((data, nBitsPrecision));
        }
    }
}