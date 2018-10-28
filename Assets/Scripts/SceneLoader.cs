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

    public void LoadOptions()
    {
        SceneManager.LoadScene("00_Options");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("00_Credits");
    }

    public void LaunchFacebook()
    {
        Application.OpenURL("https://www.facebook.com/LongEarDevelopment/");
    }

    public void LaunchPayPal()
    {
        Application.OpenURL("https://www.paypal.me/longeardev");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
