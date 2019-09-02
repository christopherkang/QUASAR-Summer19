import ast
import auxiliary
import json

# interaction_file_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/3OptimizeCircuit/swap/test.txt"
# interaction_file_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/3OptimizeCircuit/swap/interaction_file.txt"
# interaction_file_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/TestPipeline/_data_failed_round/interaction_file.txt"
# json_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/TestPipeline/_data_failed_round/extracted_terms.json"

interaction_file_path = auxiliary.interaction_file_path
input_json_path = auxiliary.input_json_path

max_qubit_number = 12


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
                               ) < max_qubit_number, "Element too large"
                    assert min(swap[0], swap[1]) >= 0, "Element too large"


def test_verify_swap_pattern():
    order = auxiliary.SpinOrder(max_qubit_number)
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
                print(order.update(swap_list))
            elif "Orbital position" in line:
                stripped = line.split(":  ")[1]
                dict_list = ast.literal_eval(stripped)
                swap_list = list(dict_list.values())
                assert swap_list == order.return_order(), "There was a mismatch before the end"

    canonical_order = list(range(max_qubit_number))
    assert order.return_order() == canonical_order, "The final ordering is not canonical"


def test_verify_interaction_number_matches():
    # verify that the number of unique interactions matches the actual number
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
    # verify that the interactions are actually on valid qubits
    order = auxiliary.SpinOrder(max_qubit_number)
    with open(input_json_path) as json_file:
        original_json = json.load(json_file)
        original_gates = original_json["terms"]
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
                    order.update(swap_list)
                    # print(order.update(swap_list))
                elif "Orbital position" in line:
                    stripped = line.split(":  ")[1]
                    dict_list = ast.literal_eval(stripped)
                    swap_list = list(dict_list.values())
                    assert swap_list == order.return_order(), "There was a mismatch before the end"
                elif "Iteration" in line:
                    stripped = line.split(" : ")[1]
                    interaction_list = ast.literal_eval(stripped)
                    for interaction in interaction_list:
                        spin_order = order.return_order()
                        reordered_interaction = list(
                            map(lambda x: spin_order[x], interaction))
                        matching_gates = filter(
                            lambda gate: sorted(set(gate["targets"])) == sorted(set(reordered_interaction)), original_gates)

                        avail_gates = list(matching_gates)

                        print(f"{interaction} | {reordered_interaction}")
                        print(avail_gates)

                        assert avail_gates, "There are no matching gates"
