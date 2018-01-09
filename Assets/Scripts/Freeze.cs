using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

	public float freezeTimeOfPlayer2 = 3.0f;
	private bool isTriggered = false;
	public GameObject freeze;
	public Sprite freezeSprite;
    public Material freezeMaterial;
    public Material standardMaterial;

	void Update () {

		if (!StaticOptions.spawnItems.Exists (x => x == freeze)) {
			Destroy (freeze);
		}

		if (isTriggered) {
			if (Player2Controller.p2GamePad) {
				if (Input.GetKeyDown ("joystick 2 button 6") && !GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed) {
					//P1ItemIcon.itemSprite = freezeSprite;
					//P1ItemIcon.iconColor = Color.red;
					P2ItemIcon.iconColor = Color.red;
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped = true;
					//P1ItemCountDown.started = true;
					P2ItemCountDown.started = true;
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed = true;
					//P1ItemCountDown.itemText = "Freeze\n" + "Time Remaining: ";
					P2ItemCountDown.itemText = "Freeze Used\n" + "Time Remaining: ";
				}

				if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped) {
				    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = freezeMaterial;
                    //P1ItemCountDown.itemTimeRemaining = freezeTimeOfPlayer2;
					P2ItemCountDown.itemTimeRemaining = freezeTimeOfPlayer2;
					freezeTimeOfPlayer2 -= Time.deltaTime;
					if (freezeTimeOfPlayer2 < 0) {
						//P1ItemIcon.itemSprite = null;
						P2ItemIcon.itemSprite = null;
						//P1ItemCountDown.started = false;
						P2ItemCountDown.started = false;
						GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed = false;
						GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped = false;
						//P1ItemCountDown.itemText = "No item";
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						Destroy (freeze);
					    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
                    }
				}
			} else if (!Player2Controller.p2GamePad) {
				if (Input.GetKeyDown (KeyCode.Alpha9) && !GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed) {
					//P1ItemIcon.itemSprite = freezeSprite;
					//P1ItemIcon.iconColor = Color.red;
					P2ItemIcon.iconColor = Color.red;
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped = true;
					//P1ItemCountDown.started = true;
					P2ItemCountDown.started = true;
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed = true;
					//P1ItemCountDown.itemText = "Freeze\n" + "Time Remaining: ";
					P2ItemCountDown.itemText = "Freeze Used\n" + "Time Remaining: ";
				}

				if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped) {
				    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = freezeMaterial;
                    //P1ItemCountDown.itemTimeRemaining = freezeTimeOfPlayer2;
					P2ItemCountDown.itemTimeRemaining = freezeTimeOfPlayer2;
					freezeTimeOfPlayer2 -= Time.deltaTime;
					if (freezeTimeOfPlayer2 < 0) {
						//P1ItemIcon.itemSprite = null;
						P2ItemIcon.itemSprite = null;
						//P1ItemCountDown.started = false;
						P2ItemCountDown.started = false;
						GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed = false;
						GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped = false;
						//P1ItemCountDown.itemText = "No item";
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						Destroy (freeze);
					    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
                    }
				}
			}
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			StaticOptions.spawnItems.Remove (freeze);
			Destroy (freeze);
		}
	}

	void OnTriggerEnter(Collider col) {
		
		if (col.gameObject.tag == "block") {
			
			StaticOptions.spawnItems.Remove (freeze);
			if (P2ItemCountDown.itemText != "No item") {
				Destroy (GameObject.FindGameObjectWithTag("p2TakenItem"));
			}
			freeze.tag = "p2TakenItem";
			isTriggered = true;
			if (Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Freeze the opponent\n" + "Press L2 to use";
			} else if (!Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Freeze the opponent\n" + "Press 9 to use";
			}
			P2ItemIcon.itemSprite = freezeSprite;
			freeze.GetComponent<SphereCollider> ().enabled = false;
			for (int i = 0; i < freeze.transform.childCount; i++) {
				freeze.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
}
