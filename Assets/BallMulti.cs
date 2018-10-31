using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMulti : MonoBehaviour
{

    Rigidbody2D ballRigidbody2D;
    [SerializeField] GameObject ball;
    Vector3 shiftOfClone = new Vector3(0.5f, 0f, 0f);
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 5.2f;

    void Start()
    {
        ballRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Clone()
    {
        minusClone();
        plusClone();
    }

    public void minusClone()
    {
        Vector3 spawnPos = new Vector3(PosX(), transform.position.y, transform.position.z);

        GameObject xball = Instantiate(ball, spawnPos, transform.rotation);
        xball.GetComponent<Rigidbody2D>().velocity = ballRigidbody2D.velocity;
    }

    public void plusClone()
    {
        Vector3 spawnPos = new Vector3(PosX(), transform.position.y, transform.position.z);

        GameObject xball = Instantiate(ball, spawnPos, transform.rotation);
        xball.GetComponent<Rigidbody2D>().velocity = ballRigidbody2D.velocity;
    }

    private float PosX()
    {
        float posX = UnityEngine.Random.Range(minX, maxX);
        return posX;
    }
}
