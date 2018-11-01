using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{

    // configuration parameters
    [SerializeField] float minX = 0.2f;
    [SerializeField] float maxX = 6f;
    [SerializeField] float screenWidthInUnits = 6.25f;
    [SerializeField] Slider moveSlider;

    [SerializeField] GameObject ball;
    [SerializeField] Vector3 startOffset = new Vector3 (0f,0.5f,0f);

    [SerializeField] GameObject startButton;        // must be serialized and wired up something to make it active
    // Use this for initialization

    GameSession gameSession;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        //ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        //float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;        // creating float for actual mouse position
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
}
