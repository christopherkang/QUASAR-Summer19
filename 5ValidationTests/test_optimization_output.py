import ast
import auxiliary
import json

# interaction_file_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/3OptimizeCircuit/swap/test.txt"
# interaction_file_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/3OptimizeCircuit/swap/interaction_file.txt"
# interaction_file_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/TestPipeline/_data_failed_round/interaction_file.txt"
# json_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/TestPipeline/_data_failed_round/extracted_terms.json"

interaction_file_path = auxiliary.interaction_file_path
input_json_path = auxiliary.input_json_path

number_of_orbitals = 12


ignore_line_symbols = ["Number", "Time", "Bringing", "{"]


def tests_are_working():
    assert 1 == 1


def test_check_max_inputs():
    # verify that the largest qubits to be swapped are less than our max and at least 0
    with open(interaction_file_path) as f:
        for line in f:
            line = line.rstrip()
            if any(symbol in line for symbol in ignore_line_symbols):
                pass
            elif not line:
                pass
            elif "SWAP" in line:
                stripped = line.split(" : ")[1]
                swap_list = ast.literal_eval(stripped)
                for swap in swap_list:
                    assert max(swap[0], swap[1]
                               ) < number_of_orbitals, "Element too large"
                    assert min(swap[0], swap[1]) >= 0, "Element too large"


def test_verify_interaction_number_matches():
    """Verify that the number of unique interactions matches the actual number
    """
    with open(interaction_file_path) as f:
        number_of_unique_interactions = 0
        running_total_of_interactions = 0
        for line in f:
            line = line.rstrip()
            if "Number of Unique Interactions:" in line:
                raw_line = line.split(":  ")[1]
                number_of_unique_interactions = ast.literal_eval(raw_line)
            elif "Iteration" in line:
                raw_line = line.split(" : ")[1]
                interactions = ast.literal_eval(raw_line)
                print(interactions)
                running_total_of_interactions += len(interactions)

    print(number_of_unique_interactions)
    assert number_of_unique_interactions == running_total_of_interactions, "The number of interactions is unverified"


def test_all_interactions_are_valid():
    """Major unit test - validates almost all components of the interaction file, including:
    - Ensuring that spin orbital positions + interactions match up
    - Validating that interactions only occur where gates say they occur
    - Ensures 'bringing back' returns qubits into their canonical ordering
    """

    # holds the spin order
    order = auxiliary.SpinOrder(number_of_orbitals)

    # pull the files
    json_file = open(input_json_path)
    interaction_file = open(interaction_file_path)

    original_json = json.load(json_file)
    original_gates = original_json["terms"]

    for line in interaction_file:
        line = line.rstrip()

        if any(symbol in line for symbol in ignore_line_symbols):
            pass
        elif not line:
            pass
        elif "SWAP" in line:
            stripped = line.split(" : ")[1]
            swap_list = ast.literal_eval(stripped)

            # update the swap order
            order.update(swap_list)
            # print(order.update(swap_list))
        elif "Orbital position" in line:
            stripped = line.split(":  ")[1]
            dict_list = ast.literal_eval(stripped)

            # pull the ordering from the interaction file
            swap_list = list(dict_list.values())

            # verify this ordering matches our current ordering
            assert swap_list == order.return_order(), "There was a mismatch before the end"
        elif "Iteration" in line:
            stripped = line.split(" : ")[1]

            # pull the interactions
            interaction_list = ast.literal_eval(stripped)

            for interaction in interaction_list:
                spin_order = order.return_order()

                # reorder the interactions to their orbitals and verify against the JSON
                reordered_interaction = list(
                    map(lambda x: spin_order[x], interaction))
                matching_gates = filter(
                    lambda gate: sorted(set(gate["targets"])) == sorted(set(reordered_interaction)), original_gates)

                avail_gates = list(matching_gates)
                assert avail_gates, "There are no matching gates"

    assert order.return_order() == list(range(0, number_of_orbitals))
    json_file.close()
    interaction_file.close()
