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

}
