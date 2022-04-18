using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUIBehaviour : MonoBehaviour
{
    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
