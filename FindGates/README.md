# Using the Gate Extraction Tool

## Authored by Christopher Kang under Sriram Krishnamoorthy, PNNL

## How to use this tool

This tool allows you to dump the exact gates applied in the Trotterization library. Follow these steps:

1. Clone the [QuantumLibraries](https://github.com/microsoft/QuantumLibraries) repo
2. Replace the **QuantumLibraries/Chemistry/src/Runtime/JordanWigner/JordanWignerEvolutionSet.qs** file with the provided file
3. Replace the **QuantumLibraries/Standard/src/Simulation/Algorithms.qs** file with the provided file
4. Replace the **QuantumLibraries/Standard/src/Standard.csproj** file with the provided file. Note - if you'd like a QDK version other than 0.7.1905.3109, you'll need to edit this .csproj file.
5. Edit your project's .csproj file to reference this library. Replace the reference to the Q# chem library with three project references:
    - \<ProjectReference Include="**[PATH]**/QuantumLibraries/Standard/src/Standard.csproj" />
    - \<ProjectReference Include="**[PATH]**/QuantumLibraries/Chemistry/src/Runtime/Runtime.csproj" />
    - \<ProjectReference Include="**[PATH]**/QuantumLibraries/Chemistry/src/DataModel/DataModel.csproj" />
6. Your code should now already be using the modified libraries and will print the additional information to the console. To collect this data, run from the command line:
    - dotnet run > test_gates.txt
    - This stores the output in a txt file.

## WARNING

If you proceed with this modification, DO NOT REFERENCE THE LOCAL CHEM LIBRARY! It will NOT apply gates to qubits, and your results will be incorrect.
