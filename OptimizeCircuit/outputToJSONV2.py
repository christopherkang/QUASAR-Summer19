# Author Christopher Kang
# Pacific Northwest National Laboratory
import json
import collections
import argparse
import ast

# Input: we want to take a file in the following format:
# SWAP phase: [x_1, x_2, x_3] where the data in q_1, q_2, ... moves to x_1, x_2, ...
# Interaction phase: [(x_1, x_2), (...)] where the program instructs (x_1, x_2) to interact


parser = argparse.ArgumentParser(
    description='Reconstruct JSON file from optimizations')

parser.add_argument("filepath", help="path to input json", type=str)
parser.add_argument(
    "optimizations", help="path to input optimizations", type=str)
args = parser.parse_args()
import_path = args.filepath
optimization_path = args.optimizations


# import term data from JSON
# import_path = "extracted_terms.json"
# optimization_path = "optimizations.txt"
out_path = "reconstructed.json"

# import_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/OptimizeCircuit/swap/test.json"
# optimization_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/OptimizeCircuit/Interaction Samples/sample_interaction_file_7.txt"


# categorize the term data with collections.defaultdict
def prepare_interaction_categories(file_path):
    """Prepare the interaction categories to be used later on

    Arguments:
        file_path {str} -- json file path

    Returns:
        collections category -- categories to list of terms
    """
    interaction_categories = collections.defaultdict(list)
    with open(file_path) as json_file:

        # load json file + the data terms
        data = json.load(json_file)
        terms = data["terms"]

        # for each term
        for term_data in terms:
            # add it to the interaction category
            targets = term_data["targets"].copy()
            targets.sort()

            # the id will be the str / tuple version of the array
            # FLAG - changed to str / tuple / set; originally was str / tuple
            target_id = str(tuple(set(targets)))
            # print(term_data["targets"])

            # flag - will this consider [1, 2] and [2, 1] as the same? let's have it autosort for the EXP
            interaction_categories[target_id].append(term_data)
    return interaction_categories


def retrieve_auxiliary_data(file_path):
    """Get the auxiliary data

    Arguments:
        file_path {str} -- name of the json file

    Returns:
        dict, dict -- other data from json file
    """
    with open(file_path) as json_file:
        data = json.load(json_file)
        constants = data["constants"]
        state_prep_data = data["statePrepData"]
    return constants, state_prep_data


hamiltonian_constants, state_prep_data = retrieve_auxiliary_data(import_path)
number_of_qubits = hamiltonian_constants["nSpinOrbitals"]

output_json = {
    "constants": hamiltonian_constants,
    "statePrepData": state_prep_data
}


# categories + their terms
categorized_interactions = prepare_interaction_categories(import_path)
# print(categorized_interactions)

# go through the interactions and swaps and relabel the information
with open(optimization_path, "r") as interaction_file:
    # this represents the order of the original spins in the qubits
    spin_order = list(range(0, number_of_qubits))

    # new terms to be used
    new_term_list = []

    for line in interaction_file:
        line = line.rstrip()
        if "Iteration" in line:
            # parse the interactions
            # Iteration  1 : [(5, 4), (1, 2), (6, 7)]
            interaction_list = line.split(" : ")[1]
            interaction_list = ast.literal_eval(interaction_list)

            # convert the interaction list back to spin orbital numberings
            for interaction_term in interaction_list:
                # we now have a tuple; each of these needs to be converted
                # we need an array where we give it a qubit id and it returns the original spin orbital

                # relabel the qubits with their spin orbitals
                renumbered_terms = list(
                    map(lambda x: spin_order[x], interaction_term))

                # pull this type of term from the categorized term list
                sorted_renumbered_terms = renumbered_terms.copy()
                sorted_renumbered_terms.sort()

                # pull the relevant terms
                relevant_terms = categorized_interactions.pop(str(
                    tuple(sorted_renumbered_terms)))

                for term in relevant_terms:
                    # relabel their targets to align with the renumbered qubits

                    # these were the original orbitals being interacted on
                    term['orbitals'] = term['targets']

                    # these are the new qubit targets that need to be interacted on
                    term['targets'] = list(
                        map(lambda x: spin_order.index(x), term['targets']))

                # add them to our list
                new_term_list.extend(relevant_terms)
        elif "Number" in line:
            pass
        elif "Time" in line:
            pass
        elif not line:
            pass
        else:
            # [(0, 1), (2, 3)]
            # input: list of tuples with two adjacent numbers describing the swaps that need to occur
            # output: update the qubit order
            # do work on the SWAPs

            # parse the line
            swap_patterns = ast.literal_eval(line)

            # update the swapped qubits
            for update_pattern in swap_patterns:
                swap_template = {
                    "type": "SWAP",
                    "angle": 0,
                    "controls": [],
                    "ops": [],
                    "targets": []
                }
                # 4, 3, 2, 1, 0 -> want to swap 2nd, 3rd qubits (2, 1)
                qubit1 = update_pattern[0]
                qubit2 = update_pattern[1]
                spin_order[qubit1], spin_order[qubit2] = spin_order[qubit2], spin_order[qubit1]
                swap_template['targets'] = [qubit1, qubit2]

                new_term_list.append(swap_template)

            # ensure that all elements of the ordering are unique
            assert len(set(spin_order)) == len(spin_order)
    while categorized_interactions:
        # get the term
        terms = categorized_interactions.popitem()[1]
        for single_body in terms:
            # rename the qubit targets
            single_body["targets"] = list(
                map(lambda x: spin_order.index(x), single_body["targets"]))

            new_term_list.append(single_body)


output_json["terms"] = new_term_list

with open(out_path, 'w') as out_file:
    json.dump(output_json, out_file)
