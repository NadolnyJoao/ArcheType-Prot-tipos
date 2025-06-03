using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject UIpauseMenu;  
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        UIpauseMenu.SetActive(false);
        Time.timeScale = 1f;   
        isPaused = false;
    }

    void Pause()
    {
        UIpauseMenu.SetActive(true);
        Time.timeScale = 0f;   
        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
