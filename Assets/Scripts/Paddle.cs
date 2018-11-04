using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{

    // configuration parameters
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float screenWidthInUnits = 6.25f;
    [SerializeField] Slider moveSlider;
    [SerializeField] GameObject ball;
    [SerializeField] Vector3 startOffset;
    [SerializeField] GameObject startButton;
    GameSession gameSession;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        MovingPaddle();
    }

    private void MovingPaddle()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return FindObjectOfType<Ball>().transform.position.x;
        }
        else
        {
            return moveSlider.value * screenWidthInUnits;
        }
    }

    public void SpawnBall()
    {
        Instantiate(ball, transform.position + startOffset, transform.rotation);
        ball.GetComponent<Ball>().WiatForResetBallPostion();
    }

    public void LaunchBall()
    {
        FindObjectOfType<Ball>().LaunchBall();
    }

    public void SetActiveStartButton()
    {
        startButton.SetActive(true);
    }

    public void SetDeActiveStartButton()
    {
        startButton.SetActive(false);
    }
}
