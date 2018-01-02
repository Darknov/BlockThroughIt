using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (PlayerPrefs.GetInt("whichSlider") == 1)
        {
            GetComponent<AudioSource>().volume = StaticOptions.AudioVolumeMaster;
        }
        if (PlayerPrefs.GetInt("whichSlider") == 2)
        {
            GetComponent<AudioSource>().volume = StaticOptions.AudioVolumeEffects;
        }
        if (PlayerPrefs.GetInt("whichSlider") == 3 && GetComponent<AudioSource>().clip.name == "Mecha_Action")
        {
            GetComponent<AudioSource>().volume = StaticOptions.AudioVolumeBackground;
        }
    }
}
