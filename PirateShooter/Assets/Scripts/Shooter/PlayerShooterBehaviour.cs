using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterBehaviour : ShooterBehaviour
{
    [Header("Triple shoot positions")]
    [SerializeField]
    protected Transform leftSpawnPosition;
    [SerializeField]
    protected Transform centerSpawnPosition;
    [SerializeField]
    protected Transform rightSpawnPosition;
    [SerializeField]
    protected float tripleBulletDamage = 10f;

    public void ExecuteTripleShoot()
    {
        // TODO: CHECK AMMO
        if (!canShoot) return;

        canShoot = false;

        Bullet bullet = Instantiate(bulletPrefab);
        bullet.Initialize(centerSpawnPosition, tripleBulletDamage);

        Bullet bulletLeft = Instantiate(bulletPrefab);
        bulletLeft.Initialize(leftSpawnPosition, tripleBulletDamage);

        Bullet bulletRight = Instantiate(bulletPrefab);
        bulletRight.Initialize(rightSpawnPosition, tripleBulletDamage);

        coroutine = StartCoroutine(CooldownRoutine(cooldownTime));
    }
}
