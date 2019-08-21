
import json


def test_input_json_matches_optimized():
    input_json_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/TestPipeline/_temp/extracted_terms.json"
    optimized_json_path = "/Users/kang828/Documents/GitHub/QUASAR-Summer19/TestPipeline/_temp/reconstructed.json"

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
    optimized_terms = input_json["terms"]

    non_trivial_term_count = 0

    for term in optimized_terms:
        if term["type"] == "SWAP":
            pass
        else:
            non_trivial_term_count += 1

    assert len(input_terms) == non_trivial_term_count, "# of terms do not match"

    # assert not input_terms, "Mismatch between input and optimized terms"

    input_json_file.close()
    optimized_json_file.close()
