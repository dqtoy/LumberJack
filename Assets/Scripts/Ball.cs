using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //TODO find a way to limit max ball speed

    Paddle paddle;
    Rigidbody2D ballRigibody;

    [SerializeField] Vector2 launchForce;
    [SerializeField] Vector2 minBallVelocity;
    [SerializeField] Vector2 maxBallVelocity;
    Vector2 paddleToBallVector;
    Vector2 tempVelocity;

    [SerializeField] float unFreezTime;
    [SerializeField] float timeToRestartBallPosition;
    public bool IsThisBallWithChainsaw { get; set; }
    [SerializeField] bool hasStarted = false;

    public bool HasStarted()
    {
        return hasStarted;
    }
    public void SetHasStarted(bool state)
    {
        hasStarted = state;
    }

    void Start()
    {
        paddle = FindObjectOfType<Paddle>();
        ballRigibody = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle.transform.position;
    }

    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
        }
        if (ballRigibody.velocity.magnitude < minBallVelocity.magnitude)
        {
            ballRigibody.velocity += minBallVelocity;
        }
        //else if (ballRigibody.velocity.magnitude > maxBallVelocity.magnitude)
        //{
        //    ballRigibody.velocity -= minBallVelocity;
        //}
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
        FindObjectOfType<SFXController>().PlaySFX(collision);
        if (FindObjectOfType<PowerUpHandler>().IsPowerUpRestartActive && collision.gameObject.CompareTag("Paddle"))
        {
            ResetBallPosition();
        }
    }

    public void LaunchBall()
    {
        ballRigibody.velocity = launchForce;
        hasStarted = true;
        FindObjectOfType<Paddle>().SetDeActiveStartButton();
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
        Invoke("RestoreVelocity", unFreezTime);
    }

    private void RestoreVelocity()
    {
        ballRigibody.velocity = tempVelocity;
    }

    public void WiatForResetBallPostion()
    {
        Invoke("ResetBallPosition", timeToRestartBallPosition);
    }

    private void ResetBallPosition()
    {
        hasStarted = false;
        FindObjectOfType<Paddle>().SetActiveStartButton();
    }


}

