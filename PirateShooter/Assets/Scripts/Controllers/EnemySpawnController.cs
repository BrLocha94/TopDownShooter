using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour, IReceiver<GameState>
{
    [Header("External references")]
    [SerializeField]
    private Transform target;

    [Header("Ship spawn positions references")]
    [SerializeField]
    private List<Transform> spawnPositions = new List<Transform>();

    [Header("Ships prefabs references")]
    [SerializeField]
    private List<EnemyShip> enemyShips = new List<EnemyShip>();

    public float spawnTime { get; set; } = 8;

    private float time = 0;
    private bool canCount = true;

    private void Update()
    {
        if (!canCount) return;

        time += Time.deltaTime;

        if(time >= spawnTime)
        {
            SpawnShip();
            time = 0f;
        }
    }

    private void SpawnShip()
    {
        int randomPosition = Random.Range(0, spawnPositions.Count);

        int randomShip = Random.Range(0, enemyShips.Count);

        EnemyShip enemyShip = Instantiate(enemyShips[randomShip], spawnPositions[randomPosition].position, Quaternion.identity);
        enemyShip.Initialize();
        enemyShip.onEnemyShipDestroyed += OnEnemyShipDestroyed;
        enemyShip.SetTarget(target);
    }

    private void OnEnemyShipDestroyed()
    {
        PersistentData.DestroyedShip();
    }

    public void ReceiveUpdate(GameState updatedValue)
    {
        if (updatedValue == GameState.Null || updatedValue == GameState.Running)
            canCount = true;
        else
            canCount = false;
    }
}
