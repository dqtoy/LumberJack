using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [Range(0, 2)] [SerializeField] float gameSpeed;
    [SerializeField] bool isAutoplayEnabled;
    [SerializeField] int lifes;

    private static GameSession gameSession = null;
    private void Awake()
    {
        if (gameSession != null && gameSession != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            gameSession = this;
            DontDestroyOnLoad(gameObject);
        }
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
        if (lifes <= 0)
        {
            GameOver();
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
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoplayEnabled;
    }

}
