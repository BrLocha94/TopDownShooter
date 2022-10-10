using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [Header("External references")]
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text messageText;

    private void Awake()
    {
        bool gameClear = StateMachineController.gameState == GameState.GameClear;

        titleText.text = gameClear ? "CONGRATULATIONS!!!" : "GAME OVER";

        string message = string.Empty;

        if (gameClear)
            message = "You survived the battle and sank " + PersistentData.shipsDestroyed + " ships!";
        else
            message = "You fought bravely, sank " + PersistentData.shipsDestroyed + " ships, but didn't survived...";

        messageText.text = message;
    }
}
