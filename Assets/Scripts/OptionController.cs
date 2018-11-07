using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionController : MonoBehaviour
{

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] int sceneBuildOffsetToLock;
    [SerializeField] Toggle DontShowIntro;

    void Start()
    {
        musicSlider.value = PlayerPreferenceController.GetMusicVolume();
        SFXSlider.value = PlayerPreferenceController.GetSFXVolume();
        DontShowIntro.isOn = !PlayerPreferenceController.IsIntroPanelsOn();
    }

    void Update()
    {
        audioMixer.SetFloat("musicLevel", musicSlider.value);
        audioMixer.SetFloat("SFXLevel", SFXSlider.value);
    }

    public void SafeAndExitToMainMenu()
    {
        PlayerPreferenceController.SetMusicVolume(musicSlider.value);
        PlayerPreferenceController.SetSFXVolume(SFXSlider.value);
        sceneLoader.LoadMainMenu();
    }

    public void SafeAndResume()
    {
        PlayerPreferenceController.SetMusicVolume(musicSlider.value);
        PlayerPreferenceController.SetSFXVolume(SFXSlider.value);
        sceneLoader.BackToGamePlay();
    }

    public void LockAllLevelsExludeOne()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings -1; i++)
        {
            PlayerPreferenceController.LockLeverNumber(i + sceneBuildOffsetToLock);
        }
        PlayerPreferenceController.UnlockLevelNumber(1);
    }

    public void IntroONOFF(bool value)
    {
        if (value)
        {
            PlayerPreferenceController.TurnIntroOFF();
            Debug.Log(PlayerPreferenceController.IsIntroPanelsOn());
        }
        else
        {
            PlayerPreferenceController.TurnIntroON();
            Debug.Log(PlayerPreferenceController.IsIntroPanelsOn());
        }
    }
}
