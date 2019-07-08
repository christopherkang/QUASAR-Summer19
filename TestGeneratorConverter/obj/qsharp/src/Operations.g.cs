#pragma warning disable 1591
using System;
using Microsoft.Quantum.Core;
using Microsoft.Quantum.Intrinsic;
using Microsoft.Quantum.Simulation.Core;

[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"HelloQ\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs\",\"Position\":{\"Item1\":11,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":17}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"UnitType\"},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"HelloQ\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs\",\"Position\":{\"Item1\":11,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":17}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"Unpack\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs\",\"Position\":{\"Item1\":15,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":17}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"qSharpData\"]},\"Type\":{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":32},\"Item2\":{\"Line\":1,\"Column\":56}}]}}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":19},\"Item2\":{\"Line\":1,\"Column\":29}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Null\"}}]},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"Unpack\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs\",\"Position\":{\"Item1\":15,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":17}},\"Documentation\":[]}")]
#line hidden
namespace Microsoft.Quantum.Chemistry.Samples.Hydrogen
{
    public partial class HelloQ : Operation<QVoid, QVoid>, ICallable
    {
        public HelloQ(IOperationFactory m) : base(m)
        {
        }

        String ICallable.Name => "HelloQ";
        String ICallable.FullName => "Microsoft.Quantum.Chemistry.Samples.Hydrogen.HelloQ";
        protected ICallable<String, QVoid> MicrosoftQuantumIntrinsicMessage
        {
            get;
            set;
        }

        public override Func<QVoid, QVoid> Body => (__in__) =>
        {
#line 13 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs"
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

    public partial class Unpack : Operation<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData, QVoid>, ICallable
    {
        public Unpack(IOperationFactory m) : base(m)
        {
        }

        String ICallable.Name => "Unpack";
        String ICallable.FullName => "Microsoft.Quantum.Chemistry.Samples.Hydrogen.Unpack";
        protected ICallable<Microsoft.Quantum.Chemistry.JordanWigner.JWOptimizedHTerms, Microsoft.Quantum.Simulation.GeneratorSystem> MicrosoftQuantumChemistryJordanWignerJordanWignerGeneratorSystem
        {
            get;
            set;
        }

        protected ICallable<String, QVoid> MicrosoftQuantumIntrinsicMessage
        {
            get;
            set;
        }

        public override Func<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData, QVoid> Body => (__in__) =>
        {
            var qSharpData = __in__;
#line 17 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs"
            var (nSpinOrbitals,fermionTermData,statePrepData,energyOffset) = ((Int64,Microsoft.Quantum.Chemistry.JordanWigner.JWOptimizedHTerms,(Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),Double))qSharpData.Data;
#line 18 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs"
            var generatorSystem = MicrosoftQuantumChemistryJordanWignerJordanWignerGeneratorSystem.Apply(fermionTermData);
#line 19 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs"
            var (upperBound,converter) = generatorSystem.Data;
#line 20 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs"
            foreach (var index in new Range(0L, (upperBound - 1L)))
#line hidden
            {
#line 21 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ReadGatesFile/Operations.qs"
                MicrosoftQuantumIntrinsicMessage.Apply(String.Format("{0}", converter.Apply<Microsoft.Quantum.Simulation.GeneratorIndex>(index)));
            }

#line hidden
            return QVoid.Instance;
        }

        ;
        public override void Init()
        {
            this.MicrosoftQuantumChemistryJordanWignerJordanWignerGeneratorSystem = this.Factory.Get<ICallable<Microsoft.Quantum.Chemistry.JordanWigner.JWOptimizedHTerms, Microsoft.Quantum.Simulation.GeneratorSystem>>(typeof(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerGeneratorSystem));
            this.MicrosoftQuantumIntrinsicMessage = this.Factory.Get<ICallable<String, QVoid>>(typeof(Microsoft.Quantum.Intrinsic.Message));
        }

        public override IApplyData __dataIn(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData data) => data;
        public override IApplyData __dataOut(QVoid data) => data;
        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData qSharpData)
        {
            return __m__.Run<Unpack, Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData, QVoid>(qSharpData);
        }
    }
}