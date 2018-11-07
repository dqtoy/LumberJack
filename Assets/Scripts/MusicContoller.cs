using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicContoller : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioClip _forestMusic;
    [Range(0, 1)] [SerializeField] float _forestMusicVolume;
    [SerializeField] AudioClip _swampMusic;
    [Range(0, 1)] [SerializeField] float _swampMusicVolume;
    [SerializeField] AudioClip _mountainMusic;
    [Range(0, 1)] [SerializeField] float _mountianMusicVolume;
    [SerializeField] AudioClip _eagleMusic;
    [Range(0, 1)] [SerializeField] float _eagleMusicVolume;
    AudioSource musicAudioScource;

    // singleton patter implementation
    private static MusicContoller gameStatus = null;

    private void Awake()
    {
        if (gameStatus == null)
        {
            gameStatus = this;
            musicAudioScource = GetComponent<AudioSource>();
        }
        else if (gameStatus != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        audioMixer.SetFloat("musicLevel", PlayerPreferenceController.GetMusicVolume());
        audioMixer.SetFloat("SFXLevel", PlayerPreferenceController.GetSFXVolume());
    }

    public void SwitchMusicForLevels(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex <= 6)
        {
            SwitchClipAndSetVolumeAndPlay(_forestMusic, _forestMusicVolume);
        }
        else if (levelIndex > 6 && levelIndex <= 12)
        {
            SwitchClipAndSetVolumeAndPlay(_swampMusic, _swampMusicVolume);
        }
        else if (levelIndex > 12 && levelIndex <= 17)
        {
            SwitchClipAndSetVolumeAndPlay(_mountainMusic, _mountianMusicVolume);
        }
        else if (levelIndex == 18)
        {
            SwitchClipAndSetVolumeAndPlay(_eagleMusic, _eagleMusicVolume);
        }
        else if (levelIndex == 19)
        {
            SwitchClipAndSetVolumeAndPlay(_forestMusic, _forestMusicVolume);
        }
        else
        {
            SwitchClipAndSetVolumeAndPlay(_mountainMusic, _mountianMusicVolume);
        }
    }

    private void SwitchClipAndSetVolumeAndPlay(AudioClip clip, float clipVolume)
    {
        if (musicAudioScource.clip == clip) { return; }
        musicAudioScource.clip = clip;
        musicAudioScource.volume = clipVolume;
        musicAudioScource.Play();
    }

}
