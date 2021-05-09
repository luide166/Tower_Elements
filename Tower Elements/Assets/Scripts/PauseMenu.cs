using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public string sceneRetry;
    public string sceneQuit;
    public Scene_Fader fader;

    public void Resume()
    {
        Time.timeScale = 1f;
        this.enabled = false;
    }

    public void Retry()
    {
        Resume();
        Spawner.enemiesAlive = 0;
        fader.FadeTo(sceneRetry);
    }

    public void QuitGame()
    {
        fader.FadeTo(sceneQuit);
    }
}
