using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //config parameters
    Paddle paddle1;
    Rigidbody2D ballRigibody;

    [SerializeField] Vector2 launchForce;
    [SerializeField] float timeToRestoreVelocityAfterPause = 0.5f;

    Vector2 paddleToBallVector;
    public bool HasStarted { get; set; }

    Vector2 tempVelocity;
    [SerializeField] Vector2 minBallVelocity;

    public bool IsThisBallWithChainsaw { get; set; }

    void Start()
    {
        HasStarted = false;
        paddle1 = FindObjectOfType<Paddle>();
        ballRigibody = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    void Update()
    {
        if (!HasStarted)
        {
            LockBallToPaddle();
        }

        if(ballRigibody.velocity.magnitude < minBallVelocity.magnitude)
        {
            ballRigibody.velocity += minBallVelocity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "LoseCollider")
        {
            FindObjectOfType<BallsCounter>().SubFromBalls(this);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (FindObjectOfType<PowerUpHandler>().IsPowerUpRestartActive && collision.gameObject.CompareTag("Paddle"))
        {
            ResetBallPosition();
        }
    }

    public void LaunchBall()
    {
        ballRigibody.velocity = launchForce;
        HasStarted = true;
    }
    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    public void FreezBall()
    {
        tempVelocity = ballRigibody.velocity;
        ballRigibody.constraints = RigidbodyConstraints2D.FreezePosition;
    }
    public void UnFreezeBall()
    {
        ballRigibody.constraints = RigidbodyConstraints2D.None;
        Invoke("RestoreVelocity", timeToRestoreVelocityAfterPause);
    }
    private void RestoreVelocity()
    {
        ballRigibody.velocity = tempVelocity;
    }

    public void WiatForResetBallPostion()
    {
        Invoke("ResetBallPosition", 1f);
    }
    private void ResetBallPosition()
    {
        HasStarted = false;
        FindObjectOfType<Paddle>().SetActiveStartButton();
    }




}

