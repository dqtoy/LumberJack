using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    // configuration parameters
    [Range(0, 2)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] bool isAutoplayEnabled;

    // state variable
    public static int CurrentScore { get; set; }

    [SerializeField] int lifes = 3;

    private static GameSession gameSession = null;

    private void Awake()
    {
        if (gameSession == null)
        {
            gameSession = this;
        }
        else if (gameSession != this)
        {
            DestroyImmediate(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public int GetCurrentLife()
    {
        return lifes;
    }

    public void ReduceLifePoint()
    {
        lifes--;

        if (lifes < 0)
        {
            GameOver();
            return;
        }

        FindObjectOfType<RemainsLifeDisplay>().UpdateLive(lifes);


    }

    public void AddLifePoint()
    {
        lifes++;
        FindObjectOfType<RemainsLifeDisplay>().UpdateLive(lifes);
    }

    private void GameOver()
    {
        GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<Animator>().SetTrigger("looseLevel");
        return;
    }

    public void ResetGame()
    {
        CurrentScore = 0;
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoplayEnabled;
    }

}
