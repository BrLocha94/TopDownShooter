using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterShip : EnemyShip
{
    [Header("Enemy shooter config")]
    [SerializeField]
    private float minDistance = 1f;

    [Header("Enemy shooter")]
    [SerializeField]
    private ShooterBehaviour shooterBehaviour;

    public override void Initialize()
    {
        base.Initialize();

        shooterBehaviour.ResetShooter(false);
    }

    protected override void ExecuteOnFixedUpdate()
    {
        base.ExecuteOnFixedUpdate();

        if(target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < minDistance)
                moveVector = Vector3.zero;
        }
    }

    protected override void ExecuteOnUpdate()
    {
        base.ExecuteOnUpdate();

        if(target != null)
            shooterBehaviour.ExecuteShoot();
    }
}
