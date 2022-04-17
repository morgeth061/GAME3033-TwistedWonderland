using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUIBehaviour : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject mapScreen;
    public GameObject instructionsScreen;
    public GameObject creditsScreen;
    public void OnTitleScreenClick()
    {
        titleScreen.SetActive(false);
        mapScreen.SetActive(true);
    }

    public void OnInstructionsClicked()
    {
        instructionsScreen.SetActive(true);
        mapScreen.SetActive(false);
    }

    public void OnCreditsClicked()
    {
        creditsScreen.SetActive(true);
        mapScreen.SetActive(false);
    }

    public void OnBackClicked()
    {
        instructionsScreen.SetActive(false);
        creditsScreen.SetActive(false);
        mapScreen.SetActive(true);
    }

    public void OnFinancialDistrictClick()
    {
        SceneManager.LoadScene("Level1");
    }
}
