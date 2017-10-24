using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update () {
		if (CountDown.timeRemaining <= 0) {
			anim.SetTrigger ("GameOver1");
		}

		if (!GameObject.FindWithTag("Player")) {
			anim.SetTrigger ("GameOver2");
		}
	}
}
