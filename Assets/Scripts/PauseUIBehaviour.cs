using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIBehaviour : MonoBehaviour
{
    public void OnResumeClicked()
    {

    }

    public void OnSaveClicked()
    {

    }

    public void OnLoadClicked()
    {

    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
