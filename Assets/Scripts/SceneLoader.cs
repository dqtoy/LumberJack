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

    public void LoadStartScene()
    {
        
        SceneManager.LoadScene(0);
    }

    public void LoadLoseScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("03_GameOver");
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
