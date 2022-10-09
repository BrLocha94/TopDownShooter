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
    [SerializeField]
    private CountdownWindow countdownWindow;

    private static bool hasAlreadyPlayed = false;

    private void OnEnable()
    {
        countdownWindow.onCountDownFinish += OnCountdownFinish;
        timer.onCountEndEvent += OnTimerEnd;
    }

    private void OnDisable()
    {
        countdownWindow.onCountDownFinish -= OnCountdownFinish;
        timer.onCountEndEvent -= OnTimerEnd;
    }

    private void OnCountdownFinish()
    {
        countdownWindow.Deactivate();
        this.InvokeAfterFrame(() => StateMachineController.ExecuteTransition(GameState.Running));
    }

    private void OnTimerEnd()
    {
        StateMachineController.ExecuteTransition(GameState.GameClear);
        return;
    }

    public void SetTimerValues(float value)
    {
        timer.Initialize(value);
    }

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

            countdownWindow.Activate();
            return;
        }

        if (updatedValue == GameState.Running)
        {
            pauseWindow.Deactivate();
            timer.CountTime();
        }

        if (updatedValue == GameState.Paused)
        {
            pauseWindow.Activate();
            timer.StopTimer();
        }

        if(updatedValue == GameState.GameClear || updatedValue == GameState.GameOver)
        {
            timer.StopTimer();
        }
    }
}
