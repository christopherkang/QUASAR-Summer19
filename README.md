# QUASAR-Summer19

Collection of summer projects for my internship at PNNL over the summer of 2019.
Warning: This work may need to be kept private and/or scrubbed before use, as it contains PNNL sensitive information (network username).

## Using the Pipeline

1. Clone this repo. If you want to use Microsoft's YAML files, also clone [this folder](https://github.com/microsoft/Quantum/tree/master/Chemistry/IntegralData/YAML) in Microsoft/Quantum
2. Install all Python requirements with the `spec.yml` file provided.
3. Install [Microsoft's QDK](https://docs.microsoft.com/en-us/quantum/install-guide/csharp?view=qsharp-preview)
4. Go to /TestPipeline/testPipeline.sh, and edit the top of the script so it has the correct parameters.
5. Run the script - you may need to give proper permissions. [Additionally, if you are Windows, you may need to run the commands in the shell script manually.]
6. Find results in /TestPipeline/_temp

## Components

### 1ProduceSampleEnergy

Input: YAML file

Output: Energy level estimates

This folder has a C#/Q# project which serves as the control estimates for the energy level via Trotterization. There are no additional optimizations applied, and the code is almost entirely taken from the sample provided by Microsoft.

### 2ExtractTrotterGates

Input: YAML file

Output: JSON file

This folder contains a C#/Q# file to produce the necessary Trotter gates and a Python script to convert the output to JSON (`produceJSON.py`). See the `README.md` for requirements.

### 3OptimizeCircuit

Input: JSON file

Output: JSON file

This folder applies a variety of optimizations onto the Trotter gate set. These optimizations may be described in Khan, et. al.

### 4ImportOptimizedFermions

Input: Optimized JSON file

Output: Energy level estimates

This folder contains a C#/Q# file to ingest the optimizations and produce energy level estimates from it.

### 5ValidationTests

Input: All aspects of the pipeline

Output: Pytest output

This folder contains a series of validation tests for the pipeline's output and unit tests for the pipeline's components. The following outputs are checked in the validation testing:

- Extracted interaction JSON
- Interaction / swap optimizations
- Optimized JSON

### Format

This folder describes the format specification of the intermediate JSON file.

### TestPipeline

Input: YAML file, parameters

Output:

- Two text files comparing energy level estimates with/wo optimizations (`_sampled_reference_energy.txt`, `_sampled_optimized_energy.txt`)
- Two CSVs with resource estimates (`_costEstimateReference.csv`, `_costEstimateOptimized.csv`)
- The raw extracted Hamiltonian (`extracted_terms.json`)
- The optimizations to be applied (`interaction_file.txt`)
- The optimized JSON (`reconstructed.json`)
- The logfile describing all key parameters (`logfile.txt`)

This folder has a shell script which executes the other folders in the following order:

1. `ProduceSampleEnergy`, creating a sample energy level estimate
2. `ExtractTrotterGates`, providing a JSON file describing the Trotter steps
3. `OptimizeCircuit`, applying any optimizations selected on the JSON file
4. `ConvertFileToGates`, reingesting the optimized JSON to provide an energy level estimate
5. `ValidationTests`, checking all outputs and the validity of the pipeline components

Two files, `_sampled_optimized_energy.txt` and `_sampled_reference_energy.txt` are outputted in the `./_temp` folder, where the outputs with/wo optimizations can be compared.
