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
    [SerializeField]
    private LifeBar lifebarPrefab;

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

    protected LifeBar lifeBar;

    public virtual void Initialize()
    {
        currentLife = shipBaseLife;

        if (lifeBar == null)
        {
            lifeBar = Instantiate(lifebarPrefab);
            lifeBar.Initialize(transform, currentLife);
        }

        shipCollider.enabled = true;
        shipAnimator.Play("idle");
        isAlive = true;
    }

    public virtual void TakeDamage(float damage)
    {
        currentLife -= damage;

        if (currentLife < 0)
            currentLife = 0;

        lifeBar.SetLife(currentLife);

        if (currentLife <= 0f)
        {
            Explosion explosion = Instantiate(explosionPrefab);
            explosion.Initialize(transform.position);

            shipAnimator.Play("damage_lv_03");
            shipCollider.enabled = false;
            isAlive = false;

            lifeBar.DestroyBar();

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
