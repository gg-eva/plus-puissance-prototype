using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : MonoBehaviour {

    GameManager gameManager;
    MoveAvatar moveAvatar;
    MoveSelection moveSelection;
    CreatureManager creatureManager;

	void Start () {

        GameObject go_gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = go_gameManager.GetComponent<GameManager>();

        GameObject go_avatar = GameObject.FindGameObjectWithTag("Player");
        moveAvatar = go_avatar.GetComponent<MoveAvatar>();
        creatureManager = go_avatar.GetComponent<CreatureManager>();

        GameObject go_selection = GameObject.FindGameObjectWithTag("Selection");
        moveSelection = go_selection.GetComponent<MoveSelection>();
    }
	

	void Update () {
        gameManager.input_start = Input.GetKeyDown(InputsMapping.Button(XboxInputButton.StartButton));
        gameManager.input_back = Input.GetKeyDown(InputsMapping.Button(XboxInputButton.BackButton));

        moveAvatar.input_forward = Input.GetAxis(InputsMapping.Axis(XboxInputAxis.LeftStickY));
        moveAvatar.input_right = Input.GetAxis(InputsMapping.Axis(XboxInputAxis.LeftStickX));
        moveAvatar.input_dash = Input.GetKeyDown(InputsMapping.Button(XboxInputButton.ButtonA));

        moveSelection.input_lock = Input.GetKeyDown(InputsMapping.Button(XboxInputButton.RightStickClick));
        moveSelection.input_forward = Input.GetAxis(InputsMapping.Axis(XboxInputAxis.RightStickY));
        moveSelection.input_right = Input.GetAxis(InputsMapping.Axis(XboxInputAxis.RightStickX));

        creatureManager.input_dispatch = Input.GetAxis(InputsMapping.Axis(XboxInputAxis.RightTrigger));
        creatureManager.input_regroup = Input.GetAxis(InputsMapping.Axis(XboxInputAxis.LeftTrigger));
    }
}
