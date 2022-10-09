using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : ShipBase
{
    [Header("Enemy ship configs")]
    [SerializeField]
    protected Transform target;

    protected Vector3 defaultRotation;

    protected Vector3 moveVector = Vector3.zero;
    protected Vector3 rotationVector = Vector3.zero;

    protected float angle = 0f;
    protected float offset = 0f;

    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();

        defaultRotation = transform.rotation.eulerAngles;
    }

    private void FixedUpdate()
    {
        moveVector = Vector3.zero;
        rotationVector = Vector3.zero;

        ExecuteOnFixedUpdate();
    }

    private void Update()
    {
        ExecuteOnUpdate();
    }

    protected virtual void ExecuteOnFixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            offset = 90f;

            moveVector.y += shipMovimentSpeed;
        }
    }

    protected virtual void ExecuteOnUpdate()
    {
        if (target != null)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
            transform.Rotate(new Vector3(0f, 0f, 180f));

            transform.Translate(moveVector * Time.deltaTime);
        }
    }

    public float GetShipDamage()
    {
        return shipBaseDamage;
    }

    public void CollidedWithPlayer()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerBullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet)
            {
                TakeDamage(bullet.GetDamage());
                bullet.CollidedWithTarget();
            }

            return;
        }

    }
}
