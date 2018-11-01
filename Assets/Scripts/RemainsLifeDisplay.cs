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
        //liveText.text = "x" + FindObjectOfType<GameSession>().GetCurrentLife().ToString();
        UpdateLive(FindObjectOfType<GameSession>().GetCurrentLife());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLive(int lifes)
    {
        //liveText.text = "x" + FindObjectOfType<GameSession>().GetCurrentLife().ToString();
        liveText.text = "x" + lifes.ToString();
    }
}