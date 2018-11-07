using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefBasics : MonoBehaviour {

	void Start () {
        PlayerPreferenceController.UnlockLevelNumber(1);	
	}
}
