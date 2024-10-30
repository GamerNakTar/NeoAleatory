using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        GameIsPaused = false;

        CursorManager.TurnCursorOff();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

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
