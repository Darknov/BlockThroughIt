using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1ItemIcon : MonoBehaviour {

	public static Sprite itemSprite = null;
	public static Color iconColor = Color.white;
    public GameObject partEffect;
    public GameObject player1Hint;
    public GameObject ring;

    Image image;
    private bool boom = true;
    private Sprite checkPickup;

	void Start () {
		image = GetComponent<Image>();
        Instantiate(ring, new Vector3(-8f, 0, -8.6f), new Quaternion(0, 90, 90, 2));
        Instantiate(ring, new Vector3(8f, 0, -8.6f), new Quaternion(0, 90, 90, 2));


    }

    void Update() {
		if (itemSprite != null) {
            player1Hint.SetActive(true);

            if (checkPickup != itemSprite)
            {
                boom = true;

            }
            if (boom )
            {
                Instantiate(partEffect, new Vector3(-8, -0, -9), new Quaternion());

               // Instantiate(partEffect, GameObject.FindGameObjectWithTag("rabbitsRing").transform.position, GameObject.FindGameObjectWithTag("rabbitsRing").transform.rotation);
                if(FindObjectOfType<AudioManager>()!=null) FindObjectOfType<AudioManager>().Play("GettingItem");

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
            player1Hint.SetActive(false);

        }

    }
}
