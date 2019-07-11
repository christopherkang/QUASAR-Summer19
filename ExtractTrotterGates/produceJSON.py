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

    # find the type of the term
    term_type = line_terms[0]

    # extract the qubit targets, and convert them to integers
    qubit_targets = line_terms[1].split(", ")
    qubit_targets = list(map(int, qubit_targets))
    out_dict["targets"] = qubit_targets

    # add the base ops and angle for all terms.
    # Converts them to integers, as defined by the Constant class
    op_terms = Constant.OPS.value[term_type]
    out_dict["ops"] = list(
        map(lambda x: Constant.PAULI_TO_INT.value[x], op_terms))

    # also adds angle data to the dict
    out_dict["angle"] = float(line_terms[2])

    # modify the data based on the different terms
    if "Zterm" in term_type:
        # no additional work necessary
        pass
    elif "ZZterm" in term_type:
        # no additional work necessary
        pass
    elif "PQterm" in term_type:
        # we'll need to add the qubits in between the two given qubits
        for index in range(qubit_targets[0] + 1, qubit_targets[1]):
            out_dict["targets"].append(index)
            out_dict["ops"].append(Constant.PAULI_TO_INT.value["PauliZ"])
    elif "PQQRterm" in term_type:
        # we need to do a more complicated series of manipulations
        for index in range(qubit_targets[0] + 1, qubit_targets[1]):
            if (index == qubit_targets[2]):
                pass
            else:
                out_dict["targets"].append(index)
                out_dict["ops"].append(Constant.PAULI_TO_INT.value["PauliZ"])
    elif "PQQR_Parityterm" in term_type:
        # we need to do even more complicated work
        for index in range(qubit_targets[0] + 1, qubit_targets[1]):
            out_dict["targets"].append(index)
            out_dict["ops"].append(Constant.PAULI_TO_INT.value["PauliZ"])
        out_dict["targets"].append(qubit_targets[2])
        out_dict["ops"].append(Constant.PAULI_TO_INT.value["PauliZ"])
        pass
    elif "0123term" in term_type:
        # we have a 0123 term, so we'll need to add additional Z ops
        # use the naming convention from
        # /Chemistry/src/Runtime/JordanWigner/JordanWignerEvolutionSet.qs
        PQJW = range(qubit_targets[0] + 1, qubit_targets[1] - 1)
        RSJW = range(qubit_targets[2] + 1, qubit_targets[3] - 1)

        for index in PQJW:
            out_dict["targets"].append(index)
            out_dict["ops"].append(Constant.PAULI_TO_INT.value["PauliZ"])

        for index in RSJW:
            out_dict["targets"].append(index)
            out_dict["ops"].append(Constant.PAULI_TO_INT.value["PauliZ"])

    # the number of operations should match the number of target and parity qubits
    assert len(out_dict["ops"]) == len(out_dict["targets"])

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
            terms.append(parse_line(cleanedLine))
        elif hasConstantInfo:
            # take in constant information, in the format "name:type:data"
            # FLAG - WHEN ADDING NEW CONSTANTS, TYPE MUST BE THE PYTHON EQUIVALENT TO CONVERT
            # int(), float(), str(), etc.
            keyValuePair = cleanedLine.split(":")
            print(keyValuePair)
            constants[keyValuePair[0]] = eval(
                keyValuePair[1] + f"({keyValuePair[2]})")

data = {
    "constants": constants,
    "terms": terms
}


# output the data
with open(outpath, 'w') as outfile:
    json.dump(data, outfile)
