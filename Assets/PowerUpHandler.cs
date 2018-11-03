using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{

    //spawning powerUps
    [SerializeField] AudioClip sfxForPowerUps;
    [Range(0, 1)] [SerializeField] float sfxVolume;
    [SerializeField] GameObject[] powerUps;
    [Range(0, 10)] [SerializeField] int tresholdForPowerUpSpawn;

    [SerializeField] float powerUpChainsawTimeEffect;
    [SerializeField] Sprite[] axeAndChansaw;
    public bool IsPowerUpChainsawActive { get; set; }

    [SerializeField] float powerUpLongPaddleTimeEffect;
    [SerializeField] Vector3 paddleLengthMultiplier;

    [SerializeField] float powerUpRestartTimeEffect;
    public bool IsPowerUpRestartActive { get; set; }

    [SerializeField] int maxBallsInPlay;
    [SerializeField] Vector3 offsetOfSpawningNewBalls = new Vector3(0.2f, 0, 0);
    [SerializeField] float minXPosForSpawn = 1f;
    [SerializeField] float maxXPosForSpawn = 5.5f;
    [SerializeField] GameObject ball;

    public void SpawnPowerUp(Vector3 position)
    {
        int chance = UnityEngine.Random.Range(0, 11);
        int index = UnityEngine.Random.Range(0, powerUps.Length);
        if (chance >= tresholdForPowerUpSpawn)
        {
            Instantiate(powerUps[index], position, transform.rotation);
            GetComponent<AudioSource>().PlayOneShot(sfxForPowerUps, sfxVolume);
        }
    }

    public void PowerUpLife()
    {
        FindObjectOfType<GameSession>().AddLifePoint();
    }

    public void ActiveChainsawInHandler(Collider2D paddle)
    {
        StartCoroutine(PowerUpChainsaw(paddle));
    }
    IEnumerator PowerUpChainsaw(Collider2D player)
    {
        var changedBall = FindObjectOfType<Ball>();

        IsPowerUpChainsawActive = true;
        ChangingSpriteFormAxeToChainsaw();
        changedBall.IsThisBallWithChainsaw = true;
        FindObjectOfType<BallAudio>().PlayChainsawAudio();

        yield return new WaitForSeconds(powerUpChainsawTimeEffect);

        IsPowerUpChainsawActive = false;
        if (changedBall != null)
        {
            changedBall.IsThisBallWithChainsaw = false;
        }
        ChangingSpriteFormAxeToChainsaw();
    }
    private void ChangingSpriteFormAxeToChainsaw()
    {
        if (IsPowerUpChainsawActive)
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
        FindObjectOfType<Ball>().GetComponent<SpriteRenderer>().sprite = axeAndChansaw[0];
    }
    private void SwapSpriteToChainsaw()
    {
        FindObjectOfType<Ball>().GetComponent<SpriteRenderer>().sprite = axeAndChansaw[1];
    }

    public void ActivePowerUpLong(Collider2D collider)
    {
        StartCoroutine(PowerUpLong(collider));
    }
    IEnumerator PowerUpLong(Collider2D paddle)
    {
        paddle.transform.localScale += paddleLengthMultiplier;
        yield return new WaitForSeconds(powerUpLongPaddleTimeEffect);
        paddle.transform.localScale -= paddleLengthMultiplier;
    }

    public void ActivePowerUpExplosion()
    {
        if (FindObjectOfType<DestroyStone>() == null) { return; }
        FindObjectOfType<DestroyStone>().GetComponent<Animator>().SetTrigger("DestroyStone");

    }

    public void ActivePowerUpRestart(Collider2D paddle)
    {
        StartCoroutine(PowerUpRestart(paddle));
    }

    IEnumerator PowerUpRestart(Collider2D player)
    {
        IsPowerUpRestartActive = true;
        yield return new WaitForSeconds(powerUpRestartTimeEffect);
        IsPowerUpRestartActive = false;
    }

    public void ActivePowerUpMultiball()
    {
        FindObjectOfType<BallsCounter>().CountBalls();
        if (FindObjectOfType<Ball>() == null) { Debug.Log("Multi canel: no ball"); return; }
        if (IfAllBallsHasStarted()) { Debug.Log("Multi canel: Not all ball started"); return; }
        if (FindObjectOfType<BallsCounter>().BallsInPlay() >= maxBallsInPlay) { Debug.Log("Multi canel: max ball in play"); return; }

        //// get info from old ball
        //var originalBall = FindObjectOfType<Ball>();
        //Vector3 originalPosition = originalBall.GetComponent<Transform>().transform.position;
        //Vector2 originalVelocity = originalBall.GetComponent<Rigidbody2D>().velocity;

        //SpawnSecondBall(NewBallPos(originalPosition), originalVelocity);

        //SpwanThirdBall(NewBallPos(originalPosition), originalVelocity);

        //FindObjectOfType<BallsCounter>().CountBalls();



    }

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

    private void SpawnSecondBall(Vector3 newBallPos, Vector2 originVelocity)
    {
        GameObject positiveOffsetBallSpawn = Instantiate(ball, newBallPos + offsetOfSpawningNewBalls, Quaternion.identity);
        SetupStartParametersForSpawnedBall(positiveOffsetBallSpawn, originVelocity);
    }

    private void SpwanThirdBall(Vector3 newBallPos, Vector2 originVelocity)
    {
        GameObject negativeOffsetBallSpawn = Instantiate(ball, newBallPos - offsetOfSpawningNewBalls, Quaternion.identity);
        SetupStartParametersForSpawnedBall(negativeOffsetBallSpawn, originVelocity);
    }

    private void SetupStartParametersForSpawnedBall(GameObject ball, Vector2 velocity)
    {
        ball.GetComponent<Ball>().HasStarted = true;
        ball.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private Vector3 NewBallPos(Vector3 originPostion)
    {
        float clampedXpos = Mathf.Clamp(originPostion.x + offsetOfSpawningNewBalls.x, minXPosForSpawn, maxXPosForSpawn);
        Vector3 newBallPos = new Vector3(clampedXpos, originPostion.y, originPostion.z);
        return newBallPos;
    }
}
