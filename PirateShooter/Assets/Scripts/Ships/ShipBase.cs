using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipBase : MonoBehaviour
{
    [Header("External References")]
    [SerializeField]
    protected Animator shipAnimator;

    [Header("Ship base config")]
    [SerializeField]
    protected float shipMovimentSpeed;
    [SerializeField]
    protected float shipRotationSpeed;
    [SerializeField]
    protected float shipBaseLife;
    [SerializeField]
    protected float shipBaseDamage;

    protected float currentLife;

    public virtual void Initialize()
    {
        currentLife = shipBaseLife;
        shipAnimator.Play("idle");
    }

    public virtual void TakeDamage(float damage)
    {
        currentLife -= damage;

        if (currentLife <= 0f)
            shipAnimator.Play("damage_lv_03");

        else if (currentLife <= shipBaseLife * 25/100)
            shipAnimator.Play("damage_lv_02");

        else if (currentLife <= shipBaseLife * 60/100)
            shipAnimator.Play("damage_lv_01");

        else
            shipAnimator.Play("idle");
    }
}
