using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : ShipBase, IReceiver<GameState>
{
    public delegate void OnEnemyShipDestroyedHandler();
    public event OnEnemyShipDestroyedHandler onEnemyShipDestroyed;

    [Header("Enemy ship configs")]
    [SerializeField]
    protected Transform target;

    protected Vector3 defaultRotation;

    protected Vector3 moveVector = Vector3.zero;
    protected Vector3 rotationVector = Vector3.zero;

    protected float angle = 0f;
    protected float offset = 0f;

    bool initializedRotation = false;
    bool canMoveFoward = true;

    bool canAct = true;

    public override void Initialize()
    {
        base.Initialize();

        target = null;
        onEnemyShipDestroyed = null;

        if (!initializedRotation)
        {
            initializedRotation = true;
            defaultRotation = transform.rotation.eulerAngles;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void EnemyShipDestroyed()
    {
        onEnemyShipDestroyed?.Invoke();

        this.Invoke(2f, () => {
            Destroy(gameObject);
        });
    }

    private void FixedUpdate()
    {
        if (!isAlive || !canAct) return;

        moveVector = Vector3.zero;
        rotationVector = Vector3.zero;

        ExecuteOnFixedUpdate();
    }

    private void Update()
    {
        if (!isAlive || !canAct) return;

        ExecuteOnUpdate();
    }

    protected virtual void ExecuteOnFixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            offset = 90f;

            if(canMoveFoward)
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
        TakeDamage(shipBaseLife);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Obstacle"))
        {
            canMoveFoward = false;

            return;
        }

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Obstacle"))
        {
            canMoveFoward = true;

            return;
        }
    }

    public void ReceiveUpdate(GameState updatedValue)
    {
        if (updatedValue == GameState.Null || updatedValue == GameState.Running)
            canAct = true;
        else
            canAct = false;
    }
}
