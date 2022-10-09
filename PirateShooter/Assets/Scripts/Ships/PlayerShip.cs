using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : ShipBase
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

    private void Awake()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        moveVector = Vector3.zero;

        if (Input.GetKey(frontMoveKey))
        {
            moveVector.y += shipMovimentSpeed;
        }
        if (Input.GetKey(backMoveKey))
        {
            moveVector.y -= shipMovimentSpeed;
        }

        rotationVector = Vector3.zero;

        if (Input.GetKey(rightRotationKey))
            rotationVector.z += shipRotationSpeed;
        if (Input.GetKey(leftRotationKey))
            rotationVector.z -= shipRotationSpeed;
    }

    private void Update()
    {
        transform.Translate(moveVector * Time.deltaTime);
        transform.Rotate(rotationVector * Time.deltaTime);

        if (Input.GetKey(shootKey))
            shooterBehaviour.ExecuteShoot();

        else if (Input.GetKey(tripleShootKey))
            shooterBehaviour.ExecuteTripleShoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
}
