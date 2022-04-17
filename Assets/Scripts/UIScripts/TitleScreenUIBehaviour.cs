using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUIBehaviour : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject mapScreen;
    public void OnTitleScreenClick()
    {
        titleScreen.SetActive(false);
        mapScreen.SetActive(true);
    }

    public void OnFinancialDistrictClick()
    {
        SceneManager.LoadScene("Level1");
    }
}
