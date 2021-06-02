using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject UI;
    public string sceneRetry;
    public string sceneQuit;
    public Scene_Fader fader;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Resume()
    {
        Toggle();
    }

    public void Toggle()
    {
        UI.SetActive(!UI.activeSelf);

        if (UI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        Spawner.enemiesAlive = 0;
        PlayerStats.rounds = 0;

        fader.FadeTo(sceneRetry);
    }

    public void QuitGame()
    {
        Toggle();
        fader.FadeTo(sceneQuit);
    }
}
