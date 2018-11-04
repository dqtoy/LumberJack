using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RemainsLifeDisplay : MonoBehaviour
{
    TextMeshProUGUI liveText;

    void Start()
    {
        liveText = GetComponent<TextMeshProUGUI>();
        UpdateLive(FindObjectOfType<GameSession>().GetCurrentLife());
    }

    public void UpdateLive(int lifes)
    {
        liveText.text = "x" + lifes.ToString();
    }
}