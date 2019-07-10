#!/bin/bash
# Extract and Convert Trotter terms to the JSON format

read -p "Have you already specified the desired Hamiltonian in the C# file? [Y/n] " -n 1 -r
echo    # (optional) move to a new line
if [[ $REPLY =~ ^[Yy]$ ]]
then
    # do dangerous stuff
    directory=$(pwd)
    echo Saving a temp file to $directory"/_temp.txt"
    dotnet run > _temp.txt
    python3 produceJSON.py $directory"/_temp.txt" -o extracted_terms.json
    echo Finished term extraction. 
    echo JSON at $directory/extracted_terms.json 
fi