using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PreGame,
    Playing,
    Paused,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    public static bool gameisOver;
    public UI_Manager ui_Manager;

    public string nextLevel = "Level_02";
    public int levelToUnlock = 2;
    public Scene_Fader fader;


    private void Start()
    {
        ui_Manager = GetComponent<UI_Manager>();
        gameisOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }

        if(PlayerStats.lives <= 0)
        {
            EndGame();
        }   
    }



    void EndGame()
    {
        ui_Manager.gameOverUI.SetActive(true);
        print("Game Over");
    }

    public void WinLevel()
    {
        print("level Won");
        PlayerPrefs.SetInt("leverReached",levelToUnlock);
        fader.FadeTo(nextLevel);
    }
}
