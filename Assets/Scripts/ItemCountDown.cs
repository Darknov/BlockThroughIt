using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCountDown : MonoBehaviour {

	public static float itemTimeRemaining = 5f;
	Text text;
	public static bool started = false;

	void Start () {
		text = GetComponent<Text>();
	}

	void Update () {

		text.text = "No item";

		if (started) {
			text.text = "Time Remaining: " + (int)itemTimeRemaining;
		}
	}
}
