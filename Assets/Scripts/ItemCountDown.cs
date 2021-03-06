﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCountDown : MonoBehaviour {

	public static float itemTimeRemaining = 5f;
	public static string itemText = "";
	public static bool started = false;
	Text text;

	void Start () {
		text = GetComponent<Text>();
	}

	void Update () {

		text.text = "No item";

		if (started) {
			text.text = itemText + Mathf.Round (itemTimeRemaining * 100f) / 100f;
		}
	}
}