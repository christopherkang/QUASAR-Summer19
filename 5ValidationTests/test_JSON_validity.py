import auxiliary
import json

input_json_path = auxiliary.input_json_path
optimized_json_path = auxiliary.optimized_json_path


def _equal_ignore_order(a, b):
    """ Use only when elements are neither hashable nor sortable! """
    unmatched = b
    for element in a:
        try:
            unmatched.remove(element)
        except ValueError:
            return False
    return not unmatched


def _sum_dicts(list_of_dicts):
    running_sum = 0
    for individual in list_of_dicts:
        running_sum += individual["angle"]
    return running_sum


def test_quickly_input_json_matches_optimized():
    """Ensure that the optimized JSON file has the same data as the input JSON. Verifies that:
    - Does this quickly!! (Simply verifies the number of terms is maintained + angle sum is the same)
    - All constants and state data is unchanged
    - All gates are left unchanged (though they may have more data / be rearranged)
    """

    input_json_file = open(input_json_path)
    optimized_json_file = open(optimized_json_path)

    input_json = json.load(input_json_file)
    optimized_json = json.load(optimized_json_file)

    # verify noninteracting components are still OK
    assert input_json["constants"] == optimized_json["constants"]
    assert input_json["statePrepData"] == optimized_json["statePrepData"]

    # verify that the optimized terms are equivalent to the input terms
    # (when all the SWAPS are removed)
    input_terms = input_json["terms"]
    optimized_terms = optimized_json["terms"]["interactions"]

    non_trivial_term_count = 0

    non_trivial_term_list = []

    for termRound in optimized_terms:
        non_trivial_term_count += len(termRound)
        non_trivial_term_list.extend(termRound)

    assert len(input_terms) == non_trivial_term_count, "# of terms do not match"
    assert abs(_sum_dicts(input_terms)-_sum_dicts(
        non_trivial_term_list)) < 1e-7, "Sum of angles does not match"

    input_json_file.close()
    optimized_json_file.close()


def test_input_json_matches_optimized():
    """Ensure that the optimized JSON file has the same data as the input JSON. Verifies that:
    - All constants and state data is unchanged
    - All gates are left unchanged (though they may have more data / be rearranged)
    """

    input_json_file = open(input_json_path)
    optimized_json_file = open(optimized_json_path)

    input_json = json.load(input_json_file)
    optimized_json = json.load(optimized_json_file)

    # verify noninteracting components are still OK
    assert input_json["constants"] == optimized_json["constants"]
    assert input_json["statePrepData"] == optimized_json["statePrepData"]

    # verify that the optimized terms are equivalent to the input terms
    # (when all the SWAPS are removed)
    input_terms = input_json["terms"]
    optimized_terms = optimized_json["terms"]["interactions"]

    non_trivial_term_count = 0

    non_trivial_term_list = []

    for termRound in optimized_terms:
        non_trivial_term_count += len(termRound)
        non_trivial_term_list.extend(termRound)

    assert len(input_terms) == non_trivial_term_count, "# of terms do not match"
    print(non_trivial_term_count)

    assert _equal_ignore_order(input_terms, non_trivial_term_list)

    input_json_file.close()
    optimized_json_file.close()
