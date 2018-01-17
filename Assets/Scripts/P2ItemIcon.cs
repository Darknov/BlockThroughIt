﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2ItemIcon : MonoBehaviour {

	public static Sprite itemSprite = null;
	public static Color iconColor = Color.white;
    public GameObject partEffect;
    Image image;
    public GameObject player2Hint;

    private bool boom = true;
    private Sprite checkPickup;

    void Start () {
		image = GetComponent<Image>();
	}

	void Update() {

        if (itemSprite != null) {
            player2Hint.SetActive(true);

            if (checkPickup != itemSprite)
            {
                boom = true;
            }
            if (boom)
            {
                Instantiate(partEffect, new Vector3(8, -0, -9.1f), new Quaternion());

               // Instantiate(partEffect, GameObject.FindGameObjectWithTag("godsRing").transform.position, GameObject.FindGameObjectWithTag("godsRing").transform.rotation);
                if(FindObjectOfType<AudioManager>()!=null)FindObjectOfType<AudioManager>().Play("godGetItem");
                boom = false;
            }
            image.color = iconColor;
			image.enabled = true;
			image.sprite = itemSprite;
            checkPickup = itemSprite;

        }
        else {
			iconColor = Color.white;
			image.enabled = false;
		}
	}
}