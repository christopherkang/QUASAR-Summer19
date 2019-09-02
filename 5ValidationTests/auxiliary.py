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
