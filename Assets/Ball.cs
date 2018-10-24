using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    //config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] Vector2 launchForce;
    Rigidbody2D ballRigibody;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Use this for initialization
    void Start()
    {
        ballRigibody = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchBall();
        }
       

    }

    private void LaunchBall()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ballRigibody.velocity = launchForce;
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
   
    public void FreezBall()
    {
        ballRigibody.isKinematic = true;
        ballRigibody.velocity = Vector2.zero;
    }
}
