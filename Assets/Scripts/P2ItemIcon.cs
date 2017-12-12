using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2ItemIcon : MonoBehaviour {

	public static Sprite itemSprite = null;
	public static Color iconColor = Color.white;
	Image image;

	void Start () {
		image = GetComponent<Image>();
	}

	void Update() {
		if (itemSprite != null) {
			image.color = iconColor;
			image.enabled = true;
			image.sprite = itemSprite;
		} else {
			iconColor = Color.white;
			image.enabled = false;
		}
	}
}