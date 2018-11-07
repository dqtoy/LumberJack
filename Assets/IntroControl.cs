using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroControl : MonoBehaviour
{

    [SerializeField] GameObject[] _introPanels;

    void Start()
    {
        if (PlayerPreferenceController.IsIntroPanelsOn())
        {
            _introPanels[0].SetActive(true);
        }
        else
        {
            _introPanels[0].SetActive(false);
        }
    }

    public void GoToNextPanel(int panelNumber)
    {
        _introPanels[panelNumber + 1].SetActive(true);
        _introPanels[panelNumber].SetActive(false);
    }

    public void LastPanel(int panelNumber)
    {
        _introPanels[panelNumber].SetActive(false);
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
