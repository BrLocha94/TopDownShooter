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

    public void Initialize(float totalTime)
    {
        counting = false;

        this.totalTime = totalTime;
        currentTime = totalTime;
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
    }

    private void Update()
    {
        if (!counting) return;

        // TODO: UpdateTime

        // TODO: Check time to change color or start pulse
    }
}
