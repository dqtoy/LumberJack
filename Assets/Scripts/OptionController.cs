using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionController : MonoBehaviour
{

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] SceneLoader sceneLoader;

    [SerializeField] AudioMixer audioMixer;

    // Use this for initialization
    void Start()
    {
        musicSlider.value = PlayerPreferenceController.GetMusicVolume();
        SFXSlider.value = PlayerPreferenceController.GetSFXVolume();
    }

    // Update is called once per frame
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


}
