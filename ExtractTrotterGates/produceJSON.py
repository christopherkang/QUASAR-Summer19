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

    line_terms = line.split(" | ")
    print(line_terms)
    out_dict = {
        "type": line_terms[0],
        "angle": 0,
        "controls": [],
        "ops": [],
        "parity": [],
        "targets": []
    }
    term_name = line_terms[0]
    qubit_targets = line_terms[1].split(", ")

    # add the base ops and angle for all terms
    op_terms = Constant.OPS.value[term_name]
    out_dict["ops"] = list(
        map(lambda x: Constant.PAULI_TO_INT.value[x], op_terms))
    out_dict["angle"] = line_terms[2]
    out_dict["targets"] = qubit_targets

    if "Zterm" in term_name:
        # no additional work necessary
        pass
    elif "ZZterm" in term_name:
        # no additional work necessary
        pass
    elif "PQterm" in term_name:
        # we'll need to add the qubits in between the two given qubits
        for index in range(qubit_targets[0], qubit_targets[1]):
            out_dict["targets"].append(index)
            out_dict["ops"].append(Constant.PAULI_TO_INT.value["PauliZ"])
    elif "PQQRterm" in term_name:
        # we need to do a more complicated series of manipulations
        pass
    elif "0123term" in term_name:
        # we have a 0123 term - additional information is stored in the +
        term_type = term_name[-1]

        # FLAG unfinished
        out_dict["ops"] = [
            map(lambda x: Constant.PAULI_TO_INT[x], Constant.OPS_0123[term_type])]

        qubit_targets = line_terms[1].split(", ")
        out_dict["targets"] = qubit_targets
        out_dict["angle"] = line_terms[2]

    # # the number of operations should match the number of target and parity qubits
    # assert len(out_dict["ops"]) == len(
    #     out_dict["targets"]) + len(out_dict["parity"])

    return out_dict


# add command line arguments to run the python script
parser = argparse.ArgumentParser(
    description='Create JSON file from C# output.')

parser.add_argument("filepath", help="path to .NET output", type=str)
parser.add_argument("-o", "--outpath", help="output name", type=str)
args = parser.parse_args()
path = args.filepath

outpath = "./hamiltonian.json"

data = {}
terms = []
constants = {}

if args.outpath:
    assert args.outpath[-5:] == ".json", "Must use a .json filename"
    assert len(args.outpath) > 5, "Must have a named file"
    outpath = args.outpath

print(f"Outputting file to {outpath}")

# parse the file
with open(path) as f:
    # this script needs to have resiliency from extraneous lines
    # remember, it is the .NET output, so it may begin with error lines
    hasTermInfo = False
    hasConstantInfo = False
    for line in f:
        cleanedLine = line.rstrip()
        if (cleanedLine == "----- END FILE -----"):
            print("End of file reached")
        elif (cleanedLine == "----- BEGIN ORACLE WRITE -----"):
            # next lines will have term info
            hasTermInfo = True
        elif (cleanedLine == "----- END ORACLE WRITE -----"):
            # Oracle writing process is over; we're now in metadata and constant time
            hasTermInfo = False
            hasConstantInfo = True
        elif hasTermInfo:
            # ingest the term here
            if not cleanedLine[:2] == "Ge":
                terms.append(parse_line(cleanedLine))
        elif hasConstantInfo:
            # take in constant information, in the format "name: data"
            print("not implemented yet either LOL")
            keyValuePair = cleanedLine.split(": ")
            print(keyValuePair)
            constants[keyValuePair[0]] = keyValuePair[1]

        # print(line)

data = {
    "constants": constants,
    "terms": terms
}


# output the data
with open(outpath, 'w') as outfile:
    json.dump(data, outfile)
