# QUASAR-Summer19

Collection of summer projects for my internship at PNNL over the summer of 2019.
Warning: This work may need to be kept private and/or scrubbed before use, as it contains PNNL sensitive information (network username).

## Components

### ConvertFileToGates

Input: JSON file (potentially optimized)

Output: Energy level estimate

This C#/Q# project converts the created JSON format into an energy level estimate. In the pipeline, it is used after optimizations to produce an updated energy level estimate.

### ExtractTrotterGates

Input: YAML file

Output: JSON file

This folder contains a C#/Q# file to produce the necessary Trotter gates and a Python script to convert the output to JSON (`produceJSON.py`). See the `README.md` for requirements.

### Format

This folder describes the format specification of the intermediate JSON file.

### OptimizeCircuit

Input: JSON file

Output: JSON file

This folder applies a variety of optimizations onto the Trotter gate set. These optimizations may be described in Khan, et. al.

### ProduceSampleEnergy

Input: YAML file

Output: Energy level estimates

This folder has a C#/Q# project which serves as the control estimates for the energy level via Trotterization. There are no additional optimizations applied, and the code is almost entirely taken from the sample provided by Microsoft.

### TestPipeline

Input: YAML file, parameters

Output: Two text files comparing energy level estimates with/wo optimizations

This folder has a shell script which executes the other folders in the following order:

1. `ProduceSampleEnergy`, creating a sample energy level estimate
2. `ExtractTrotterGates`, providing a JSON file describing the Trotter steps
3. `OptimizeCircuit`, applying any optimizations selected on the JSON file
4. `ConvertFileToGates`, reingesting the optimized JSON to provide an energy level estimate

Two files, `_sampled_optimized_energy.txt` and `_sampled_reference_energy.txt` are outputted in the `./_temp` folder, where the outputs with/wo optimizations can be compared.

### UnitTests (_In Progress_)

This folder will contain unit tests in the future.
