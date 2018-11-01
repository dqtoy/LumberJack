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
    [SerializeField] int lifes = 3;

    List<Ball> balls = new List<Ball>();

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

    private void Start()
    {
        CountBalls();
    }

    private void CountBalls()
    {
        Ball[] countBalls = FindObjectsOfType<Ball>();
        foreach (var ball in countBalls)
        {
            balls.Add(ball);
        }
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
        if(balls.Count == 0)
        {
            FindObjectOfType<Paddle>().SpawnBall();
            CountBalls();
        }
    }

    public void AddPointsToScore(int pointsPerBlockDestoryed)
    {
        currentScore += pointsPerBlockDestoryed;
    }

    public void ReduceLifePoint()
    {
        lifes--;
    }

    public void AddLifePoint()
    {
        lifes++;
    }

    public int GetCurrentLife()
    {
        return lifes;
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

    public void SubFromBalls(Ball ball)
    {
        balls.Remove(ball);
    }

}
