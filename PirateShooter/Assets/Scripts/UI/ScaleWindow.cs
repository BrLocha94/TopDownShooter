using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleWindow : WindowBase
{
    [SerializeField]
    private RectTransform targetRect;
    [SerializeField]
    private Vector3 activateScale = Vector3.one;
    [SerializeField]
    private Vector3 deactivateScale = Vector3.zero;

    public override void Activate(Action callback = null)
    {
        targetRect.transform.localScale = activateScale;
        gameObject.SetActive(true);

        callback?.Invoke();
    }

    public override void Deactivate(Action callback = null)
    {
        targetRect.transform.localScale = deactivateScale;
        gameObject.SetActive(false);

        callback?.Invoke();
    }
}
