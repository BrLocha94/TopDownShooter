using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
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

    [Header("Spawn configs")]
    [SerializeField]
    private float defaultSpawnTime = 8;

    private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;

        if(time >= defaultSpawnTime)
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
        enemyShip.SetTarget(target);
    }
}
