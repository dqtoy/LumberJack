using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudio : MonoBehaviour
{
    // brick sound
    public static AudioClip[] woodChopSound = new AudioClip[0];
    public static float chopVolume = 1f;
    public static AudioClip destoryBrick;
    //Bounce sounds
    public static AudioClip bounceAndClick;
    public static float bounceVolume = 1f;
    // stone hit
    public static AudioClip stoneHit;
    public static float stoneVolume = 1f;


    static AudioSource audioSource;

    // implementing singleton pattern!!
    private static SFXAudio gameStatus = null;

    private void Awake()
    {
        if (gameStatus == null)
        {
            gameStatus = this;
        }
        else if (gameStatus != this)
        {
            DestroyImmediate(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void BounceSound()
    {
        audioSource.PlayOneShot(bounceAndClick, bounceVolume);
    }

}
