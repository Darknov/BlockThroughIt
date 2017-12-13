using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    public GameObject soundSource;
    private SoundEffectPlayOnce sound;

	// Use this for initialization
	void Start () {
		sound = sound.GetComponent<SoundEffectPlayOnce>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        sound.PlaySound();
    }
}

