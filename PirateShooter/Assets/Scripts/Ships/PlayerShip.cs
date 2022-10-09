using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : ShipBase, IReceiver<GameState>
{
    [Header("Player movement keymap")]
    [SerializeField]
    private KeyCode frontMoveKey = KeyCode.W;
    [SerializeField]
    private KeyCode backMoveKey = KeyCode.S;

    [Space]
    [SerializeField]
    private KeyCode leftRotationKey = KeyCode.A;
    [SerializeField]
    private KeyCode rightRotationKey = KeyCode.D;

    [Header("Player shooter")]
    [SerializeField]
    private PlayerShooterBehaviour shooterBehaviour;
    [SerializeField]
    private KeyCode shootKey = KeyCode.P;
    [SerializeField]
    private KeyCode tripleShootKey = KeyCode.K;

    private Vector3 moveVector = Vector3.zero;
    private Vector3 rotationVector = Vector3.zero;

    bool canMoveFoward = true;
    bool canMoveBackwards = true;

    bool canAct = true;

    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();

        shooterBehaviour.ResetShooter(true);
    }

    private void FixedUpdate()
    {
        moveVector = Vector3.zero;

        if (!isAlive || !canAct) return;

        if (Input.GetKey(frontMoveKey) && canMoveFoward)
            moveVector.y += shipMovimentSpeed;
        else if (Input.GetKey(backMoveKey) && canMoveBackwards)
            moveVector.y -= shipMovimentSpeed;

        rotationVector = Vector3.zero;

        if (Input.GetKey(rightRotationKey))
            rotationVector.z += shipRotationSpeed;
        if (Input.GetKey(leftRotationKey))
            rotationVector.z -= shipRotationSpeed;
    }

    private void Update()
    {
        if (!isAlive || !canAct) return;

        transform.Translate(moveVector * Time.deltaTime);
        transform.Rotate(rotationVector * Time.deltaTime);

        if (Input.GetKey(shootKey))
            shooterBehaviour.ExecuteShoot();

        else if (Input.GetKey(tripleShootKey))
            shooterBehaviour.ExecuteTripleShoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Obstacle"))
        {
            if (moveVector.y > 0)
                canMoveFoward = false;
            else if (moveVector.y < 0)
                canMoveBackwards = false;

            return;
        }

        if (collision.tag.Equals("Enemy"))
        {
            EnemyShip enemy = collision.GetComponent<EnemyShip>();
            if (enemy)
            {
                TakeDamage(enemy.GetShipDamage());
                enemy.CollidedWithPlayer();
            }

            return;
        }
        if (collision.tag.Equals("EnemyBullet"))
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
            canMoveBackwards = true;

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
