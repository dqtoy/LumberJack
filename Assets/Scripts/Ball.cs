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
    [SerializeField] GameObject startButton;


    // state
    Vector2 paddleToBallVector;
    [SerializeField] bool hasStarted = false;
    Vector2 tempVelocity;
    Vector2 startPosition;

    [SerializeField] bool isPowerUpRestartActive = false;
    [SerializeField] bool isPowerUpChainsawActive = false;

    [SerializeField] Sprite[] balls = new Sprite[2];
    // Use this for initialization
    void Start()
    {
        ballRigibody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
        }

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
        //if (Input.GetMouseButtonUp(0))
        //{
        ballRigibody.velocity = launchForce;
        hasStarted = true;
        startButton.SetActive(false);
        //}
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
        ballRigibody.velocity = Vector3.zero;
        ballRigibody.angularVelocity = 0;
        hasStarted = false;
        transform.position = startPosition;
        startButton.SetActive(true);
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

}
