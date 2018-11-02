using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsCounter : MonoBehaviour
{

    // class to contain all states for pickups in game (bools etc)
    // TODO - chainsaw changes, multiball, sickToPaddle

    List<Ball> balls = new List<Ball>();



    // Use this for initialization
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

        if (balls.Count == 0)
        {
            FindObjectOfType<GameSession>().ReduceLifePoint();
            FindObjectOfType<Paddle>().SpawnBall();
        }

    }

    public int BallsInPlay()
    {
        return balls.Count;
    }
}
