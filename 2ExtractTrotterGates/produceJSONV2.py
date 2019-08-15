import argparse
import json


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

# parse the fermion term file
with open(path + "/_FermionTerms.txt") as f:
    for line in f:
        print(line)
        terms.append(parse_line(line.rstrip()))

# parse the constants data file
with open(path + "/_constants.txt") as f:
    for line in f:
        keyValuePair = line.rstrip().split(":")
        print(keyValuePair)
        constants[keyValuePair[0]] = eval(
            keyValuePair[1] + f"({keyValuePair[2]})")

# parse the state prep data file
with open(path + "/_stateData.txt") as f:
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
