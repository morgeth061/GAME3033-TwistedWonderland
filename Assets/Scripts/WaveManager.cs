using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float waveTime;
    public int totalWaves;
    public int currentWave;

    public ZombieSpawner[] spawners;

    private bool timerCounting;


    public float timer;

    void Start()
    {
        timer = waveTime;
        timerCounting = true;

        NewWave();
        //Load scene if applicable
    }

    void Update()
    {
        if (currentWave >= totalWaves)
        {
            //Win Condition
        }

        if (timerCounting)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = waveTime;
            NewWave();
            currentWave++;
        }
    }

    private void NewWave()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].NewWave();
        }
    }
}
