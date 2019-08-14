# Hamiltonian Term Extraction

## Written by Christopher Kang, Mentor Sriram Krishnamoorthy @ PNNL, Components from Microsoft's Q# Samples

## Usage

In order to extract the Hamiltonian terms in the supported JSON format, use the `extract_gates.sh` script. This will first run Q# to obtain the gates / state prep data, then write the gates and state prep data to file. A Python script then converts this to the final JSON.

## Components

In order to support the pipeline, a number of new components had to be written and integrated:

- A modified Q#/.NET project to dump the raw gates and other metadata
- Python file (`produceJSONV2.py`) to convert the dumped information into the standardized JSON format
