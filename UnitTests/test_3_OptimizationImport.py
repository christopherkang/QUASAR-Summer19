import ast
import auxiliary

# path_to_check = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/3OptimizeCircuit/swap/test.txt"
path_to_check = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/3OptimizeCircuit/swap/big.txt"
max_qubit_number = 12


def tests_are_working():
    assert 1 == 1


def test_check_max_inputs():
    # verify that the largest qubits to be swapped are less than our max and at least 0
    ignore_line_symbols = ["Number", "Time", "Bringing", "{", "Iteration"]
    with open(path_to_check) as f:
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
    ignore_line_symbols = ["Number", "Time", "Bringing", "{", "Iteration"]
    order = auxiliary.SpinOrder(max_qubit_number)
    with open(path_to_check) as f:
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
    with open(path_to_check) as f:
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


"""
def test_swap_directly():
    # Verify that the swaps made by outputToJSONV2 are correct
    # assumes of input format of
    # [spin positions]
    # (swaps)
    # ...
    path_to_check = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/TestPipeline/_temp/interaction_file.txt"
    order = auxiliary.SpinOrder(8)
    with open(path_to_check) as f:
        isAssert = True
        for line in f:
            line = line.rstrip()
            if isAssert:
                assert order.return_order() == ast.literal_eval(line)
                print(line)
            else:
                print(order.update(ast.literal_eval(line)))
            isAssert = not isAssert
"""
