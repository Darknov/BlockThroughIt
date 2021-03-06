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
	public GameObject partEffect;
	private static bool freezeSpecialEffects = false;

	void Update () {
		
		if (isTriggered) {
			if (Player2Controller.p2GamePad) {
                if ((Input.GetKeyDown ("joystick 2 button 4")|| Input.GetKeyDown("joystick 2 button 6")) && !Player1Controller.IsPlayerStoppedUsed) {
                    Debug.Log("---");
                    Debug.Log(Input.GetKeyDown("joystick 2 button 1"));
                    Debug.Log(Input.GetKeyDown("joystick 2 button 2"));
                    Debug.Log(Input.GetKeyDown("joystick 2 button 3"));
                    Debug.Log(Input.GetKeyDown("joystick 2 button 4"));
                    Debug.Log(Input.GetKeyDown("joystick 2 button 5"));
                    Debug.Log(Input.GetKeyDown("joystick 2 button 6"));
                    Debug.Log(Input.GetKeyDown("joystick 2 button 7"));
                    Debug.Log(Input.GetKeyDown("joystick 2 button 8"));
                    Debug.Log(Input.GetKeyDown("joystick 2 button 9"));
                    P2ItemIcon.iconColor = Color.blue;
                    Player1Controller.IsPlayerStopped = true;
					P2ItemCountDown.started = true;
                    Player1Controller.IsPlayerStoppedUsed = true;
					P2ItemCountDown.itemText = "Rabbit froze up";
					if (freezeSpecialEffects) {
						if(FindObjectOfType<AudioManager>()!=null) FindObjectOfType<AudioManager>().Play("freeze");
						Instantiate(partEffect, 
							new Vector3(GameObject.FindWithTag ("Player").GetComponent<Transform>().position.x, 
								GameObject.FindWithTag ("Player").GetComponent<Transform>().position.y, 
								GameObject.FindWithTag ("Player").GetComponent<Transform>().position.z
							), 
							GameObject.FindWithTag ("Player").GetComponent<Transform>().rotation,  
							GameObject.FindWithTag ("Player").GetComponent<Transform>().transform
						);
						freezeSpecialEffects = false;
					}
				}

				if (Player1Controller.IsPlayerStopped) {
				    //GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = freezeMaterial;
					//P2ItemCountDown.itemTimeRemaining = freezeTimeCountDown;
					freezeTimeCountDown -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("evilTimeBar").GetComponent<TimeBar>().maxhitpoint = freezeTimeOfPlayer2;

                    GameObject.FindGameObjectWithTag("evilTimeBar").SendMessage("SubTime", freezeTimeCountDown);
					if (freezeTimeCountDown < 0) {
						P2ItemIcon.iconColor = Color.white;
						P2ItemIcon.itemSprite = null;
						P2ItemCountDown.started = false;
                        Player1Controller.IsPlayerStoppedUsed = false;
                        Player1Controller.IsPlayerStopped = false;
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						StaticOptions.p2SpawnItems.Remove (freeze);
						if (GameObject.FindGameObjectWithTag("p2particle")!=null)
						GameObject.FindWithTag ("p2particle").transform.parent = null;
						Destroy (GameObject.FindWithTag ("p2particle"));
					    //GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
						Destroy (freeze);
                    }
				}
			} else if (!Player2Controller.p2GamePad) {
				if (Input.GetKeyDown (KeyCode.Alpha9) && !Player1Controller.IsPlayerStoppedUsed) {
					P2ItemIcon.iconColor = Color.blue;
                    Player1Controller.IsPlayerStopped = true;
					P2ItemCountDown.started = true;
                    Player1Controller.IsPlayerStoppedUsed = true;
					P2ItemCountDown.itemText = "Rabbit froze up";
					if (freezeSpecialEffects) {
						if(FindObjectOfType<AudioManager>()!=null) FindObjectOfType<AudioManager>().Play("freeze");
						Instantiate(partEffect, 
							new Vector3(GameObject.FindWithTag ("Player").GetComponent<Transform>().position.x, 
								GameObject.FindWithTag ("Player").GetComponent<Transform>().position.y, 
								GameObject.FindWithTag ("Player").GetComponent<Transform>().position.z
							), 
							GameObject.FindWithTag ("Player").GetComponent<Transform>().rotation,  
							GameObject.FindWithTag ("Player").GetComponent<Transform>().transform
						);
						freezeSpecialEffects = false;
					}
				}

				if (Player1Controller.IsPlayerStopped) {
				    //GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = freezeMaterial;
					//P2ItemCountDown.itemTimeRemaining = freezeTimeCountDown;
					freezeTimeCountDown -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("evilTimeBar").GetComponent<TimeBar>().maxhitpoint = freezeTimeOfPlayer2;

                    GameObject.FindGameObjectWithTag("evilTimeBar").SendMessage("SubTime", freezeTimeCountDown);

                    if (freezeTimeCountDown < 0) {
						P2ItemIcon.iconColor = Color.white;
						P2ItemIcon.itemSprite = null;
						P2ItemCountDown.started = false;
                        Player1Controller.IsPlayerStoppedUsed = false;
                        Player1Controller.IsPlayerStopped = false;
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						StaticOptions.p2SpawnItems.Remove (freeze);
						if (GameObject.FindGameObjectWithTag("p2particle")!=null)
						GameObject.FindWithTag ("p2particle").transform.parent = null;
						Destroy (GameObject.FindWithTag ("p2particle"));
					    //GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
						Destroy (freeze);
                    }
				}
			}
		}
	}

	/*void LateUpdate() {
		if (!StaticOptions.p2SpawnItems.Exists (x => x == freeze)) {
			Destroy (freeze);
		}
	}*/

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			StaticOptions.p2SpawnItems.Remove (freeze);
			Destroy (freeze);
		}
	}

	void OnTriggerEnter(Collider col) {

		if (col.gameObject.tag == "platform") {
			StaticOptions.p2SpawnItems.Remove (freeze);
			Destroy (freeze);
		}
				
		if (col.gameObject.tag == "block") {
            if(FindObjectOfType<AudioManager>()!=null) FindObjectOfType<AudioManager>().Play("godGetItem");
			if (P2ItemCountDown.itemText != "No item") {
                GameObject.FindGameObjectWithTag("evilTimeBar").SendMessage("SubTime", 0f);
                P2ItemIcon.iconColor = Color.white;
				P2ItemIcon.itemSprite = null;
				P2ItemCountDown.started = false;
                Player1Controller.inverseControlUsed = false;
                Player1Controller.inverseControl = false;
                Player1Controller.IsPlayerStoppedUsed = false;
                Player1Controller.IsPlayerStopped = false;
                Player2Controller.isDestroyBlockAvailable = false;
                Player2Controller.isDestroyBlockActivated = false;
                P2ItemCountDown.itemText = "No item";
				isTriggered = false;
				if (GameObject.FindWithTag ("p2particle") != null) {
					GameObject.FindWithTag ("p2particle").transform.parent = null;
					Destroy (GameObject.FindWithTag ("p2particle"));
				}
				StaticOptions.p2SpawnItems.Remove (GameObject.FindGameObjectWithTag("p2TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag("p2TakenItem"));
			}
			freezeSpecialEffects = true;
			freezeTimeCountDown = freezeTimeOfPlayer2;
			freeze.tag = "p2TakenItem";
			isTriggered = true;
			if (Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Freeze";
			} else if (!Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Freeze";
			}
			P2ItemIcon.itemSprite = freezeSprite;
			freeze.GetComponent<SphereCollider> ().enabled = false;
			for (int i = 0; i < freeze.transform.childCount; i++) {
				freeze.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
}
