using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownWindow : ScaleWindow
{
    public Action onCountDownFinish;

    [Header("External references")]
    [SerializeField]
    private Text targetText;

    [Header("Text configs")]
    [SerializeField]
    private List<string> textList = new List<string>();

    Coroutine coroutine;

    public override void Activate()
    {
        base.Activate();

        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(CountDownRoutine());
    }

    public override void Deactivate()
    {
        base.Deactivate();

        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    IEnumerator CountDownRoutine()
    {
        float timer = 4;

        while(timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer > 2.5)
                targetText.text = textList[2];
            else if (timer > 1)
                targetText.text = textList[1];
            else
                targetText.text = textList[0];

            yield return null;
        }

        onCountDownFinish?.Invoke();
    }
}
