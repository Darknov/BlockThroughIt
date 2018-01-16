using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1ItemIcon : MonoBehaviour {

	public static Sprite itemSprite = null;
	public static Color iconColor = Color.white;
    public GameObject partEffect;
    public GameObject ring;

    Image image;
    private bool boom = true;
    private Sprite checkPickup;

	void Start () {
		image = GetComponent<Image>();
        Instantiate(ring, new Vector3(-7.5f, 0, -9), new Quaternion(0, 90, 90, 2));
        Instantiate(ring, new Vector3(7.5f, 0, -9.1f), new Quaternion(0, 90, 90, 2));


    }

    void Update() {
		if (itemSprite != null) {
            if (checkPickup != itemSprite)
            {
                boom = true;
            }
            if (boom )
            {
                Instantiate(partEffect, new Vector3(-8, -0, -9), new Quaternion());

               // Instantiate(partEffect, GameObject.FindGameObjectWithTag("rabbitsRing").transform.position, GameObject.FindGameObjectWithTag("rabbitsRing").transform.rotation);
                FindObjectOfType<AudioManager>().Play("GettingItem");
                boom = false;
            }

            image.color = iconColor;
			image.enabled = true;
			image.sprite = itemSprite;
            checkPickup = itemSprite;
            
		} else {
			iconColor = Color.white;
			image.enabled = false;
            boom = true;
        }

	}
}
