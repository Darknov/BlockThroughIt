using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public static float timeRemaining = 60;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timeRemaining >= 0 && GameObject.FindWithTag("Player")) {
			timeRemaining -= Time.deltaTime;
		}
			
		if (timeRemaining <= 0) {
			anim.SetTrigger ("GameOver1");
		}

		if (!GameObject.FindWithTag("Player")) {
			anim.SetTrigger ("GameOver2");
		}
	}
}
