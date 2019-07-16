#pragma warning disable 1591
using System;
using Microsoft.Quantum.Core;
using Microsoft.Quantum.Intrinsic;
using Microsoft.Quantum.Simulation.Core;

[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"ProduceSampleEnergy\",\"Name\":\"GetEnergyByTrotterization\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ProduceSampleEnergy/Operations.qs\",\"Position\":{\"Item1\":10,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":36}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"qSharpData\"]},\"Type\":{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":51},\"Item2\":{\"Line\":1,\"Column\":75}}]}}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":38},\"Item2\":{\"Line\":1,\"Column\":48}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"nBitsPrecision\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":77},\"Item2\":{\"Line\":1,\"Column\":91}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"trotterStepSize\"]},\"Type\":{\"Case\":\"Double\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":99},\"Item2\":{\"Line\":1,\"Column\":114}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"trotterOrder\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":125},\"Item2\":{\"Line\":1,\"Column\":137}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Null\"}}]},{\"Case\":\"Int\"},{\"Case\":\"Double\"},{\"Case\":\"Int\"}]]},\"ReturnType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"Double\"},{\"Case\":\"Double\"}]]},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"ProduceSampleEnergy\",\"Name\":\"GetEnergyByTrotterization\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ProduceSampleEnergy/Operations.qs\",\"Position\":{\"Item1\":10,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":36}},\"Documentation\":[]}")]
#line hidden
namespace ProduceSampleEnergy
{
    public partial class GetEnergyByTrotterization : Operation<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64,Double,Int64), (Double,Double)>, ICallable
    {
        public GetEnergyByTrotterization(IOperationFactory m) : base(m)
        {
        }

        public class In : QTuple<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64,Double,Int64)>, IApplyData
        {
            public In((Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64,Double,Int64) data) : base(data)
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
        String ICallable.FullName => "ProduceSampleEnergy.GetEnergyByTrotterization";
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

        protected ICallable<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64), (Int64,(Double,IUnitary))> MicrosoftQuantumChemistryJordanWignerTrotterStepOracle
        {
            get;
            set;
        }

        protected ICallable<(Int64,ICallable,IUnitary,ICallable), Double> MicrosoftQuantumSimulationEstimateEnergy
        {
            get;
            set;
        }

        public override Func<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64,Double,Int64), (Double,Double)> Body => (__in__) =>
        {
            var (qSharpData,nBitsPrecision,trotterStepSize,trotterOrder) = __in__;
#line 15 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ProduceSampleEnergy/Operations.qs"
            var (nSpinOrbitals,fermionTermData,statePrepData,energyOffset) = ((Int64,Microsoft.Quantum.Chemistry.JordanWigner.JWOptimizedHTerms,(Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),Double))qSharpData.Data;
#line 19 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ProduceSampleEnergy/Operations.qs"
            var (nQubits,(rescaleFactor,oracle)) = MicrosoftQuantumChemistryJordanWignerTrotterStepOracle.Apply((qSharpData, trotterStepSize, trotterOrder));
#line 23 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ProduceSampleEnergy/Operations.qs"
            var statePrep = MicrosoftQuantumChemistryJordanWignerPrepareTrialState.Partial(new Func<IQArray<Qubit>, ((Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),IQArray<Qubit>)>((__arg1__) => (statePrepData, __arg1__)));
#line 27 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ProduceSampleEnergy/Operations.qs"
            var phaseEstAlgorithm = MicrosoftQuantumCharacterizationRobustPhaseEstimation.Partial(new Func<(Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>), (Int64,Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>)>((__arg2__) => (nBitsPrecision, __arg2__.Item1, __arg2__.Item2)));
#line 30 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ProduceSampleEnergy/Operations.qs"
            var estPhase = MicrosoftQuantumSimulationEstimateEnergy.Apply((nQubits, statePrep, oracle, phaseEstAlgorithm));
#line 35 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ProduceSampleEnergy/Operations.qs"
            var estEnergy = ((estPhase * rescaleFactor) + energyOffset);
#line 38 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ProduceSampleEnergy/Operations.qs"
            return (estPhase, estEnergy);
        }

        ;
        public override void Init()
        {
            this.MicrosoftQuantumCharacterizationRobustPhaseEstimation = this.Factory.Get<ICallable<(Int64,Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>), Double>>(typeof(Microsoft.Quantum.Characterization.RobustPhaseEstimation));
            this.MicrosoftQuantumChemistryJordanWignerPrepareTrialState = this.Factory.Get<ICallable<((Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),IQArray<Qubit>), QVoid>>(typeof(Microsoft.Quantum.Chemistry.JordanWigner.PrepareTrialState));
            this.MicrosoftQuantumChemistryJordanWignerTrotterStepOracle = this.Factory.Get<ICallable<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64), (Int64,(Double,IUnitary))>>(typeof(Microsoft.Quantum.Chemistry.JordanWigner.TrotterStepOracle));
            this.MicrosoftQuantumSimulationEstimateEnergy = this.Factory.Get<ICallable<(Int64,ICallable,IUnitary,ICallable), Double>>(typeof(Microsoft.Quantum.Simulation.EstimateEnergy));
        }

        public override IApplyData __dataIn((Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64,Double,Int64) data) => new In(data);
        public override IApplyData __dataOut((Double,Double) data) => new Out(data);
        public static System.Threading.Tasks.Task<(Double,Double)> Run(IOperationFactory __m__, Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData qSharpData, Int64 nBitsPrecision, Double trotterStepSize, Int64 trotterOrder)
        {
            return __m__.Run<GetEnergyByTrotterization, (Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64,Double,Int64), (Double,Double)>((qSharpData, nBitsPrecision, trotterStepSize, trotterOrder));
        }
    }
}