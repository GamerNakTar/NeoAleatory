using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    private static SceneSwapper _instance;
    public enum Scene
    {
        Title,
        Game,
        Clear,
        GameOver
    }
    private void Start()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static void SwapScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void SwapScene(Scene scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public static void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
