using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OptionsSet : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onClickEvent;

    [SerializeField]
    private Slider gameTimeSlider;
    [SerializeField]
    private Text gameTimeText;
    [SerializeField]
    private Slider spawnSlider;
    [SerializeField]
    private Text spawnText;

    private int gameTime = 60;
    private int spawnTime = 8;

    public void Activate()
    {
        OnGameTimeChange();
        OnSpawnTimeChange();
    }

    public void OnGameTimeChange()
    {
        gameTime = (int)gameTimeSlider.value;
        gameTimeText.text = "Game Time: " + gameTime;
    }

    public void OnSpawnTimeChange()
    {
        spawnTime = (int)spawnSlider.value;
        spawnText.text = "Spawn Time: " + spawnTime;
    }

    public void OnClick()
    {
        PersistentData.gameTime = gameTime;
        PersistentData.gameSpawnTime = spawnTime;

        onClickEvent?.Invoke();
    }
}
