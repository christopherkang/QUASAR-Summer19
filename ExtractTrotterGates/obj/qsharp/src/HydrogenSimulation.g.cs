#pragma warning disable 1591
using System;
using Microsoft.Quantum.Core;
using Microsoft.Quantum.Intrinsic;
using Microsoft.Quantum.Simulation.Core;

[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"GetEnergyByTrotterization\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs\",\"Position\":{\"Item1\":33,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":36}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"qSharpData\"]},\"Type\":{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":51},\"Item2\":{\"Line\":1,\"Column\":75}}]}}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":38},\"Item2\":{\"Line\":1,\"Column\":48}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"nBitsPrecision\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":77},\"Item2\":{\"Line\":1,\"Column\":91}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"trotterStepSize\"]},\"Type\":{\"Case\":\"Double\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":99},\"Item2\":{\"Line\":1,\"Column\":114}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"trotterOrder\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":125},\"Item2\":{\"Line\":1,\"Column\":137}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Null\"}}]},{\"Case\":\"Int\"},{\"Case\":\"Double\"},{\"Case\":\"Int\"}]]},\"ReturnType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"Double\"},{\"Case\":\"Double\"}]]},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"GetEnergyByTrotterization\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs\",\"Position\":{\"Item1\":33,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":36}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"TargetedGateExtraction\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs\",\"Position\":{\"Item1\":63,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":33}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"qSharpData\"]},\"Type\":{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":48},\"Item2\":{\"Line\":1,\"Column\":72}}]}}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":35},\"Item2\":{\"Line\":1,\"Column\":45}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"trotterStepSize\"]},\"Type\":{\"Case\":\"Double\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":74},\"Item2\":{\"Line\":1,\"Column\":89}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"trotterOrder\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":100},\"Item2\":{\"Line\":1,\"Column\":112}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Null\"}}]},{\"Case\":\"Double\"},{\"Case\":\"Int\"}]]},\"ReturnType\":{\"Case\":\"UnitType\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"TargetedGateExtraction\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs\",\"Position\":{\"Item1\":63,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":33}},\"Documentation\":[]}")]
[assembly: CallableDeclaration("{\"Kind\":{\"Case\":\"Operation\"},\"QualifiedName\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"GetEnergyByQubitization\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs\",\"Position\":{\"Item1\":85,\"Item2\":4},\"SymbolRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":34}},\"ArgumentTuple\":{\"Case\":\"QsTuple\",\"Fields\":[[{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"qSharpData\"]},\"Type\":{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Value\",\"Fields\":[{\"Item1\":{\"Line\":1,\"Column\":49},\"Item2\":{\"Line\":1,\"Column\":73}}]}}]},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":36},\"Item2\":{\"Line\":1,\"Column\":46}}}]},{\"Case\":\"QsTupleItem\",\"Fields\":[{\"VariableName\":{\"Case\":\"ValidName\",\"Fields\":[\"nBitsPrecision\"]},\"Type\":{\"Case\":\"Int\"},\"InferredInformation\":{\"IsMutable\":false,\"HasLocalQuantumDependency\":false},\"Position\":{\"Case\":\"Null\"},\"Range\":{\"Item1\":{\"Line\":1,\"Column\":75},\"Item2\":{\"Line\":1,\"Column\":89}}}]}]]},\"Signature\":{\"TypeParameters\":[],\"ArgumentType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"UserDefinedType\",\"Fields\":[{\"Namespace\":\"Microsoft.Quantum.Chemistry.JordanWigner\",\"Name\":\"JordanWignerEncodingData\",\"Range\":{\"Case\":\"Null\"}}]},{\"Case\":\"Int\"}]]},\"ReturnType\":{\"Case\":\"TupleType\",\"Fields\":[[{\"Case\":\"Double\"},{\"Case\":\"Double\"}]]},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}}},\"Documentation\":[]}")]
[assembly: SpecializationDeclaration("{\"Kind\":{\"Case\":\"QsBody\"},\"TypeArguments\":{\"Case\":\"Null\"},\"Information\":{\"Affiliation\":{\"Case\":\"EmptySet\"},\"InferredInformation\":{\"IsSelfAdjoint\":false,\"IsIntrinsic\":false}},\"Parent\":{\"Namespace\":\"Microsoft.Quantum.Chemistry.Samples.Hydrogen\",\"Name\":\"GetEnergyByQubitization\"},\"SourceFile\":\"/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs\",\"Position\":{\"Item1\":85,\"Item2\":4},\"HeaderRange\":{\"Item1\":{\"Line\":1,\"Column\":11},\"Item2\":{\"Line\":1,\"Column\":34}},\"Documentation\":[]}")]
#line hidden
namespace Microsoft.Quantum.Chemistry.Samples.Hydrogen
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
        String ICallable.FullName => "Microsoft.Quantum.Chemistry.Samples.Hydrogen.GetEnergyByTrotterization";
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
#line 38 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var (nSpinOrbitals,fermionTermData,statePrepData,energyOffset) = ((Int64,Microsoft.Quantum.Chemistry.JordanWigner.JWOptimizedHTerms,(Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),Double))qSharpData.Data;
#line 42 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var (nQubits,(rescaleFactor,oracle)) = MicrosoftQuantumChemistryJordanWignerTrotterStepOracle.Apply((qSharpData, trotterStepSize, trotterOrder));
#line 46 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var statePrep = MicrosoftQuantumChemistryJordanWignerPrepareTrialState.Partial(new Func<IQArray<Qubit>, ((Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),IQArray<Qubit>)>((__arg1__) => (statePrepData, __arg1__)));
#line 50 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var phaseEstAlgorithm = MicrosoftQuantumCharacterizationRobustPhaseEstimation.Partial(new Func<(Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>), (Int64,Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>)>((__arg2__) => (nBitsPrecision, __arg2__.Item1, __arg2__.Item2)));
#line 53 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var estPhase = MicrosoftQuantumSimulationEstimateEnergy.Apply((nQubits, statePrep, oracle, phaseEstAlgorithm));
#line 58 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var estEnergy = ((estPhase * rescaleFactor) + energyOffset);
#line 61 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
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

    public partial class TargetedGateExtraction : Operation<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64), QVoid>, ICallable
    {
        public TargetedGateExtraction(IOperationFactory m) : base(m)
        {
        }

        public class In : QTuple<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64)>, IApplyData
        {
            public In((Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64) data) : base(data)
            {
            }

            System.Collections.Generic.IEnumerable<Qubit> IApplyData.Qubits => null;
        }

        String ICallable.Name => "TargetedGateExtraction";
        String ICallable.FullName => "Microsoft.Quantum.Chemistry.Samples.Hydrogen.TargetedGateExtraction";
        protected ICallable<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64), (Int64,(Double,IUnitary))> MicrosoftQuantumChemistryJordanWignerTrotterStepOracle
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

        protected ICallable<IQArray<Qubit>, QVoid> MicrosoftQuantumIntrinsicResetAll
        {
            get;
            set;
        }

        public override Func<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64), QVoid> Body => (__in__) =>
        {
            var (qSharpData,trotterStepSize,trotterOrder) = __in__;
#line 68 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var (nSpinOrbitals,fermionTermData,statePrepData,energyOffset) = ((Int64,Microsoft.Quantum.Chemistry.JordanWigner.JWOptimizedHTerms,(Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),Double))qSharpData.Data;
#line 69 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var (nQubits,(rescaleFactor,oracle)) = MicrosoftQuantumChemistryJordanWignerTrotterStepOracle.Apply((qSharpData, trotterStepSize, trotterOrder));
#line hidden
            {
#line 71 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
                var register = Allocate.Apply(nQubits);
#line hidden
                Exception __arg1__ = null;
                try
                {
#line 72 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
                    oracle.Apply(register);
#line 73 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
                    MicrosoftQuantumIntrinsicResetAll.Apply(register);
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
                    Release.Apply(register);
                }
            }

#line hidden
            return QVoid.Instance;
        }

        ;
        public override void Init()
        {
            this.MicrosoftQuantumChemistryJordanWignerTrotterStepOracle = this.Factory.Get<ICallable<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64), (Int64,(Double,IUnitary))>>(typeof(Microsoft.Quantum.Chemistry.JordanWigner.TrotterStepOracle));
            this.Allocate = this.Factory.Get<Allocate>(typeof(Microsoft.Quantum.Intrinsic.Allocate));
            this.Release = this.Factory.Get<Release>(typeof(Microsoft.Quantum.Intrinsic.Release));
            this.MicrosoftQuantumIntrinsicResetAll = this.Factory.Get<ICallable<IQArray<Qubit>, QVoid>>(typeof(Microsoft.Quantum.Intrinsic.ResetAll));
        }

        public override IApplyData __dataIn((Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64) data) => new In(data);
        public override IApplyData __dataOut(QVoid data) => data;
        public static System.Threading.Tasks.Task<QVoid> Run(IOperationFactory __m__, Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData qSharpData, Double trotterStepSize, Int64 trotterOrder)
        {
            return __m__.Run<TargetedGateExtraction, (Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Double,Int64), QVoid>((qSharpData, trotterStepSize, trotterOrder));
        }
    }

    public partial class GetEnergyByQubitization : Operation<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64), (Double,Double)>, ICallable
    {
        public GetEnergyByQubitization(IOperationFactory m) : base(m)
        {
        }

        public class In : QTuple<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64)>, IApplyData
        {
            public In((Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64) data) : base(data)
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

        String ICallable.Name => "GetEnergyByQubitization";
        String ICallable.FullName => "Microsoft.Quantum.Chemistry.Samples.Hydrogen.GetEnergyByQubitization";
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

        protected ICallable<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData, (Int64,(Double,IUnitary))> MicrosoftQuantumChemistryJordanWignerQubitizationOracle
        {
            get;
            set;
        }

        protected ICallable<Double, Double> MicrosoftQuantumMathSin
        {
            get;
            set;
        }

        protected ICallable<(Int64,ICallable,IUnitary,ICallable), Double> MicrosoftQuantumSimulationEstimateEnergy
        {
            get;
            set;
        }

        public override Func<(Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64), (Double,Double)> Body => (__in__) =>
        {
            var (qSharpData,nBitsPrecision) = __in__;
#line 90 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var (nSpinOrbitals,fermionTermData,statePrepData,energyOffset) = ((Int64,Microsoft.Quantum.Chemistry.JordanWigner.JWOptimizedHTerms,(Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),Double))qSharpData.Data;
#line 94 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var (nQubits,(oneNorm,oracle)) = MicrosoftQuantumChemistryJordanWignerQubitizationOracle.Apply(qSharpData);
#line 98 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var statePrep = MicrosoftQuantumChemistryJordanWignerPrepareTrialState.Partial(new Func<IQArray<Qubit>, ((Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),IQArray<Qubit>)>((__arg1__) => (statePrepData, __arg1__)));
#line 102 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var phaseEstAlgorithm = MicrosoftQuantumCharacterizationRobustPhaseEstimation.Partial(new Func<(Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>), (Int64,Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>)>((__arg2__) => (nBitsPrecision, __arg2__.Item1, __arg2__.Item2)));
#line 105 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var estPhase = MicrosoftQuantumSimulationEstimateEnergy.Apply((nQubits, statePrep, oracle, phaseEstAlgorithm));
#line 113 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            var estEnergy = ((MicrosoftQuantumMathSin.Apply(estPhase) * oneNorm) + energyOffset);
#line 116 "/Users/kang828/Documents/GitHub/QUASAR-Summer19/ExtractTrotterGates/HydrogenSimulation.qs"
            return (estPhase, estEnergy);
        }

        ;
        public override void Init()
        {
            this.MicrosoftQuantumCharacterizationRobustPhaseEstimation = this.Factory.Get<ICallable<(Int64,Microsoft.Quantum.Oracles.DiscreteOracle,IQArray<Qubit>), Double>>(typeof(Microsoft.Quantum.Characterization.RobustPhaseEstimation));
            this.MicrosoftQuantumChemistryJordanWignerPrepareTrialState = this.Factory.Get<ICallable<((Int64,IQArray<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerInputState>),IQArray<Qubit>), QVoid>>(typeof(Microsoft.Quantum.Chemistry.JordanWigner.PrepareTrialState));
            this.MicrosoftQuantumChemistryJordanWignerQubitizationOracle = this.Factory.Get<ICallable<Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData, (Int64,(Double,IUnitary))>>(typeof(Microsoft.Quantum.Chemistry.JordanWigner.QubitizationOracle));
            this.MicrosoftQuantumMathSin = this.Factory.Get<ICallable<Double, Double>>(typeof(Microsoft.Quantum.Math.Sin));
            this.MicrosoftQuantumSimulationEstimateEnergy = this.Factory.Get<ICallable<(Int64,ICallable,IUnitary,ICallable), Double>>(typeof(Microsoft.Quantum.Simulation.EstimateEnergy));
        }

        public override IApplyData __dataIn((Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64) data) => new In(data);
        public override IApplyData __dataOut((Double,Double) data) => new Out(data);
        public static System.Threading.Tasks.Task<(Double,Double)> Run(IOperationFactory __m__, Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData qSharpData, Int64 nBitsPrecision)
        {
            return __m__.Run<GetEnergyByQubitization, (Microsoft.Quantum.Chemistry.JordanWigner.JordanWignerEncodingData,Int64), (Double,Double)>((qSharpData, nBitsPrecision));
        }
    }
}