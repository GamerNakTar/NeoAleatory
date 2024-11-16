using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    public GameObject pauseMenuUI;

    public OptionWindow optionWindow;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !optionWindow.isOpen)
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;

        CursorManager.TurnCursorOff();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;

        CursorManager.TurnCursorOn();
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu");
    }

    public void LoadTitle()
    {
        Debug.Log("Loading Title");
        SceneSwapper.SwapScene(SceneSwapper.Scene.Title);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
