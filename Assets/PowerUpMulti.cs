using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMulti : MonoBehaviour
{


    [SerializeField] GameObject VFXforPickUp;
    [SerializeField] float vfxLifeTime = 1f;
    [SerializeField] float maxLifeTime = 30f;

    [SerializeField] GameObject ball;
    [SerializeField] Vector3 offset = new Vector3(0.2f, 0, 0);
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 5.5f;

    [SerializeField] int maxBallsInPlay = 3;
    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            FindObjectOfType<BallsCounter>().CountBalls();
            PickUp();
        }

    }

    private void PickUp()
    {
        // spawn VFX at pickup
        PlayEffects();

        // Apply effect first chack if possible

        if (FindObjectOfType<Ball>() == null) { Destroy(gameObject); return; }

        if (IfAllBallsHasStarted()) { Destroy(gameObject); return; }

        if (FindObjectOfType<BallsCounter>().BallsInPlay() >= maxBallsInPlay) { Destroy(gameObject); return; }



        // get info from old ball
        Vector3 newBallPosRaw = FindObjectOfType<Ball>().GetComponent<Transform>().transform.position;
        float clampedXpos = Mathf.Clamp(newBallPosRaw.x + offset.x, minX, maxX);
        Vector3 newBallPos = new Vector3(clampedXpos, newBallPosRaw.y, newBallPosRaw.z);

        Vector2 originVelocity = FindObjectOfType<Ball>().GetComponent<Rigidbody2D>().velocity;

        for (int i = 1; i < maxBallsInPlay; i++)
        {
            Instantiate(ball, newBallPos, Quaternion.identity);
        }

        Ball[] balls = FindObjectsOfType<Ball>();
        foreach(var ball in balls)
        {
            ball.GetComponent<Ball>().HasStarted = true;
            ball.GetComponent<Rigidbody2D>().velocity = originVelocity;
        }

        //// sawn second ball and add velocity
        //SpawnSecondBall(newBallPos, originVelocity);

        //// sawn third ball and add velocity
        //SpwanThirdBall(newBallPos, originVelocity);

        //Ball[] balls = FindObjectsOfType<Ball>();
        //foreach (var ball in balls)
        //{
        //    ball.GetComponent<Ball>().HasStarted = true;
        //}

        // update numbers of balls in play
        FindObjectOfType<BallsCounter>().CountBalls();

        Destroy(gameObject);
    }



    //private void SpawnSecondBall(Vector3 newBallPos, Vector2 originVelocity)
    //{
    //    GameObject positiveOffsetBallSpawn = Instantiate(ball, newBallPos + offset, Quaternion.identity);
    //    positiveOffsetBallSpawn.GetComponent<Ball>().HasStarted = true;
    //    positiveOffsetBallSpawn.GetComponent<Rigidbody2D>().velocity = originVelocity;
    //}

    //private void SpwanThirdBall(Vector3 newBallPos, Vector2 originVelocity)
    //{
    //    GameObject negativeOffsetBallSpawn = Instantiate(ball, newBallPos - offset, Quaternion.identity);
    //    negativeOffsetBallSpawn.GetComponent<Ball>().HasStarted = true;
    //    negativeOffsetBallSpawn.GetComponent<Rigidbody2D>().velocity = originVelocity;
    //}

    private bool IfAllBallsHasStarted()
    {
        Ball[] ballsInGameplay = FindObjectsOfType<Ball>();

        foreach (var ball in ballsInGameplay)
        {
            if (ball.HasStarted)
            {
                return false;
            }
        }

        return true;
    }

    private void PlayEffects()
    {
        if (VFXforPickUp == null) { return; }
        GameObject vfx = Instantiate(VFXforPickUp, transform.position, transform.rotation);
        Destroy(vfx, vfxLifeTime);
    }
}

