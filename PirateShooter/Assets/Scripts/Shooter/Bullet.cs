using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IReceiver<GameState>
{
    [SerializeField]
    private Explosion explosionPrefab;
    [SerializeField]
    private float speed = 7;
    [SerializeField]
    private float lifeTime = 3;

    private Coroutine coroutine = null;
    private float damage = 0f;

    private bool canCount = true;

    public void Initialize(Transform spawn, float damage)
    {
        transform.parent = spawn;
        transform.localPosition = Vector3.zero;
        transform.localRotation = spawn.localRotation;
        transform.parent = null;

        this.damage = damage;

        coroutine = StartCoroutine(LifeCountdown(lifeTime));
    }

    public float GetDamage() => damage;

    public void CollidedWithTarget()
    {
        Explosion explosion = Instantiate(explosionPrefab);
        explosion.Initialize(transform.position);

        Destroy(gameObject);
    }

    private void Update()
    {
        if (!canCount) return;

        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    protected IEnumerator LifeCountdown(float lifeTime)
    {
        float time = 0;

        while(time < lifeTime)
        {
            if(canCount)
                time += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

    public void ReceiveUpdate(GameState updatedValue)
    {
        if (updatedValue == GameState.Null || updatedValue == GameState.Running)
            canCount = true;
        else
            canCount = false;
    }
}
