using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;
    public Scene_Fader fader;
    public void PlayGame()
    {
        fader.FadeTo(sceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
