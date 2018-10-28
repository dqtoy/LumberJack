using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    Ball ball;
    [SerializeField] GameObject gameCanvas;


    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().ReduceLifePoint();
        //ball.FreezBall();
        if (FindObjectOfType<GameSession>().GetCurrentLife() < 0)
        {
            gameCanvas.GetComponent<Animator>().SetTrigger("looseLevel");
        }
        else
        {
            FindObjectOfType<RemainsLifeDisplay>().UpdateLive();
            ProcessResetBall();
        }
    }

    private void ProcessResetBall()
    {
        ball.WiatForResetBallPostion();
        //ball.UnFreezeBall();
    }
}
