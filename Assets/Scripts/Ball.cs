using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Paddle paddle;
    Rigidbody2D ballRigibody;

    [SerializeField] Vector2 launchForce;
    [SerializeField] Vector2 minBallVelocity;
    Vector2 paddleToBallVector;
    Vector2 tempVelocity;
    [SerializeField] float timeToRestoreVelocityAfterPause = 0.5f;
    public bool HasStarted { get; set; }
    public bool IsThisBallWithChainsaw { get; set; }

    void Start()
    {
        HasStarted = false;
        paddle = FindObjectOfType<Paddle>();
        ballRigibody = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle.transform.position;
    }

    void Update()
    {
        if (!HasStarted)
        {
            LockBallToPaddle();
        }

        if (ballRigibody.velocity.magnitude < minBallVelocity.magnitude)
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
        HasStarted = true;
        ballRigibody.velocity = launchForce;
    }
    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
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
    public Vector2 CurrentVelocity()
    {
        return ballRigibody.velocity;
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

