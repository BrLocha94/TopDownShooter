using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Action onCountEndEvent;

    [Header("External references")]
    [SerializeField]
    private Text targetText;
    [SerializeField]
    private PulseBehaviour pulseBehaviour;

    [Header("Timer general configs")]
    [SerializeField]
    private Color defaultColor = Color.white;
    [SerializeField]
    private Color mediumColor = Color.yellow;
    [SerializeField]
    private Color clearColor = Color.green;

    private float totalTime = 0f;
    private float currentTime = 0f;

    private bool counting = false;
    private TimerState currentState = TimerState.Null;

    public void Initialize(float totalTime)
    {
        counting = false;
        currentState = TimerState.Null;

        this.totalTime = totalTime;
        currentTime = totalTime;

        UpdateTime(currentTime);
    }

    public void StopTimer()
    {
        counting = false;
    }

    public void CountTime()
    {
        counting = true;
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        currentState = TimerState.Null;
    }

    private void Update()
    {
        if (!counting) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            UpdateTime(currentTime);
            counting = false;
            onCountEndEvent?.Invoke();
            return;
        }
        else
            UpdateTime(currentTime);
    }

    public void UpdateTime(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        int miliseconds = Mathf.FloorToInt((currentTime * 100) % 100);

        string time = string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, miliseconds);

        targetText.text = time;

        if(currentTime > totalTime/4)
        {
            if (currentState != TimerState.Default)
            {
                targetText.color = defaultColor;
                currentState = TimerState.Default;
            }

            return;
        }

        if (currentTime > 0)
        {
            if (currentState != TimerState.Medium)
            {
                targetText.color = mediumColor;
                currentState = TimerState.Medium;
                pulseBehaviour.StartPulse();
            }

            return;
        }

        if (currentTime <= 0)
        {
            if (currentState != TimerState.Clear)
            {
                targetText.color = clearColor;
                currentState = TimerState.Clear;
                pulseBehaviour.StopPulse();
            }

            return;
        }
    }
}