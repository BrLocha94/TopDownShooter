using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateMachineController
{
    public delegate void OnGameStateChangeHandler(GameState gameState);
    public static event OnGameStateChangeHandler onGameStateChangeEvent;

    private static GameState currentGameState = GameState.Null;

    public static GameState gameState
    {
        get
        {
            return currentGameState;
        }
        private set
        {
            currentGameState = value;
            onGameStateChangeEvent?.Invoke(currentGameState);
        }
    }

    private static bool CheckCurrentState(GameState gameState) => currentGameState == gameState;

    public static void InitializeStateMachine()
    {
        gameState = GameState.Null;
    }

    public static void ExecuteTransition(GameState nextState)
    {
        if (!CheckTransition(nextState))
        {
            Debug.Log("Unauthorized transition: " + currentGameState + " to " + nextState);
            return;
        }
        
        gameState = nextState;
    }

    private static bool CheckTransition(GameState nextState)
    {
        switch (currentGameState)
        {
            case GameState.Null:
                if (nextState == GameState.Initializing) return true;
                break;
            case GameState.Initializing:
                if (nextState == GameState.Running) return true;
                break;
            case GameState.Running:
                if (nextState == GameState.Paused) return true;
                if (nextState == GameState.GameClear) return true;
                if (nextState == GameState.GameOver) return true;
                break;
            case GameState.Paused:
                if (nextState == GameState.Running) return true;
                break;
            case GameState.GameClear:
                if (nextState == GameState.Initializing) return true;
                break;
            case GameState.GameOver:
                if (nextState == GameState.Initializing) return true;
                break;
        }

        return false;
    }
}
