import argparse
import json

parser = argparse.ArgumentParser(
    description='Create rounds file from JSON.')

parser.add_argument("filepath", help="path to output folder", type=str)
parser.add_argument("-o", "--outpath", help="output name", type=str)
args = parser.parse_args()
path = args.filepath

outpath = "./rounds.json"

round_dict_list = []

if args.outpath:
    assert args.outpath[-5:] == ".json", "Must use a .json filename"
    assert len(args.outpath) > 5, "Must have a named file"
    outpath = args.outpath

print(f"Outputting file to {outpath}")


def make_round_dict():
    out_dict = {}
    for index in range(0, number_of_qubits):
        out_dict["qubit" + str(index)] = "?"
    return out_dict


with open(path) as f:
    json_data = json.load(f)
    term_data = json_data["terms"]
    number_of_qubits = json_data["constants"]["nSpinOrbitals"]

    round_dict_list.append(make_round_dict())

    round_num = 0
    occupied_qubits = set()
    temp_dict = make_round_dict()

    # for each term in the term data
    for gate_index in range(0, len(term_data)):

        # unpack the data
        gate_info = term_data[gate_index]
        term_name = gate_info["type"]
        angle = gate_info["angle"]
        ops = gate_info["ops"]
        targets = gate_info["targets"]

        # need to check if the target qubit has already been occupied
        if set(targets).intersection(occupied_qubits):
            # we have tried to reference a qubit that's already being used
            # so we need to place this in another round

            # temp_dict["occupied_qubits"] = list(occupied_qubits)

            # dump the previous round's data
            round_dict_list.append(temp_dict)

            # add one to the round
            round_num += 1

            # reset the occupied qubits list
            occupied_qubits = set()

            # make a new dict
            temp_dict = make_round_dict()

        # update the occupied qubits list
        occupied_qubits.update(targets)

        # for each target qubit
        for qubit_id in range(len(targets)):

            # prepare a dict with info on the operation
            qubit_info = {
                "type": term_name,
                "targets": targets,
                "op": ops[qubit_id],
                "angle": angle,
                "gate_id": gate_index,
                "qubit_id": targets[qubit_id]
            }

            # add it to the dict to be sent out
            temp_dict['qubit' + str(targets[qubit_id])] = qubit_info

round_dict_list.append(temp_dict)

"""
[
    {
        qubit1:
        {
            type: "",
            targets: "",
            angle: 0.0,
            op: 0,
            gate_id: 0
        },
        occupied_qubits: []
    }
]
"""

with open(outpath, 'w') as outfile:
    json.dump(round_dict_list, outfile)
