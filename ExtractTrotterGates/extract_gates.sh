#!/bin/bash
# Extract and Convert Trotter terms to the JSON format
# Positional argument order:
# YAML, input state label, precision, step size, and trotter order.

if [ "$5" != "" ]
then
    # do dangerous stuff
    directory=$(pwd)
    mkdir temp
    echo Saving a temp file to $directory"/temp/_temp.txt"
    dotnet run $1 $2 $3 $4 $5 > ./temp/_temp.txt
    python3 produceJSON.py $directory"/temp" -o extracted_terms.json
    echo Finished term extraction. 
    echo JSON at $directory/extracted_terms.json 
else
    echo "There are not enough positional parameters."
fi