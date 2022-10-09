using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 7;
    [SerializeField]
    private float lifeTime = 3;

    private Coroutine coroutine = null;
    private float damage = 0f;

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
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    protected IEnumerator LifeCountdown(float lifeTime)
    {
        float time = 0;

        while(time < lifeTime)
        {
            time += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
