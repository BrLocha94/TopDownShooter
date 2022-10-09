using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserShip : EnemyShip
{
    [Header("Enemy chaser config")]
    [SerializeField]
    private float minDistance = 3f;
    [SerializeField]
    private float speedModificator = 2f;

    protected override void ExecuteOnFixedUpdate()
    {
        base.ExecuteOnFixedUpdate();

        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < minDistance)
            {
                moveVector *= speedModificator;
            }
        }
    }
}
