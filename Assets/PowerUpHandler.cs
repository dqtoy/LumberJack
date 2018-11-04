﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{

    //spawning powerUps
    [SerializeField] AudioClip sfxForPowerUps;
    [Range(0, 1)] [SerializeField] float sfxVolume;
    [SerializeField] GameObject[] powerUps;
    [Range(0, 10)] [SerializeField] int tresholdForPowerUpSpawn;

    public bool IsPowerUpChainsawActive { get; set; }
    [SerializeField] float powerUpChainsawTimeEffect;
    [SerializeField] Sprite[] axeAndChainsaw;

    [SerializeField] Vector3 paddleLengthMultiplier;
    [SerializeField] float powerUpLongPaddleTimeEffect;

    public bool IsPowerUpRestartActive { get; set; }
    [SerializeField] float powerUpRestartTimeEffect;

    [SerializeField] int maxBallsInPlay;
    [SerializeField] GameObject ball;
    [SerializeField] Vector3 offsetForSpawningBall;
    [SerializeField] float minXForSpawn;
    [SerializeField] float maxXForSpawn;


    private void Start()
    {
        IsPowerUpRestartActive = false;
    }

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

    public void ActiveChainsawInHandler()
    {
        StartCoroutine(PowerUpChainsaw());
    }
    IEnumerator PowerUpChainsaw()
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
        FindObjectOfType<Ball>().GetComponent<SpriteRenderer>().sprite = axeAndChainsaw[0];
    }
    private void SwapSpriteToChainsaw()
    {
        FindObjectOfType<Ball>().GetComponent<SpriteRenderer>().sprite = axeAndChainsaw[1];
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

    public void ActivePowerUpRestart()
    {
        StartCoroutine(PowerUpRestart());
    }
    IEnumerator PowerUpRestart()
    {
        IsPowerUpRestartActive = true;
        yield return new WaitForSeconds(powerUpRestartTimeEffect);
        IsPowerUpRestartActive = false;
    }

    //public void PowerUpMultiBall()
    //{
    //    if (FindObjectOfType<Ball>() == null) { return; }

    //    if (IfAllBallsHasStarted()) { return; }

    //    if (FindObjectOfType<BallsCounter>().BallsInPlay() >= maxBallsInPlay) { return; }

    //    Ball oldBall = FindObjectOfType<Ball>();
    //    Vector3 oldVelocity = oldBall.GetComponent<Rigidbody2D>().velocity;
    //    bool oldHasStarted = oldBall.GetComponent<Ball>().HasStarted();

    //    Instantiate(ball, oldBall.transform.position + offsetForSpawningBall, Quaternion.identity);
    //    Instantiate(ball, oldBall.transform.position - offsetForSpawningBall, Quaternion.identity);

    //    Ball[] balls = FindObjectsOfType<Ball>();
    //    foreach (var ball in balls)
    //    {
    //        ball.GetComponent<Ball>().SetHasStarted(oldHasStarted);
    //        ball.GetComponent<Rigidbody2D>().velocity = oldVelocity;
    //    }
    //}

    //private bool IfAllBallsHasStarted()
    //{
    //    Ball[] ballsInGameplay = FindObjectsOfType<Ball>();

    //    foreach (var ball in ballsInGameplay)
    //    {
    //        if (ball.HasStarted())
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
}
