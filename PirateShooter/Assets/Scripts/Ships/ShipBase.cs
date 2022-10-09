using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ShipBase : MonoBehaviour
{
    [Header("External References")]
    [SerializeField]
    protected Animator shipAnimator;
    [SerializeField]
    protected Collider2D shipCollider;
    [SerializeField]
    private Explosion explosionPrefab;

    [Header("Ship base config")]
    [SerializeField]
    protected float shipMovimentSpeed;
    [SerializeField]
    protected float shipRotationSpeed;
    [SerializeField]
    protected float shipBaseLife;
    [SerializeField]
    protected float shipBaseDamage;

    [Header("Ship events")]
    [SerializeField]
    private UnityEvent onShipDestroyed;

    protected bool isAlive = false;
    protected float currentLife;

    public virtual void Initialize()
    {
        currentLife = shipBaseLife;
        shipCollider.enabled = true;
        shipAnimator.Play("idle");
        isAlive = true;
    }

    public virtual void TakeDamage(float damage)
    {
        currentLife -= damage;

        if (currentLife < 0)
            currentLife = 0;

        //TODO: Update Health bar

        if (currentLife <= 0f)
        {
            Explosion explosion = Instantiate(explosionPrefab);
            explosion.Initialize(transform.position);

            shipAnimator.Play("damage_lv_03");
            shipCollider.enabled = false;
            isAlive = false;

            onShipDestroyed?.Invoke();
            return;
        }

        else if (currentLife <= shipBaseLife * 25 / 100)
            shipAnimator.Play("damage_lv_02");

        else if (currentLife <= shipBaseLife * 60 / 100)
            shipAnimator.Play("damage_lv_01");

        else
            shipAnimator.Play("idle");
    }
}
