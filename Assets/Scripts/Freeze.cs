﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

	public float freezeTimeOfPlayer2 = 3.0f;
	private float freezeTimeCountDown;
	private bool isTriggered = false;
	public GameObject freeze;
	public Sprite freezeSprite;
    public Material freezeMaterial;
    public Material standardMaterial;

	void Update () {
		
		if (isTriggered) {
			if (Player2Controller.p2GamePad) {
				if (Input.GetKeyDown ("joystick 2 button 6") && !GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed) {
                    FindObjectOfType<AudioManager>().Play("freeze");
					P2ItemIcon.iconColor = Color.red;
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped = true;
					P2ItemCountDown.started = true;
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed = true;
					P2ItemCountDown.itemText = "Freeze Used\n" + "Time Remaining: ";
				}

				if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped) {
				    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = freezeMaterial;
					P2ItemCountDown.itemTimeRemaining = freezeTimeCountDown;
					GameObject.FindGameObjectWithTag("evilTimeBar").SendMessage("SubTime", freezeTimeOfPlayer2);
					freezeTimeCountDown -= Time.deltaTime;
					if (freezeTimeCountDown < 0) {
						P2ItemIcon.iconColor = Color.white;
						P2ItemIcon.itemSprite = null;
						P2ItemCountDown.started = false;
						GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed = false;
						GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped = false;
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						StaticOptions.p2SpawnItems.Remove (freeze);
					    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
						Destroy (freeze);
                    }
				}
			} else if (!Player2Controller.p2GamePad) {
				if (Input.GetKeyDown (KeyCode.Alpha9) && !GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed) {
                    FindObjectOfType<AudioManager>().Play("freeze");
					P2ItemIcon.iconColor = Color.red;
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped = true;
					P2ItemCountDown.started = true;
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed = true;
					P2ItemCountDown.itemText = "Freeze Used\n" + "Time Remaining: ";
				}

				if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped) {
				    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = freezeMaterial;
					P2ItemCountDown.itemTimeRemaining = freezeTimeCountDown;
					freezeTimeCountDown -= Time.deltaTime;
					if (freezeTimeCountDown < 0) {
						P2ItemIcon.iconColor = Color.white;
						P2ItemIcon.itemSprite = null;
						P2ItemCountDown.started = false;
						GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed = false;
						GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped = false;
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						StaticOptions.p2SpawnItems.Remove (freeze);
					    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
						Destroy (freeze);
                    }
				}
			}
		}
	}

	void LateUpdate() {
		if (!StaticOptions.p2SpawnItems.Exists (x => x == freeze)) {
			Destroy (freeze);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			StaticOptions.p2SpawnItems.Remove (freeze);
			Destroy (freeze);
		}
	}

	void OnTriggerEnter(Collider col) {
				
		if (col.gameObject.tag == "block") {
            FindObjectOfType<AudioManager>().Play("godGetItem");
			if (P2ItemCountDown.itemText != "No item") {
				P2ItemIcon.iconColor = Color.white;
				P2ItemIcon.itemSprite = null;
				P2ItemCountDown.started = false;
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStoppedUsed = false;
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1Controller> ().IsPlayerStopped = false;
				P2ItemCountDown.itemText = "No item";
				isTriggered = false;
				StaticOptions.p2SpawnItems.Remove (GameObject.FindGameObjectWithTag("p2TakenItem"));
				GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
				Destroy (GameObject.FindGameObjectWithTag("p2TakenItem"));
			}
			freezeTimeCountDown = freezeTimeOfPlayer2;
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
