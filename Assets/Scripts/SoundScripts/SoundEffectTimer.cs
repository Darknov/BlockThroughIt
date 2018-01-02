using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTimer : MonoBehaviour
{

    //public GameObject SoundPlayer;
   // private AudioSource sound;
    private bool notPlayed = true;
    private CountDown countdown;
    // Use this for initialization

    void Awake()
    {
       // sound = SoundPlayer.GetComponent<AudioSource>();
    }

    void Start()
    {
        countdown  = GetComponent<CountDown>();
    }

    // Update is called once per frame
    void Update()
    {
		if(GameObject.FindGameObjectWithTag("countDown").GetComponent<CountDown>().timeRemaining < countdown.warningTime && notPlayed)
        {
            // sound.Play();
            FindObjectOfType<AudioManager>().Play("EndingMusic");
            notPlayed = false;
        }
    }


}