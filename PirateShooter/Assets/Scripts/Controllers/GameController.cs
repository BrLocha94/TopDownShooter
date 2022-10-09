using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IReceiver<GameState>
{
    [SerializeField]
    private SceneChanger sceneChanger;
    [SerializeField]
    private EnemySpawnController enemySpawnController;

    private void Awake()
    {
        StateMachineController.InitializeStateMachine();
        enemySpawnController.spawnTime = PersistentData.gameSpawnTime;
        PersistentData.ResetShipCount();
    }

    private void Start()
    {
        this.InvokeAfterFrame(() => StateMachineController.ExecuteTransition(GameState.Initializing));
    }

    public void OnPlayerShipDestroyed()
    {
        this.InvokeAfterFrame(() => StateMachineController.ExecuteTransition(GameState.GameOver));
    }

    public void ReceiveUpdate(GameState updatedValue)
    {
        if (updatedValue == GameState.GameOver || updatedValue == GameState.GameClear)
        {
            this.Invoke(2f, () => sceneChanger.LoadResults());

            return;
        }
    }
}
