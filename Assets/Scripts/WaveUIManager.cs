using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUIManager : MonoBehaviour
{

    public TextMeshProUGUI currentWaveText;
    public TextMeshProUGUI totalWaveText;
    public TextMeshProUGUI timerText;

    public WaveManager waveManagerObject;
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
    void Update()
    {
        currentWaveText.text = waveManagerObject.currentWave.ToString();
        totalWaveText.text = waveManagerObject.totalWaves.ToString();

        int minuteTimer = (int)Mathf.Round(waveManagerObject.timer) / 60;
        int secondTimer = (int)Mathf.Round(waveManagerObject.timer) % 60;

        string minuteText;
        string secondText;

        if (minuteTimer < 10)
        {
            minuteText = "0" + minuteTimer;
        }
        else
        {
            minuteText = minuteTimer.ToString();
        }

        if (secondTimer < 10)
        {
           secondText = "0" + secondTimer;
        }
        else
        {
            secondText =secondTimer.ToString();
        }


        timerText.text = (minuteTimer + ":" + secondTimer);

    }
}
