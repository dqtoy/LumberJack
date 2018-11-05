using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonsController : MonoBehaviour
{

    [SerializeField] Button[] levelButtons;
    [SerializeField] Sprite levelAvaliable;
    [SerializeField] Sprite levelUnavaliable;
    [SerializeField] int sceneBuildOffset;


    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (PlayerPreferenceController.IsLevelWithThisNumberUnlocked(i + sceneBuildOffset))
            {
                SetButtonActive(i);
            }
            else
            {
                SetButtonDeactive(i);
            }
        }
    }

    private void SetButtonDeactive(int i)
    {
        levelButtons[i].interactable = false;
        levelButtons[i].GetComponent<Image>().sprite = levelUnavaliable;
    }

    private void SetButtonActive(int i)
    {
        levelButtons[i].interactable = true;
        levelButtons[i].GetComponent<Image>().sprite = levelAvaliable;
    }

    public void LockAllLevelsExludeOne()
    {
        for (int i = 1; i < levelButtons.Length; i++)
        {
            PlayerPreferenceController.LockLeverNumber(i+sceneBuildOffset);
            SetButtonDeactive(i);
        }
    }

    public void UnlockAllLevels()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            PlayerPreferenceController.UnlockLevelNumber(i+sceneBuildOffset);
            SetButtonActive(i);
        }
    }
}
