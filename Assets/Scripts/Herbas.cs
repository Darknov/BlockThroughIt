using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Herbas : MonoBehaviour {

	public GameObject herbas;
	public Sprite herbasSprite;
	public float flyingDuration = 5f;
	private bool isTriggered = false;

	void Update() {

		if (!StaticOptions.p1SpawnItems.Exists (x => x == herbas)) {
			Destroy (herbas);
		}
			
		if (isTriggered) {
			if (!Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown ("joystick 2 button 6")) {
					P1ItemIcon.iconColor = Color.green;
					StaticOptions.isFlying = true;
					GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = false;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Herbas Flying Boots\n" + "Time Remaining: ";
					P1ItemCountDown.itemTimeRemaining = flyingDuration;
					flyingDuration -= Time.deltaTime;
					if ((int)flyingDuration <= 0) {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
						isTriggered = false;
						Destroy (herbas);
					}
				}
			} else if (Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
					P1ItemIcon.iconColor = Color.green;
					StaticOptions.isFlying = true;
					GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = false;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Herbas Flying Boots\n" + "Time Remaining: ";
					P1ItemCountDown.itemTimeRemaining = flyingDuration;
					flyingDuration -= Time.deltaTime;
					if ((int)flyingDuration <= 0) {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
						isTriggered = false;
						Destroy (herbas);
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision col) {
		
		if (col.gameObject.tag == "Player") {
			StaticOptions.p1SpawnItems.Remove (herbas);
			if (P1ItemCountDown.itemText != "No item") {
				Destroy (GameObject.FindGameObjectWithTag("p1TakenItem"));
			}
			herbas.tag = "p1TakenItem";
			isTriggered = true;
			if (!Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Flying boots\n" + "Press L2 to use";
			} else if (Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Flying boots\n" + "Press Tab to use";
			}
			P1ItemIcon.itemSprite = herbasSprite;
			herbas.GetComponent<SphereCollider> ().enabled = false;
			for (int i = 0; i < herbas.transform.childCount; i++) {
				herbas.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
}