using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadOpen()
    {
        SceneManager.LoadScene("Open");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadResults()
    {
        SceneManager.LoadScene("Results");
    }
}
