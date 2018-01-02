using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

	public static float timeRemaining = 60f;
	public float warningTime = 5f;
    Text text;
	public static bool started = false;
		
	void Start () {
		text = GetComponent<Text>();
        InvokeRepeating ("Blink", 0, 1f);
		InvokeRepeating ("Blink2", 0.5f, 1f);

	}

	void Update () {

		text.text = "Time Remaining: " + Mathf.Round (timeRemaining * 100f) / 100f;

		if (!started) {
			return;
		}

		if (timeRemaining >= 0 && GameObject.FindWithTag ("Player")) {
			timeRemaining -= Time.deltaTime;
		} else if(timeRemaining <= 0){
			timeRemaining = 0.00f;
		}
	}

	void Blink() {
		if (timeRemaining <= warningTime) {
			text.color = Color.red;
			text.fontSize = 21;
		}
	}

	void Blink2() {
		if (timeRemaining <= warningTime) {
			text.color = Color.red;
			text.fontSize = 20;
		}
	}
}
