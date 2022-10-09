using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float lifeTime = 0.2f;

    Coroutine coroutine = null;

    private void Awake()
    {
        Initialize(transform.position);
    }

    public void Initialize(Vector3 position)
    {
        transform.localPosition = position;

        if (coroutine != null)
            StopCoroutine(coroutine);

        animator.Play("idle");

        StartCoroutine(LifeCountdown(lifeTime));
    }

    protected IEnumerator LifeCountdown(float lifeTime)
    {
        float time = 0;

        while (time < lifeTime)
        {
            time += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
