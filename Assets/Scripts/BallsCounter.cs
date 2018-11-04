using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsCounter : MonoBehaviour
{
    List<Ball> balls = new List<Ball>();

    void Start()
    {
        CountBalls();
    }

    public void CountBalls()
    {
        balls.Clear();
        Ball[] countBalls = FindObjectsOfType<Ball>();
        foreach (var ball in countBalls)
        {
            balls.Add(ball);
        }
    }

    public void SubFromBalls(Ball ball)
    {
        balls.Remove(ball);
        if (balls.Count <= 0)
        {
            FindObjectOfType<GameSession>().ReduceLifePoint();
            FindObjectOfType<Paddle>().SpawnBall();
        }
    }

    public int GetBallsInPlay()
    {
        return balls.Count;
    }
}
