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
    [SerializeField] int currentScore = 0;

    private static GameSession gameStatus = null;

    private void Awake()
    {
        if (gameStatus == null)
        {
            gameStatus = this;
        }
        else if (gameStatus != this)
        {
            DestroyImmediate(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddPointsToScore(int pointsPerBlockDestoryed)
    {
        currentScore += pointsPerBlockDestoryed;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void ResetGame()
    {
        currentScore = 0;
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoplayEnabled;
    }

}
