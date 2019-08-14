# Optimized Hamiltonian Ingestion

## Written by Christopher Kang, Mentor Sriram Krishnamoorthy @ PNNL, Components from Microsoft's Q# Samples

## Usage

In order to important the optimized Hamiltonian, run the .NET project and pass the JSON path, number of samples, and bits of precision as command line arguments. The project will then output the discovered energy levels of the Hamiltonians, alongside an average energy estimate. This supports Fermionic SWAPs, alongside other types of Jordan Wigner terms.

## Components

In the production of this ingestion pipeline, additional components needed to be added

- Contributed the Fermionic SWAPs to Microsoft's QuantumLibraries repo
- Reused the GeneratorIndex type for ingestion
- Connected the work into the existing JordanWignerEncodingSet in the Quantum Chemistry library
