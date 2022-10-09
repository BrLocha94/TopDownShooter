using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBehaviour : MonoBehaviour, IReceiver<GameState>
{
    [SerializeField]
    private AnimationCurve pulseCurve;
    [SerializeField]
    private float pulseTime;
    [SerializeField]
    private float pulseRange;

    private Vector3 defaultScale;
    private bool canPulse = true;

    Coroutine pulseRoutine;

    private void Awake()
    {
        defaultScale = transform.localScale;
    }

    public void StartPulse()
    {
        if (pulseRoutine != null)
            StopCoroutine(pulseRoutine);

        transform.localScale = defaultScale;

        Vector3 targetScale = defaultScale * pulseRange;

        pulseRoutine = StartCoroutine(PulseRoutine(defaultScale, targetScale, pulseCurve, pulseTime));
    }

    public void StopPulse()
    {
        if (pulseRoutine != null)
            StopCoroutine(pulseRoutine);

        transform.localScale = defaultScale;
    }

    private IEnumerator PulseRoutine(Vector3 initialScale, Vector3 finalScale, AnimationCurve curve, float time)
    {
        bool loop = true;
        float pulseTimer = 0f;

        while (loop)
        {
            while (pulseTimer < time)
            {
                if (!canPulse)
                {
                    yield return null;
                    continue;
                }

                transform.localScale = Vector3.Lerp(initialScale, finalScale, curve.Evaluate(pulseTimer / time));
                pulseTimer += Time.deltaTime;
                yield return null;
            }

            transform.localScale = initialScale;
            pulseTimer = 0f;
        }
    }

    public void ReceiveUpdate(GameState updatedValue)
    {
        if (updatedValue == GameState.Running)
            canPulse = true;
        else
            canPulse = false;
    }
}
