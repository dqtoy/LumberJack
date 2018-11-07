using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPreferenceController : MonoBehaviour
{
    const string MUSIC_VOLUME_KEY = "music_volume";
    const string SFX_VOLUME_KEY = "SFX_volume";
    const string LEVEL_KEY = "level_number_";
    const string INTRO_PANEL = "intro_panel_status";

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

    public static void TurnIntroOFF()
    {
        PlayerPrefs.SetInt(INTRO_PANEL, 0);
    }

    public static void TurnIntroON()
    {
        PlayerPrefs.SetInt(INTRO_PANEL, 1);
    }

    public static bool IsIntroPanelsOn()
    {
        int introPanelsValue = PlayerPrefs.GetInt(INTRO_PANEL);
        if (introPanelsValue == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void LockLeverNumber(int levelNumber)
    {
        if (levelNumber <= SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + levelNumber.ToString(), 0);
        }
        else
        {
            Debug.LogWarning("Level is out of range in build setting!");
            return;
        }
    }

    public static void UnlockLevelNumber(int levelNumber)
    {
        if (levelNumber <= SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + levelNumber.ToString(), 1);
        }
        else
        {
            Debug.LogWarning("Level is out of range in build setting!");
            return;
        }
    }

    public static bool IsLevelWithThisNumberUnlocked(int levelNumber)
    {
        int storedLevelValue = PlayerPrefs.GetInt(LEVEL_KEY + levelNumber.ToString());
        if (storedLevelValue == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
