#!/bin/bash
# Test pipeline components
# Assumptions - placed in cloned Github directory with file structure intact

# STEP 0 - Setup

mkdir _temp

YAML_PATH="/Users/kang828/Documents/GitHub/Quantum/Chemistry/IntegralData/YAML/H4/h4_sto6g_0.100.yaml"
# YAML_PATH="/Users/kang828/Documents/GitHub/Quantum/Chemistry/IntegralData/Broombridge_v0.2/H2_sto-3g.yaml"
INPUT_STATE="E1"
PRECISION="7"
TROTTER_STEP="0.4"
TROTTER_ORDER="1"
SAMPLE_SIZE="1"

CMD_ARGS="$YAML_PATH $INPUT_STATE $PRECISION $TROTTER_STEP $TROTTER_ORDER"

echo "Reading YAML at $YAML_PATH and state $INPUT_STATE"
echo "Using $PRECISION bits of precision, stepsize=$TROTTER_STEP, order=$TROTTER_ORDER"
echo "Sample size = $SAMPLE_SIZE"
read -p "Confirm? [Y/n] " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    echo "Abbreviated prompts enabled."
    # do dangerous stuff
    mkdir "_temp"

    # ----- STEP 1 - Produce a sample energy estimate
    cd ../1ProduceSampleEnergy

    # arguments: path, state label, precision, step size, order, number of samples
    echo "RUNNING: dotnet run $CMD_ARGS $SAMPLE_SIZE >./_temp/_sampled_reference_energy.txt"
    dotnet run $CMD_ARGS $SAMPLE_SIZE >../TestPipeline/_temp/_sampled_reference_energy.txt

    # ----- STEP 2 - Produce the JSON file
    echo "RUNNING: ./extract_gates.sh $CMD_ARGS"

    cd ../2ExtractTrotterGates
    mkdir _temp
    dotnet run $CMD_ARGS > ./_temp/log.txt
    python3 produceJSONV2.py ./_temp -o extracted_terms.json
    cp extracted_terms.json ../TestPipeline/_temp
    echo Finished term extraction.
    echo JSON at $directory/extracted_terms.json 

    # ----- STEP 3 - (optional) Process the terms
    echo "Begin processing terms"
    cd ../3OptimizeCircuit/swap

    python3 QMap.py ../../TestPipeline/_temp/extracted_terms.json 2 1D > interaction_file.txt
    cp interaction_file.txt ../../TestPipeline/_temp/

    cd ..
    python3 outputToJSONV2.py ../TestPipeline/_temp/extracted_terms.json ../TestPipeline/_temp/interaction_file.txt
    cp ./reconstructed.json ../TestPipeline/_temp/

    cd .. 
    # in QUASAR

    # ----- STEP 4 - Ingest the JSON file
    echo "Begin ingesting the JSON file"
    cd ./4ImportOptimizedFermions 
    echo "YAML Path: $YAML_PATH" > ../TestPipeline/_temp/_sampled_optimized_energy.txt
    echo "RUNNING: dotnet run ./extracted_terms.json $SAMPLE_SIZE >./_temp/_sampled_optimized_energy.txt"
    dotnet run ../TestPipeline/_temp/reconstructed.json $SAMPLE_SIZE $PRECISION >>../TestPipeline/_temp/_sampled_optimized_energy.txt
else
    echo Exiting from user cancellation.
fi
