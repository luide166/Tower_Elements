using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    //spawn de Waves
    [Header("Spawn Stats")]
    public Transform spawnPoint;
    public float timeBetweenWaves = 12f;
    private float countdown = 2f;
    public static int enemiesAlive = 0;
    public GameManager gameManager;

    [Header("Spawn UI")]
    [Space(3)]
    public Text waveCountDownText;

    [Header("Waves Configs")]
    [Space(3)]
    public bool autoRoundChange = false;
    public WaveBluePrint[] wavesCount;


    //logica das waves
    private int waveIndex = 0;
    WaveBluePrint Wave;

    private void Awake()
    {
        countdown = timeBetweenWaves;
    }

    private void Update()
    {
        print(enemiesAlive);

        if (enemiesAlive > 0)
        {
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00.00}", countdown);

        if (countdown <= 0f) // se o cronometro zerar
        {
            print("zerou o cronometro");

            if (!PassRound())// e não estiver ativado o passador de Round
            {
                print("não pode passar automatico");
                return;
            }
            if (enemiesAlive > 0)// e se tiver algum inimigo vivo
            {
                print("tem inimigos vivos");
                return;
            }
            else
            {
                print("tudo ok, spawna memo");
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }
        }
    }

    public bool PassRound()
    {


        return true;
    }

    IEnumerator SpawnWave()
    {
        WaveBluePrint Wave = wavesCount[waveIndex];
        Wave.count = Wave.enemy1Count + Wave.enemy2Count + Wave.enemy3Count + Wave.enemy4Count;
        enemiesAlive = Wave.count;

        for (int i = 0; i < Wave.count; i++)
        {
            switch (Wave.enemyTypes) //verifica quantos tipos de inimgos eu tenho;
            {
                #region One Enemy Type
                case 1:
                    if (Wave.enemy1Count > 0) //se tem inimigos 1 ainda
                    {
                        SpawnEnemy(Wave.enemy1);
                        Wave.enemy1Count--;
                    }
                    break;
                #endregion

                #region Two Enemies Types
                case 2:
                    if (Wave.enemy1Count > 0) //se tem inimigo 1 ainda
                    {
                        SpawnEnemy(Wave.enemy1);
                        Wave.enemy1Count--;
                    }
                    else if (Wave.enemy2Count > 0 && Wave.enemy1Count <= 0) //se tem inimigo 2 ainda
                    {
                        SpawnEnemy(Wave.enemy2);
                        Wave.enemy2Count--;
                    }
                    break;
                #endregion

                #region Three Enemies Types
                case 3:
                    if (Wave.enemy1Count > 0) //se tem inimigo 1 ainda
                    {
                        SpawnEnemy(Wave.enemy1);
                        Wave.enemy1Count--;
                    }
                    else if (Wave.enemy2Count > 0 && Wave.enemy1Count <= 0) //se tem inimigo 2 ainda
                    {
                        SpawnEnemy(Wave.enemy2);
                        Wave.enemy2Count--;
                    }
                    else if (Wave.enemy3Count > 0 && Wave.enemy2Count <= 0) //se tem inimigo 3 ainda
                    {
                        SpawnEnemy(Wave.enemy3);
                        Wave.enemy3Count--;
                    }

                    break;
                #endregion

                #region Four Enemies Types
                case 4:
                    if (Wave.enemy1Count > 0) //se tem inimigo 1 ainda
                    {
                        SpawnEnemy(Wave.enemy1);
                        Wave.enemy1Count--;
                    }
                    else if (Wave.enemy2Count > 0 && Wave.enemy1Count <= 0) //se tem inimigo 2 ainda
                    {
                        SpawnEnemy(Wave.enemy2);
                        Wave.enemy2Count--;
                    }
                    else if (Wave.enemy3Count > 0 && Wave.enemy2Count <= 0) //se tem inimigo 3 ainda
                    {
                        SpawnEnemy(Wave.enemy3);
                        Wave.enemy3Count--;
                    }
                    else if (Wave.enemy4Count > 0 && Wave.enemy3Count <= 0) //se tem inimigo 3 ainda
                    {
                        SpawnEnemy(Wave.enemy4);
                        Wave.enemy4Count--;
                    }

                    break;

                    #endregion
            }

            yield return new WaitForSeconds(1f / Wave.rate);
        }

        waveIndex++;
        PlayerStats.rounds++;

        if (waveIndex == wavesCount.Length)
        {
            gameManager.WinLevel();
        }
    }

    void SpawnEnemy(GameObject _enemy)
    {
        Instantiate(_enemy, spawnPoint.position, spawnPoint.rotation);
    }


}