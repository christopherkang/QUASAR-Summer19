import auxiliary
import json


# pull input JSON path from auxiliary, then fetch the file
input_json_path = auxiliary.input_json_path
input_json = json.load(open(input_json_path, "r"))


def test_verify_all_constants_present():
    """Verify that all necessary constants are present in the input JSON, including:
    - trotterStep (double between 0 and 1)
    - trotterOrder (either 1 or 2)
    - nSpinOrbitals (int between 1 to 100)
    - energyOffset (exists)
    """
    constants = input_json["constants"]

    assert 1 >= constants["trotterStep"] > 0, "Incorrect TrotterStep"

    assert constants["trotterOrder"] in range(
        1, 2), "Trotter order is non integer or too big/small"

    assert constants["nSpinOrbitals"] in range(
        1, 100), "Number of spin orbitals is too big"

    assert "energyOffset" in constants.keys(), "No valid energy offset detected"


def test_verify_all_gates_have_valid_targets():
    """Verifies that all gate targets have targets between 0 and nSpinOrbitals - 1
    """
    nSpinOrbitals = input_json["constants"]["nSpinOrbitals"]

    interaction_list = input_json["terms"]

    for interaction in interaction_list:
        targets = interaction["targets"]

        for orbital in targets:
            assert 0 <= orbital < nSpinOrbitals, "Orbital target is out of range"


def test_verify_state_prep_data():
    state_prep_data = input_json["statePrepData"]

    assert type(state_prep_data["int"]
                ) is int, "StatePrepData's int is not an int"

    assert state_prep_data["terms"], "State data has no tuple data"
