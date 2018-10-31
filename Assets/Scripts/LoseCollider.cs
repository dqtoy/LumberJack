using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    [SerializeField] bool isMultiPowerUp = false;
    public int count;
    private void Update()
    {
        if (isMultiPowerUp)
        {
            count = FindObjectsOfType<Ball>().Length;
            if (count <= 1)
            {
                SetIsMultiPowerUp(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!isMultiPowerUp)
            {
                ProcessingBallFall();
            }
            else
            {
                WhenMultiPowerUpOn(collision);
            }
        }
    }

    private void ProcessingBallFall()
    {
        FindObjectOfType<GameSession>().ReduceLifePoint();
        if (FindObjectOfType<GameSession>().GetCurrentLife() < 0)
        {
            gameCanvas.GetComponent<Animator>().SetTrigger("looseLevel");
        }
        else
        {
            FindObjectOfType<RemainsLifeDisplay>().UpdateLive();
            ProcessResetBall();
        }
    }

    private void ProcessResetBall()
    {
        FindObjectOfType<Ball>().WiatForResetBallPostion();
    }

    public void SetIsMultiPowerUp(bool state)
    {
        isMultiPowerUp = state;
    }

    public bool IsMultiPowerUp()
    {
        return isMultiPowerUp;
    }

    private void WhenMultiPowerUpOn(Collider2D collision)
    {
        if (count > 1)
        {
            Destroy(collision.gameObject);
        }
    }
}
