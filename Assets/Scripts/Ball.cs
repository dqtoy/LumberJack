using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] Vector2 launchForce;
    [SerializeField] float unFreezTime = 0.5f;
    Rigidbody2D ballRigibody;
    [SerializeField] GameObject gameCanvas;


    // state
    Vector2 paddleToBallVector;
    [SerializeField] bool hasStarted = false;
    Vector2 tempVelocity;
    //public Vector2 startPosition = new Vector2(3.12f, 1.73f);

    [SerializeField] bool isPowerUpRestartActive = false;
    [SerializeField] bool isPowerUpChainsawActive = false;

    [SerializeField] Sprite[] balls = new Sprite[2];


    // Use this for initialization
    void Start()
    {
        paddle1 = FindObjectOfType<Paddle>();
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

        //if(transform.position.y < -3)
        //{
        //    SeekAndDestroy();
        //}

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPowerUpRestartActive && collision.gameObject.CompareTag("Paddle"))

        {
            ResetBallPosition();
        }

    }

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
        Invoke("ResetBallPosition", 1f);
    }

    private void ResetBallPosition()
    {
        hasStarted = false;
        //ballRigibody.velocity = Vector3.zero;
        //ballRigibody.angularVelocity = 0;
        //transform.position = startPosition;
        FindObjectOfType<Paddle>().SetActiveStartButton();
    }

    public void SetIsPowerUpRestartActive(bool state)
    {
        isPowerUpRestartActive = state;
    }

    public void SetIsPowerUpChainsawActive(bool state)
    {
        isPowerUpChainsawActive = state;
    }

    public bool GetIsPowerUpChainsawActive()
    {
        return isPowerUpChainsawActive;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "LoseCollider")
        {
            FindObjectOfType<GameSession>().SubFromBalls(this);
            Destroy(gameObject);
        }
    }

    
}

