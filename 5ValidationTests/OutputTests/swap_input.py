import ast
import auxiliary

while True:
    mode = input("Run auto init? [Y/n]")

    if mode == "Y":
        spin_order = auxiliary.SpinOrder(
            number_of_qubits=int(input("Number of qubits? ")))
    elif mode == "n":
        initial_order = ast.literal_eval(input("Initial ordering: "))
        spin_order = auxiliary.SpinOrder(starting_order=initial_order)
    else:
        print("Input unrecognized")

    while True:
        swap_list = input("swaps? ")
        swap_list = ast.literal_eval(swap_list)
        print(f"New order: {spin_order.update(swap_list)}")
        print()
