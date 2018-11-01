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
    [SerializeField] float unFreezTime = 0.5f;      // delay before game start after resuming from pause menu

    //[SerializeField] GameObject gameCanvas;


    // state
    Vector2 paddleToBallVector;
    [SerializeField] bool hasStarted = false;

    // storing velocity before pause to use it later to unpause
    Vector2 tempVelocity;

    // bools for activete powerUps
    [SerializeField] bool isPowerUpRestartActive = false;
    [SerializeField] bool isPowerUpChainsawActive = false;
    // chainsaw sprite swap array
    [SerializeField] Sprite[] balls = new Sprite[2];


    // Use this for initialization
    void Start()
    {
        paddle1 = FindObjectOfType<Paddle>();                       // need to find paddel everytime when spawn new ball
        ballRigibody = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
        }

        ChangingSprites();
    }

    // use for checking if ball is out of playspace

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "LoseCollider")
        {
            FindObjectOfType<BallsCounter>().SubFromBalls(this);
            Destroy(gameObject);
        }
    }

    // launching and locking start position

    public void LaunchBall()
    {
        ballRigibody.velocity = launchForce;
        hasStarted = true;
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    // methods for pausing game

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

    // function for call ResetBall with dealy

    public void WiatForResetBallPostion()
    {
        Invoke("ResetBallPosition", 1f);
    }

    private void ResetBallPosition()
    {
        hasStarted = false;
        FindObjectOfType<Paddle>().SetActiveStartButton();
    }

    // setting RestertPowerUp if collision - reset

    public void SetIsPowerUpRestartActive(bool state)
    {
        isPowerUpRestartActive = state;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPowerUpRestartActive && collision.gameObject.CompareTag("Paddle"))

        {
            ResetBallPosition();
        }

    }

    // configuratin and metods for ChainsawPowetUp

    public void SetIsPowerUpChainsawActive(bool state)
    {
        isPowerUpChainsawActive = state;
    }

    public bool GetIsPowerUpChainsawActive()
    {
        return isPowerUpChainsawActive;
    }

    private void ChangingSprites()
    {
        if (isPowerUpChainsawActive)
        {

            SwapSpriteToChainsaw();
        }
        else
        {
            SwapSpriteToAxe();
        }
    }

    private void SwapSpriteToAxe()
    {
        GetComponent<SpriteRenderer>().sprite = balls[0];
    }

    private void SwapSpriteToChainsaw()
    {
        GetComponent<SpriteRenderer>().sprite = balls[1];
    }


}

