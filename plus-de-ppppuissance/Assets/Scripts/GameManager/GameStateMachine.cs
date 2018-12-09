using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    GameMenu,
    InGame,
    Restarting,
    Exiting, 
}

public enum GameCommand
{
    Start,
    Back
}

public class GameStateMachine : StateMachine<GameState, GameCommand>
{
    public GameStateMachine() : base(GameState.InGame)
    {
        transitions.Add(new StateTransition<GameState, GameCommand>     (GameState.MainMenu,    GameCommand.Start), GameState.InGame);
        transitions.Add(new StateTransition<GameState, GameCommand>     (GameState.MainMenu,    GameCommand.Back),  GameState.Exiting);
        transitions.Add(new StateTransition<GameState, GameCommand>     (GameState.InGame,      GameCommand.Start), GameState.GameMenu);
        transitions.Add(new StateTransition<GameState, GameCommand>     (GameState.InGame,      GameCommand.Back),  GameState.Restarting);
        transitions.Add(new StateTransition<GameState, GameCommand>     (GameState.GameMenu,    GameCommand.Start), GameState.InGame);
        transitions.Add(new StateTransition<GameState, GameCommand>     (GameState.GameMenu,    GameCommand.Back),  GameState.MainMenu);
    }
}