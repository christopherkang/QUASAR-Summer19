# Hamiltonian Term Extraction

## Written by Christopher Kang, Mentor Sriram Krishnamoorthy @ PNNL, Components from Microsoft's Q# Samples

## Prerequisites

Ensure the modified `JordanWignerEvolutionSet.qs` file has overwritten the local copy, then ensure the `MolecularHydrogen.csproj` file is pointing to the local Q# Chem library implementation.

## Usage

In order to extract the Hamiltonian terms in the supported JSON format, first select the correct YAML file to be used in the `Program.cs` file. Then, run the `extract_gates.sh` shell script. The script will again prompt you to ensure you have the correct YAML file, and then run.

## Components

In order to support the pipeline, a number of new components had to be written and integrated:

  - A modified Q#/.NET project to dump the raw gates and other metadata
  - Rewritten `JordanWignerEvolutionSet.qs` to extract the terms
  - Python file to convert the dumped information into the standardized JSON format
