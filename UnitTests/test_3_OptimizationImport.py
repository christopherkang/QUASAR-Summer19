import ast
import auxiliary


def tests_are_working():
    assert 1 == 1


def test_check_max_inputs():
    # verify that the largest qubits to be swapped are less than our max and at least 0
    path_to_check = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/TestPipeline/_temp/interaction_file.txt"
    ignore_line_symbols = ["Number", "Time", "Bringing", "{", "Iteration"]
    with open(path_to_check) as f:
        for line in f:
            line = line.rstrip()
            if any(symbol in line for symbol in ignore_line_symbols):
                pass
            elif not line:
                pass
            else:
                swap_list = ast.literal_eval(line)
                for swap in swap_list:
                    assert max(swap[0], swap[1]) <= 7, "Element too large"
                    assert min(swap[0], swap[1]) >= 0, "Element too large"


def _verify_swap_pattern(path_to_check):
    # helper method
    ignore_line_symbols = ["Number", "Time", "Bringing", "{", "Iteration"]
    number_of_qubits = 8
    order = auxiliary.SpinOrder(number_of_qubits)
    with open(path_to_check) as f:
        for line in f:
            line = line.rstrip()
            if any(symbol in line for symbol in ignore_line_symbols):
                pass
            elif not line:
                pass
            else:
                line = ast.literal_eval(line)
                print(order.update(line))
    assert order.return_order() == list(range(0, number_of_qubits))


def test_verify_swap_pattern():
    # Verify that the SWAPs eventually yield 0, 1, 2, ...
    path_to_check = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/UnitTests/debug.txt"
    _verify_swap_pattern(path_to_check)


def test_swap_directly():
    # Verify that the swaps made by outputToJSONV2 are correct
    # assumes of input format of
    # [spin positions]
    # (swaps)
    # ...
    path_to_check = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/UnitTests/debug.txt"
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
