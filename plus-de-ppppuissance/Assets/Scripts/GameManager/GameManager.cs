using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public bool input_start, input_back = false;

    [HideInInspector]
    public GameStateMachine gameStateMachine = new GameStateMachine();

	void Update () {
        UpdateStateMachine();

        //if (gameStateMachine.currentState == GameState.MainMenu)
        //    Debug.Log("MainMenuLoop");
        //if (gameStateMachine.currentState == GameState.InGame)
        //    Debug.Log("InGameLoop");
        //if (gameStateMachine.currentState == GameState.GameMenu)
        //    Debug.Log("GameMenuLoop");
        if (gameStateMachine.currentState == GameState.Exiting)
            Application.Quit();
	}

    void UpdateStateMachine()
    {
        if (input_start)
            gameStateMachine.MoveNext(GameCommand.Start);

        if (input_back)
            gameStateMachine.MoveNext(GameCommand.Back);
    }
}
