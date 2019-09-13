#!/bin/bash
# Test pipeline components
# Assumptions - placed in cloned Github directory with file structure intact

# STEP 0 - Setup
# YAML_PATH="/Users/kang828/Documents/GitHub/Quantum/Chemistry/IntegralData/YAML/H4/h4_sto6g_0.100.yaml"
# YAML_PATH="/Users/kang828/Documents/GitHub/Quantum/Chemistry/IntegralData/Broombridge_v0.2/H2_sto-3g.yaml"

SECONDS=0

# extract the parameters from the .params file
source $1

CMD_ARGS="$YAML_PATH $INPUT_STATE $PRECISION $TROTTER_STEP $TROTTER_ORDER"

echo
echo "Reading YAML at $YAML_PATH and state $INPUT_STATE"
echo "Using $PRECISION bits of precision, stepSize=$TROTTER_STEP, order=$TROTTER_ORDER"
echo "Sample size = $SAMPLE_SIZE"
echo

echo "Abbreviated prompts enabled."
# do dangerous stuff - cleans out previous temp folder
STARTING_DIR=$(pwd)

FOLDER_NAME=$STARTING_DIR/_data_"$FOLDER_NAME"_`date +%Y-%m-%d.%H:%M:%S`

rm -r $FOLDER_NAME
mkdir $FOLDER_NAME

# add a parameter logfile
# start by copy/pasting the original params file
cp $1 $FOLDER_NAME/logfile.txt
echo "" >> $FOLDER_NAME/logfile.txt

# add the starting run time
echo "Run time: $(date)" >> $FOLDER_NAME/logfile.txt
echo "" >> $FOLDER_NAME/logfile.txt

# ----- STEP 1 - Produce a sample energy estimate
cd ../1ProduceSampleEnergy
tempSeconds=$SECONDS

# arguments: path, state label, precision, step size, order, number of samples
echo "RUNNING: dotnet run $CMD_ARGS $SAMPLE_SIZE > $FOLDER_NAME/_sampled_reference_energy.txt"
echo
dotnet run $CMD_ARGS $SAMPLE_SIZE > $FOLDER_NAME/_sampled_reference_energy.txt
mkdir $FOLDER_NAME/ReferenceCostEstimates
cp -r ./_temp/ $FOLDER_NAME/ReferenceCostEstimates

echo "1ProduceSampleEnergy time: $(($SECONDS - $tempSeconds)) seconds" >> $FOLDER_NAME/logfile.txt

# ----- STEP 2 - Produce the JSON file
echo "RUNNING: ./extract_gates.sh $CMD_ARGS"
echo
tempSeconds=$SECONDS

cd ../2ExtractTrotterGates
mkdir _temp
dotnet run $CMD_ARGS >./_temp/log.txt
python3 produceJSONV2.py ./_temp -o extracted_terms.json
cp extracted_terms.json $FOLDER_NAME
echo Finished term extraction.
echo JSON at $FOLDER_NAME/extracted_terms.json
echo

echo "2ExtractTrotterGates time: $(($SECONDS - $tempSeconds)) seconds" >> $FOLDER_NAME/logfile.txt


# ----- STEP 3 - (optional) Process the terms
echo "RUNNING: Python optimization script"
echo
tempSeconds=$SECONDS

cd ../3OptimizeCircuit/swap

python3 QMap.py $FOLDER_NAME/extracted_terms.json 2 1D >interaction_file.txt
cp interaction_file.txt $FOLDER_NAME/

cd ..
python3 outputToJSONV3.py $FOLDER_NAME/extracted_terms.json $FOLDER_NAME/interaction_file.txt $FOLDER_NAME/reconstructed.json
# cp ./reconstructed.json ../TestPipeline/_temp/

python3 produce_roundsV2.py $FOLDER_NAME/reconstructed.json
cp ./rounds.json $FOLDER_NAME

echo "3OptimizeCircuit time: $(($SECONDS - $tempSeconds)) seconds" >> $FOLDER_NAME/logfile.txt


cd ..
# in QUASAR

# ----- STEP 4 - Ingest the JSON file
echo "RUNNING: JSON ingestion"
echo
tempSeconds=$SECONDS

cd ./4ImportOptimizedFermions

echo "YAML Path: $YAML_PATH" >$FOLDER_NAME/_sampled_optimized_energy.txt
echo "RUNNING: dotnet run ./reconstructed.json $SAMPLE_SIZE >./$FOLDER_NAME/_sampled_optimized_energy.txt"
dotnet run $FOLDER_NAME/reconstructed.json $SAMPLE_SIZE $PRECISION >>$FOLDER_NAME/_sampled_optimized_energy.txt
echo

mkdir $FOLDER_NAME/OptimizedCostEstimates
cp -r ./_temp/ $FOLDER_NAME/OptimizedCostEstimates

echo "4ImportOptimizedFermions time: $(($SECONDS - $tempSeconds)) seconds" >> $FOLDER_NAME/logfile.txt

echo "Beginning validation and unit tests"

cd ../5ValidationTests

rm -r _temp
# mkdir _temp

cp -R $FOLDER_NAME _temp
pytest

echo "Finished - See $FOLDER_NAME for outputs."
