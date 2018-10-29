using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreferenceController : MonoBehaviour
{
    // creating strings key
    const string MUSIC_VOLUME_KEY = "music_volume";
    const string SFX_VOLUME_KEY = "SFX_volume";


    //control of music level via SET/GET
    public static void SetMusicVolume(float volume)
    {
        if (volume >= -80f && volume <= 20f)
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Music Volume Out Of Range!");
        }
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
    }

    // control of SFX via SET?GET

    public static void SetSFXVolume(float volume)
    {
        if (volume >= -80f && volume <= 20f)
        {
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("SFX Volume Out Of Range!");
        }
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
    }
}
