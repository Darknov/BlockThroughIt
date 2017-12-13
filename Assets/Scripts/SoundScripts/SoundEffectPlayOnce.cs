using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayOnce : MonoBehaviour {

    public AudioClip Clip;

    public AudioSource source;


    void Awake()
    {
        source.clip = Clip;
        source.volume = StaticOptions.AudioVolume;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void PlaySound()
    {
        source.Play();
    }
}
