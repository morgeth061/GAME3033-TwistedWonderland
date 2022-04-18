using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIBehaviour : MonoBehaviour
{
    public MovementComponent movementComponent;
    public SaveGameManager saveGameManager;
    public void OnResumeClicked()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementComponent>().isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    public void OnSaveClicked()
    {
        saveGameManager.SaveGame();
    }

    public void OnLoadClicked()
    {
        saveGameManager.LoadGame();
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
