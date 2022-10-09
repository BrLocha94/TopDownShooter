using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateButton : MonoBehaviour
{
    [SerializeField]
    private GameState targetState;

    public void OnClick()
    {
        StateMachineController.ExecuteTransition(targetState);
    }
}
