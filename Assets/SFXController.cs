using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{

    AudioSource sfxAudioSource;
    [SerializeField] AudioClip[] crackSound;
    [Range(0, 1)] [SerializeField] float crackVolume;
    [SerializeField] AudioClip blockDestory;
    [Range(0, 1)] [SerializeField] float destoryVolume;
    [SerializeField] AudioClip bounceSound;
    [Range(0, 1)] [SerializeField] float bounceVolume;
    [SerializeField] AudioClip stoneSound;
    [Range(0, 1)] [SerializeField] float stoneVolume;
    [SerializeField] AudioClip chainsawSound;
    [Range(0, 1)] [SerializeField] float chainsawVolume;
    [SerializeField] AudioClip eagle;
    [Range(0, 1)] [SerializeField] float eagleVolume;

    private static SFXController sFXController = null;
    private void Awake()
    {
        if (sFXController != null && sFXController != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            sFXController = this;
            DontDestroyOnLoad(gameObject);
            sfxAudioSource = GetComponent<AudioSource>();
        }
    }

    public void PlaySFX(Collision2D collider)
    {
        if (collider.gameObject.tag == "Brick")
        {
            int index = Random.Range(0, crackSound.Length);
            sfxAudioSource.PlayOneShot(crackSound[index], crackVolume);
        }
        else if (collider.gameObject.tag == "Stone")
        {
            sfxAudioSource.PlayOneShot(stoneSound, stoneVolume);
        }
        else
        {
            sfxAudioSource.PlayOneShot(bounceSound, bounceVolume);
        }
    }

    public void PlayChainsawAudio()
    {
        sfxAudioSource.PlayOneShot(chainsawSound, chainsawVolume);
    }

    public void PlayDestroyBlock()
    {
        sfxAudioSource.PlayOneShot(blockDestory, destoryVolume);
    }

    public void PlayHitEagle()
    {
        sfxAudioSource.PlayOneShot(eagle, eagleVolume);
    }
}
