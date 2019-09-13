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
parser.add_argument("out_path", help="path to output new JSON", type=str)
args = parser.parse_args()
import_json_path = args.filepath
optimization_file_path = args.optimizations
out_path = args.out_path

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

            # the id will be the str / tuple version of the array
            # FLAG - changed to str / tuple / set; originally was str / tuple
            target_id = str(tuple(sorted(set(targets))))

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


def parse_iteration_line(line_text, qubit_orbitals, categorized_interactions):
    """Parses a line with interactions.

    Arguments:
        line_text {string} -- Iteration information
        In the form: "Iteration 1 : [(5, 4), (1, 2), (6, 7)]
        qubit_orbitals {array of ints} -- qubit at index n has orbital {value}
        categorized_interactions {collections defaultdict} -- categorized terms

    Returns:
        list of dicts -- array of JSON terms with renumbered targets
    """
    interaction_list = line_text.split(" : ")[1]
    interaction_list = ast.literal_eval(interaction_list)

    terms_to_keep = []

    # convert the interaction list back to spin orbital numberings
    for interaction_term in interaction_list:
        print(interaction_term)
        # we now have a tuple; each of these needs to be converted
        # relabel the qubits with their spin orbitals
        renumbered_terms = list(
            map(lambda x: qubit_orbitals[x], interaction_term))

        # pull this type of term from the categorized term list
        sorted_renumbered_terms = renumbered_terms.copy()
        sorted_renumbered_terms.sort()

        # pull the relevant terms
        relevant_terms = categorized_interactions.pop(str(
            tuple(sorted_renumbered_terms)))

        # we are extracting lists
        # assert len(relevant_terms) < 2, "test"

        for term in relevant_terms:
            # relabel their targets to align with the renumbered qubits
            term['orbitals'] = term['targets']

            # these are the new qubit targets that need to be interacted on
            term['targets'] = list(
                map(lambda x: qubit_orbitals.index(x), term['targets']))

        terms_to_keep.extend(relevant_terms)

    return terms_to_keep


def parse_swap_line(swap_pattern, spin_order):
    # [(0, 1), (2, 3)]
    # input: list of tuples with two adjacent numbers describing the swaps that need to occur
    # output: update the qubit order
    # do work on the SWAPs

    swap_terms_to_add = []

    # parse the line
    swap_patterns = ast.literal_eval(swap_pattern)

    # update the swapped qubits
    for update_pattern in swap_patterns:
        swap_template = {
            "type": "SWAP",
            "angle": 0,
            "controls": [],
            "ops": [],
            "targets": [],
            "orbitals": []
        }
        # 4, 3, 2, 1, 0 -> want to swap 2nd, 3rd qubits (2, 1)
        qubit1 = update_pattern[0]
        qubit2 = update_pattern[1]
        spin_order[qubit1], spin_order[qubit2] = spin_order[qubit2], spin_order[qubit1]
        swap_template['targets'] = [qubit1, qubit2]

        sorted_orbitals = [spin_order[qubit1], spin_order[qubit2]]
        sorted_orbitals.sort()

        swap_template['orbitals'] = sorted_orbitals

        swap_terms_to_add.append(swap_template)

    # ensure that all elements of the ordering are unique
    assert len(set(spin_order)) == len(spin_order)
    return swap_terms_to_add


def produce_json(import_path, optimization_path, print_swaps=False, print_spin_order=False):
    hamiltonian_constants, state_prep_data = retrieve_auxiliary_data(
        import_path)
    number_of_qubits = hamiltonian_constants["nSpinOrbitals"]

    output_json = {
        "constants": hamiltonian_constants,
        "statePrepData": state_prep_data
    }

    # categories + their terms
    categorized_interactions = prepare_interaction_categories(import_path)

    ignore_line_symbols = ["Number", "Time", "Bringing", "{"]

    # go through the interactions and swaps and relabel the information
    with open(optimization_path, "r") as interaction_file:
        # this represents the order of the original spins in the qubits
        spin_order = list(range(0, number_of_qubits))

        # new terms to be used
        new_interaction_term_list = []
        new_swap_term_list = []
        new_single_body_list = []

        # swap series to accumulate the SWAPs into rounds.
        temp_swap_list = []

        for line in interaction_file:
            # print(spin_order)
            # print(line)
            line = line.rstrip()
            if "Iteration" in line:
                # add them to our list
                new_interaction_term_list.append(parse_iteration_line(
                    line, spin_order, categorized_interactions))

                # push the new swap list
                new_swap_term_list.append(temp_swap_list)
                temp_swap_list = []
            elif any(symbol in line for symbol in ignore_line_symbols):
                pass
            elif not line:
                pass
            elif "SWAP : " in line:
                stripped_line = line.split(" : ")[1]
                if print_spin_order:
                    print(spin_order)
                if print_swaps:
                    print(line)
                temp_swap_list.append(
                    parse_swap_line(stripped_line, spin_order))

        # push last row of SWAPs
        new_swap_term_list.append(temp_swap_list)

        assert spin_order == list(range(0, number_of_qubits))
        while categorized_interactions:
            # get the term
            terms = categorized_interactions.popitem()[1]
            for single_body in terms:
                single_body["orbitals"] = single_body["targets"]

                # assert len(
                #     single_body["orbitals"]) == 2, "Single body terms are not single bodies!!"

                # rename the qubit targets
                single_body["targets"] = list(
                    map(lambda x: spin_order.index(x), single_body["targets"]))

                new_single_body_list.append(single_body)

        new_interaction_term_list.insert(0, new_single_body_list)

    output_json["terms"] = {
        "swaps": new_swap_term_list,
        "interactions": new_interaction_term_list
    }

    return output_json


if __name__ == "__main__":
    final_json = produce_json(
        import_json_path, optimization_file_path, print_swaps=False, print_spin_order=False)
    with open(out_path, 'w') as out_file:
        json.dump(final_json, out_file)
