namespace Microsoft.Quantum.Chemistry.Samples.Hydrogen
{
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Chemistry;
    open Microsoft.Quantum.Chemistry.JordanWigner;  
	open Microsoft.Quantum.Simulation;	
    open Microsoft.Quantum.Characterization;
    open Microsoft.Quantum.Convert;
    open Microsoft.Quantum.Math;

    operation HelloQ () : Unit {
        Message("Hello quantum world!");
    }

    operation Unpack (qSharpData : JordanWignerEncodingData) : Unit {
        let (nSpinOrbitals, fermionTermData, statePrepData, energyOffset) = qSharpData!;
        let generatorSystem = JordanWignerGeneratorSystem(fermionTermData);
        let (upperBound, converter) = generatorSystem!;
        for (index in 0..upperBound - 1) {
            Message($"{converter(index)}");
        }
    }
}
