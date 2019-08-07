import argparse
import json
from enum import Enum


class Constant(Enum):
    PAULI_TO_INT = {
        "PauliI": 0,
        "PauliX": 1,
        "PauliY": 2,
        "PauliZ": 3
    }

    OPS = {
        "Zterm": ["PauliZ"],
        "ZZterm": ["PauliZ", "PauliZ"],
        "PQterm+X": ["PauliX", "PauliX"],
        "PQterm+Y": ["PauliY", "PauliY"],
        "PQQRterm+X": ["PauliX", "PauliX"],
        "PQQRterm+Y": ["PauliY", "PauliY"],
        "PQQR_Parityterm+X": ["PauliX", "PauliX"],
        "PQQR_Parityterm+Y": ["PauliY", "PauliY"],
        "0123term+0": ["PauliX", "PauliX", "PauliX", "PauliX"],
        "0123term+1": ["PauliX", "PauliX", "PauliY", "PauliY"],
        "0123term+2": ["PauliX", "PauliY", "PauliX", "PauliY"],
        "0123term+3": ["PauliY", "PauliX", "PauliX", "PauliY"],
        "0123term+4": ["PauliY", "PauliY", "PauliY", "PauliY"],
        "0123term+5": ["PauliY", "PauliY", "PauliX", "PauliX"],
        "0123term+6": ["PauliY", "PauliX", "PauliY", "PauliX"],
        "0123term+7": ["PauliX", "PauliY", "PauliY", "PauliX"]
    }


def parse_line(line):
    """Parses line for Hamiltonian term information.

    Arguments:
        line {string} -- String, delimited with the " | " symbol, with information

    Returns:
        dict -- nested dictionary in "terms" standard
    """

    # OPS CONVERSION TABLE
    # [I, X, Y, Z] - [0, 1, 2, 3]

    # Split the line into its data components
    line_terms = line.split(" | ")

    # build a sample dict to be returned
    out_dict = {
        "type": line_terms[0],
        "angle": 0,
        "controls": [],
        "ops": [],
        "targets": []
    }

    # extract the qubit targets, and convert them to integers
    qubit_targets = line_terms[1].split(",")
    if qubit_targets[0] == '':
        qubit_targets = [0]
    qubit_targets = map(int, qubit_targets)

    out_dict["targets"] = list(qubit_targets)

    # also adds angle data to the dict
    out_dict["angle"] = float(line_terms[2])

    # the number of operations should match the number of target and parity qubits
    print(f"{out_dict['type']} | {out_dict['ops']} | {out_dict['targets']}")
    return out_dict


# add command line arguments to run the python script
parser = argparse.ArgumentParser(
    description='Create JSON file from C# output.')

parser.add_argument("filepath", help="path to output folder", type=str)
parser.add_argument("-o", "--outpath", help="output name", type=str)
args = parser.parse_args()
path = args.filepath

outpath = "./hamiltonian.json"

data = {}
terms = []
constants = {}
statePrepData = {
    "terms": []
}

if args.outpath:
    assert args.outpath[-5:] == ".json", "Must use a .json filename"
    assert len(args.outpath) > 5, "Must have a named file"
    outpath = args.outpath

print(f"Outputting file to {outpath}")

# parse the file
with open(path + "/_temp.txt") as f:
    # this script needs to have resiliency from extraneous lines
    # remember, it is the .NET output, so it may begin with error lines
    hasTermInfo = False
    hasConstantInfo = False
    for line in f:
        cleanedLine = line.rstrip()
        if (cleanedLine == "----- END FILE -----"):
            print("End of Hamiltonian Data file reached")
        elif (cleanedLine == "----- BEGIN ORACLE WRITE -----"):
            # next lines will have term info
            hasTermInfo = True
        elif (cleanedLine == "----- END ORACLE WRITE -----"):
            # Oracle writing process is over; we're now in metadata and constant time
            hasTermInfo = False
            hasConstantInfo = True
        elif hasTermInfo:
            # ingest the term here
            terms.append(parse_line(cleanedLine))
        elif hasConstantInfo:
            # take in constant information, in the format "name:type:data"
            # FLAG - WHEN ADDING NEW CONSTANTS, TYPE MUST BE THE PYTHON EQUIVALENT TO CONVERT
            # int(), float(), str(), etc.
            keyValuePair = cleanedLine.split(":")
            print(keyValuePair)
            constants[keyValuePair[0]] = eval(
                keyValuePair[1] + f"({keyValuePair[2]})")

with open(path + "/_tempState.txt") as f:
    statePrepData["int"] = int(f.readline())
    for line in f:
        # data comes in the format:
        # JordanWignerInputState(((1, 0), [0,1]))
        # so, we strip away unecessary info to (((1, 0), [0, 1]))
        raw_data = line.split("JordanWignerInputState")[1]
        raw_data = eval(raw_data)
        reformed_data = {
            "tuple": list(map(float, raw_data[0])),
            "array": list(map(int, raw_data[1]))
        }
        statePrepData["terms"].append(reformed_data)

data = {
    "constants": constants,
    "terms": terms,
    "statePrepData": statePrepData
}

# output the data
with open(outpath, 'w') as outfile:
    json.dump(data, outfile)
