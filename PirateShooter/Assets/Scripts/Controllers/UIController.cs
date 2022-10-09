using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour, IReceiver<GameState>
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private WindowBase pauseWindow;
    [SerializeField]
    private WindowBase introWindow;

    private static bool hasAlreadyPlayed = false;

    public void ReceiveUpdate(GameState updatedValue)
    {
        if(updatedValue == GameState.Initializing)
        {
            pauseWindow.Deactivate();

            if (hasAlreadyPlayed)
                this.InvokeAfterFrame(() => StateMachineController.ExecuteTransition(GameState.Counting));
            else
            {
                hasAlreadyPlayed = true;
                introWindow.Activate();
            }

            return;
        }

        if(updatedValue == GameState.Counting)
        {
            introWindow.Deactivate();

            //TODO: Call count routine
            return;
        }

        if (updatedValue == GameState.Running)
        {
            pauseWindow.Deactivate();
        }

        if (updatedValue == GameState.Paused)
        {
            pauseWindow.Activate();
        }
    }
}
