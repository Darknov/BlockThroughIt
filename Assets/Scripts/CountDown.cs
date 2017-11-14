using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

	public static float timeRemaining = 60;
	Text text;
	public static bool started = false;
		
	void Start () {
		text = GetComponent<Text>();
	}

	void Update () {

		text.text = "Time Remaining: " + (int)timeRemaining;

		if (!started) {
			return;
		}

		if (timeRemaining >= 0 && GameObject.FindWithTag("Player")) {
			timeRemaining -= Time.deltaTime;
		}
	}
}
