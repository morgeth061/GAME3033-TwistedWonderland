using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float waveTime;
    public int totalWaves;
    public int currentWave;

    public GameObject winUI;

    public ZombieSpawner[] spawners;

    public AudioSource WaveBeginSFX;

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

            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementComponent>().isPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            winUI.SetActive(true);
        }

        if (timerCounting)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            
            NewWave();
            currentWave++;
        }
    }

    public void NewWave()
    {
        timer = waveTime;
        WaveBeginSFX.Play();
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].NewWave();
        }
    }
}
