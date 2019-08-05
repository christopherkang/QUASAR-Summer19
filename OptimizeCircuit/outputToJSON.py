# Author Christopher Kang
# Pacific Northwest National Laboratory
import json
import collections
import argparse

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
            target_id = str(tuple(targets))
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

# go through the interactions and swaps and relabel the information

# this represents the order of the original spins in the qubits
spin_order = list(range(0, number_of_qubits + 1))

with open(optimization_path, "r") as interaction_file:
    # new terms to be used
    new_term_list = []

    # categories + their terms
    categorized_interactions = prepare_interaction_categories(import_path)
    while True:
        # [0, 1, 2, 3]
        swap_patterns = interaction_file.readline()

        # check that the number of qubits in swap == number of qubits
        assert len(swap_patterns) == number_of_qubits

        # [(0, 1), (2, 3)]
        interactions = interaction_file.readline()

        # end read file
        if not interactions:
            break

        # update the swapped qubits
        for index in range(0, len(swap_patterns)):
            # create a new qubit order
            new_spin_order = list(range(0, number_of_qubits + 1))

            # set it to the respective qubits
            # intuitively, we are moving the data from the index to the new index
            new_spin_order[swap_patterns[index]] = spin_order[index]

        # update the spin order
        spin_order = new_spin_order

        # ensure that all elements of the ordering are unique
        assert list(set(spin_order)) == spin_order

        # convert the interaction list back to spin orbital numberings
        for interaction_term in interactions:
            # we now have a tuple; each of these needs to be converted

            # we need an array where we give it a qubit id and it returns the original spin orbital
            renumbered_terms = list(
                map(lambda x: spin_order[x], interaction_term))

            # pull this type of term from the categorized term list
            sorted_renumbered_terms = renumbered_terms.copy()
            sorted_renumbered_terms.sort()

            # pull the relevant terms
            relevant_terms = categorized_interactions[str(
                tuple(sorted_renumbered_terms))]

            # add them to our list
            new_term_list.extend(relevant_terms)


output_json["terms"] = new_term_list

with open(out_path, 'w') as out_file:
    json.dump(output_json, out_file)
