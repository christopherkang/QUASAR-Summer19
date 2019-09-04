#!/bin/bash
# Test pipeline components
# Assumptions - placed in cloned Github directory with file structure intact

# STEP 0 - Setup
# YAML_PATH="/Users/kang828/Documents/GitHub/Quantum/Chemistry/IntegralData/YAML/H4/h4_sto6g_0.100.yaml"
# YAML_PATH="/Users/kang828/Documents/GitHub/Quantum/Chemistry/IntegralData/Broombridge_v0.2/H2_sto-3g.yaml"
YAML_PATH="/Users/kang828/Documents/GitHub/Quantum/Chemistry/IntegralData/YAML/LiH_sto3g_FCI/lih_sto-3g_fci_0.800.yaml"
INPUT_STATE="E1"
PRECISION="7"
TROTTER_STEP="0.4"
TROTTER_ORDER="1"
SAMPLE_SIZE="1"

CMD_ARGS="$YAML_PATH $INPUT_STATE $PRECISION $TROTTER_STEP $TROTTER_ORDER"

echo
echo "Reading YAML at $YAML_PATH and state $INPUT_STATE"
echo "Using $PRECISION bits of precision, stepSize=$TROTTER_STEP, order=$TROTTER_ORDER"
echo "Sample size = $SAMPLE_SIZE"
echo
read -p "Confirm? [Y/n] " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    echo "Abbreviated prompts enabled."
    # do dangerous stuff - cleans out previous temp folder
    rm -r _temp; mkdir _temp


    # add a parameter logfile
    echo "Parameters Logfile" > ./_temp/logfile.txt
    echo "YAML: $YAML_PATH" >> ./_temp/logfile.txt
    echo "Input state: $INPUT_STATE" >> ./_temp/logfile.txt
    echo "Precision: $PRECISION" >> ./_temp/logfile.txt
    echo "Trotter_step: $TROTTER_STEP" >> ./_temp/logfile.txt
    echo "Trotter_order: $TROTTER_ORDER" >> ./_temp/logfile.txt
    echo "Sample_size: $SAMPLE_SIZE" >> ./_temp/logfile.txt
    echo "Run time: `date`" >> ./_temp/logfile.txt

    # ----- STEP 1 - Produce a sample energy estimate
    cd ../1ProduceSampleEnergy

    # arguments: path, state label, precision, step size, order, number of samples
    echo "RUNNING: dotnet run $CMD_ARGS $SAMPLE_SIZE >./_temp/_sampled_reference_energy.txt"
    echo
    dotnet run $CMD_ARGS $SAMPLE_SIZE >../TestPipeline/_temp/_sampled_reference_energy.txt
    cp ./_temp/_costEstimateReference.csv ../TestPipeline/_temp

    # ----- STEP 2 - Produce the JSON file
    echo "RUNNING: ./extract_gates.sh $CMD_ARGS"
    echo

    cd ../2ExtractTrotterGates
    mkdir _temp
    dotnet run $CMD_ARGS > ./_temp/log.txt
    python3 produceJSONV2.py ./_temp -o extracted_terms.json
    cp extracted_terms.json ../TestPipeline/_temp
    echo Finished term extraction.
    echo JSON at $directory/extracted_terms.json 
    echo

    # ----- STEP 3 - (optional) Process the terms
    echo "RUNNING: Python optimization script"
    echo
    cd ../3OptimizeCircuit/swap

    python3 QMap.py ../../TestPipeline/_temp/extracted_terms.json 2 1D > interaction_file.txt
    cp interaction_file.txt ../../TestPipeline/_temp/

    cd ..
    python3 outputToJSONV3.py ../TestPipeline/_temp/extracted_terms.json ../TestPipeline/_temp/interaction_file.txt ../TestPipeline/_temp/reconstructed.json
    # cp ./reconstructed.json ../TestPipeline/_temp/

    python3 produce_rounds.py ../TestPipeline/_temp/reconstructed.json
    cp ./rounds.json ../TestPipeline/_temp

    cd .. 
    # in QUASAR

    # ----- STEP 4 - Ingest the JSON file
    echo "RUNNING: JSON ingestion"
    echo
    cd ./4ImportOptimizedFermions 
    echo "YAML Path: $YAML_PATH" > ../TestPipeline/_temp/_sampled_optimized_energy.txt
    echo "RUNNING: dotnet run ./reconstructed.json $SAMPLE_SIZE >./_temp/_sampled_optimized_energy.txt"
    dotnet run ../TestPipeline/_temp/reconstructed.json $SAMPLE_SIZE $PRECISION >>../TestPipeline/_temp/_sampled_optimized_energy.txt
    echo

    cp ./_temp/_costEstimateOptimized.csv ../TestPipeline/_temp
    
    echo "Beginning validation and unit tests"
    cd ../5ValidationTests
    pytest

    echo "Finished - See _temp for outputs."
else
    echo "Exiting from user cancellation."
fi
