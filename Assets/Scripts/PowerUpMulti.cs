using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMulti : MonoBehaviour
{


    [SerializeField] GameObject VFXforPickUp;
    [SerializeField] float vfxLifeTime;
    [SerializeField] float maxLifeTime;

    [SerializeField] GameObject ball;
    [SerializeField] Vector3 offset;
    [SerializeField] float minX;
    [SerializeField] float maxX;

    [SerializeField] int maxBallsInPlay = 3;

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            PlayEffects();
            FindObjectOfType<BallsCounter>().CountBalls();
            PickUp();
            Destroy(gameObject, vfxLifeTime);
        }
    }

    private void PickUp()
    {
        if (FindObjectOfType<Ball>() == null) { Destroy(gameObject); return; }
        if (IfAllBallsHasStarted()) { Destroy(gameObject); return; }
        if (FindObjectOfType<BallsCounter>().GetBallsInPlay() >= maxBallsInPlay) { Destroy(gameObject); return; }
        Vector2 originVelocity = FindObjectOfType<Ball>().GetComponent<Rigidbody2D>().velocity;
        SpawnSecondBall(CalculatedNewBallPosition(), originVelocity);
        SpwanThirdBall(CalculatedNewBallPosition(), originVelocity);
        FindObjectOfType<BallsCounter>().CountBalls();
    }

    private Vector3 CalculatedNewBallPosition()
    {
        Vector3 newBallPosRaw = FindObjectOfType<Ball>().GetComponent<Transform>().transform.position;
        float clampedXpos = Mathf.Clamp(newBallPosRaw.x + offset.x, minX, maxX);
        Vector3 newBallPos = new Vector3(clampedXpos, newBallPosRaw.y, newBallPosRaw.z);
        return newBallPos;
    }

    private void SpawnSecondBall(Vector3 newBallPos, Vector2 originVelocity)
    {
        GameObject positiveOffsetBallSpawn = Instantiate(ball, newBallPos + offset, Quaternion.identity);
        SetupNewAxe(positiveOffsetBallSpawn, originVelocity);
    }

    private void SpwanThirdBall(Vector3 newBallPos, Vector2 originVelocity)
    {
        GameObject negativeOffsetBallSpawn = Instantiate(ball, newBallPos - offset, Quaternion.identity);
        SetupNewAxe(negativeOffsetBallSpawn, originVelocity);
    }

    private void SetupNewAxe(GameObject spawnedAxe, Vector2 originVelocity)
    {
        spawnedAxe.GetComponent<Ball>().SetHasStarted(true);
        spawnedAxe.GetComponent<Rigidbody2D>().velocity = originVelocity;
    }

    private bool IfAllBallsHasStarted()
    {
        Ball[] ballsInGameplay = FindObjectsOfType<Ball>();
        foreach (var ball in ballsInGameplay)
        {
            if (ball.HasStarted())
            {
                return false;
            }
        }
        return true;
    }


    private void PlayEffects()
    {
        if(VFXforPickUp == null) { return; }
        GameObject vfx = Instantiate(VFXforPickUp, transform.position, transform.rotation);
        Destroy(vfx, vfxLifeTime);
    }
}
