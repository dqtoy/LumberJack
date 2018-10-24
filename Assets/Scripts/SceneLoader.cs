using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    GameSession gameStatus;


    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
        if (FindObjectOfType<GameSession>() != null)
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
        SceneManager.LoadScene("00_MainMenu");
    }

    public void LoadLoseScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("00_GameOver");
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("00_GameOver");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
