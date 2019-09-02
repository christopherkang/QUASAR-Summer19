import ast


class SpinOrder():
    def __init__(self, number_of_qubits=None, starting_order=None):
        if number_of_qubits:
            self.spin_order = list(range(0, number_of_qubits))
        if starting_order:
            self.spin_order = starting_order

    def update(self, swap_list):
        """Updates the swap order

        Arguments:
            swap_list {list of 2-size tuples} -- swaps to be applied

        Returns:
            list -- new ordering
        """
        for swap in swap_list:
            self.spin_order[swap[0]], self.spin_order[swap[1]] = \
                self.spin_order[swap[1]], self.spin_order[swap[0]]
        return self.spin_order

    def return_order(self):
        return self.spin_order


def _verify_swap_pattern(path_to_check):
    ignore_line_symbols = ["Number", "Time", "Bringing", "{", "Iteration"]
    number_of_qubits = 8
    order = SpinOrder(number_of_qubits)
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
    # path_to_check = "~/Documents/GitHub/QUASAR-Summer19/UnitTests/test2.txt"
    path_to_check = input("Path of the output of QMAP? ")
    _verify_swap_pattern(path_to_check)


if __name__ == "__main__":
    test_verify_swap_pattern()
