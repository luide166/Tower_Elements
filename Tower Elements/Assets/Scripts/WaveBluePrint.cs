using UnityEngine;

[System.Serializable]
public class WaveBluePrint
{
    [HideInInspector]
    public int count; //number of Enemies
    public float rate; // (1/rate)
    public float timeToNextWave;

    public int enemyTypes;
    [Space(15)]

    public GameObject enemy1;
    public int enemy1Count;
    [Space(10)]
    public GameObject enemy2;
    public int enemy2Count;
    [Space(10)]
    public GameObject enemy3;
    public int enemy3Count;
    [Space(10)]
    public GameObject enemy4;
    public int enemy4Count;

}
