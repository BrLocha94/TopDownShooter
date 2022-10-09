using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehaviour : MonoBehaviour, IReceiver<GameState>
{
    [Header("External references")]
    [SerializeField]
    protected Bullet bulletPrefab;
    [SerializeField]
    protected Transform spawnPosition;

    [Header("Shoot configs")]
    [SerializeField]
    protected float cooldownTime = 3f;
    [SerializeField]
    protected float bulletDamage = 10f;

    protected bool canShoot = true;
    protected Coroutine coroutine;

    protected bool canCount = true;

    public virtual void ResetShooter(bool canShootOnStart)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        canShoot = canShootOnStart;

        if(!canShootOnStart)
            coroutine = StartCoroutine(CooldownRoutine(cooldownTime));
    }

    public virtual void ExecuteShoot()
    {
        if (!canShoot) return;

        canShoot = false;

        Bullet bullet = Instantiate(bulletPrefab);
        bullet.Initialize(spawnPosition, bulletDamage);

        coroutine = StartCoroutine(CooldownRoutine(cooldownTime));
    }

    protected IEnumerator CooldownRoutine(float cooldownTime)
    {
        float time = 0f;

        while(time < cooldownTime)
        {
            if(canCount)
                time += Time.deltaTime;

            yield return null;
        }

        canShoot = true;
    }

    public void ReceiveUpdate(GameState updatedValue)
    {
        if (updatedValue == GameState.Null || updatedValue == GameState.Running)
            canCount = true;
        else
            canCount = false;
    }
}
