#!/bin/bash
# Test pipeline components
# Assumptions - placed in cloned Github directory with file structure intact

# STEP 0 - Setup

mkdir _temp

YAML_PATH = "/Users/kang828/Documents/GitHub/Quantum/Chemistry/IntegralData/YAML/H2O/h2o_oh0.7_sto3g.nw.out.yaml"
INPUT_STATE = "E1"
echo Reading YAML at $YAML_PATH and state $INPUT_STATE
read -p "Confirm? [Y/n] " -n 1 -r
echo 
if [[ $REPLY =~ ^[Yy]$ ]]
then
    # do dangerous stuff
    # STEP 1 - Produce a sample energy estimate
    cd ../ProduceSampleEnergy

    # arguments: path, state label, precision, step size, order, number of samples
    dotnet run $YAML_PATH E1 4 1.0 1 100 > ../TestPipeline/_temp/_sampled_reference_energy.txt 
    yes | ../ExtractTrotterGates/extract_gates.sh
    # STEP 2 - Produce the JSON file

    # STEP 3 - (optional) Process the terms

    # STEP 4 - Ingest the JSON file

    # STEP 5 - Return information to user
else
    echo Exiting. 
fi


