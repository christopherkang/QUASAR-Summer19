# Pipeline Test Utilities

## Christopher Kang

## Purpose

This code helps to automate the testing process and ensure that the following components all work:

- Hamiltonian to JSON
- Term optimization
- JSON ingestion

## Instructions for Use

Step 1. Clone the QuantumLibraries repo and replace the `JordanWignerEvolutionSet.qs` with the provided file. Then, modify the .csproj reference (see the README in `ExtractTrotterGates`).

Step 2. Ensure the proper optimizations are in OptimizeCircuit. 

Step 3. Modify the constants in `testPipeline.sh` in this directory. These should correspond with the Hamiltonians to be run.

Step 4. Run the shell script. It will output two text files in _temp: `_sampled_optimized_energy.txt` and `_sampled_reference_energy.txt`. The optimized energy will have the energy estimates after optimizations, while the reference energy will have energy estimates before optimizations. Compare the mean values to ensure that the accuracy has been maintained.
