using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Herbas : MonoBehaviour {

	public GameObject herbas;
	public Sprite herbasSprite;
	public float flyingDuration = 5f;
<<<<<<< HEAD
	private float flyingCountDown;
=======
	private float flyingTimeCountDown;
>>>>>>> PO3
	private bool isTriggered = false;
	public GameObject partEffect;
	private Component[] meshRenderer;

	void Update() {
			
		isDestroyed ();

		if (isTriggered) {
			if (!Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown ("joystick 2 button 6")) {
					P1ItemIcon.iconColor = Color.green;
					StaticOptions.isFlying = true;
					GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = false;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Herbas Flying Boots\n" + "Time Remaining: ";
					P1ItemCountDown.itemTimeRemaining = flyingTimeCountDown;
					Instantiate(partEffect, 
						new Vector3(GameObject.FindWithTag ("Player").GetComponent<Transform>().position.x, 
							GameObject.FindWithTag ("Player").GetComponent<Transform>().position.y, 
							GameObject.FindWithTag ("Player").GetComponent<Transform>().position.z
						), 
						GameObject.FindWithTag ("Player").GetComponent<Transform>().rotation,  
						GameObject.FindWithTag ("Player").GetComponent<Transform>().transform
					);
				}
				if (StaticOptions.isFlying) {
<<<<<<< HEAD
					flyingCountDown -= Time.deltaTime;
					if ((int)flyingCountDown <= 0)
=======
					flyingTimeCountDown -= Time.deltaTime;
					if ((int)flyingTimeCountDown <= 0) {
>>>>>>> PO3
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
						isTriggered = false;
						StaticOptions.p1SpawnItems.Remove (herbas);
						partEffect.transform.parent = null;
						Destroy (partEffect.gameObject, 3);
					}
				}
			} else if (Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
					P1ItemIcon.iconColor = Color.green;
					StaticOptions.isFlying = true;
					GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = false;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Herbas Flying Boots\n" + "Time Remaining: ";
					Instantiate(partEffect, 
						new Vector3(GameObject.FindWithTag ("Player").GetComponent<Transform>().position.x, 
							GameObject.FindWithTag ("Player").GetComponent<Transform>().position.y, 
							GameObject.FindWithTag ("Player").GetComponent<Transform>().position.z
						), 
						GameObject.FindWithTag ("Player").GetComponent<Transform>().rotation,  
						GameObject.FindWithTag ("Player").GetComponent<Transform>().transform
					);
				}
				if (StaticOptions.isFlying) {
<<<<<<< HEAD
					P1ItemCountDown.itemTimeRemaining = flyingCountDown;
					flyingCountDown -= Time.deltaTime;
					if ((int)flyingDuration <= 0) {
=======
					P1ItemCountDown.itemTimeRemaining = flyingTimeCountDown;
					flyingTimeCountDown -= Time.deltaTime;
					if ((int)flyingTimeCountDown <= 0) {
>>>>>>> PO3
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
						isTriggered = false;
						StaticOptions.p1SpawnItems.Remove (herbas);
						partEffect.transform.parent = null;
						Destroy(partEffect.gameObject, 3);
					}
				}
			}
		}
	}

	void LateUpdate() {
		if (!StaticOptions.p1SpawnItems.Exists (x => x == herbas)) {
			Destroy (herbas);
		}
	}

	void isDestroyed() {

		if(!StaticOptions.p1SpawnItems.Exists(x => x == herbas)) {
			P1ItemIcon.iconColor = Color.white;
			P1ItemIcon.itemSprite = null;
			P1ItemCountDown.started = false;
			StaticOptions.isFlying = false;
			P1ItemCountDown.itemText = "No item";
			GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
			isTriggered = false;
			StaticOptions.p1SpawnItems.Remove (herbas);
			partEffect.transform.parent = null;
			Destroy(partEffect.gameObject, 3);
			Destroy (herbas);
		}
	}

	void OnCollisionEnter(Collision col) {
		
		if (col.gameObject.tag == "Player") {
			if (P1ItemCountDown.itemText != "No item") {
				P1ItemIcon.iconColor = Color.white;
				P1ItemIcon.itemSprite = null;
				P1ItemCountDown.started = false;
				StaticOptions.isFlying = false;
				P1ItemCountDown.itemText = "No item";
				StaticOptions.p1SpawnItems.Remove (GameObject.FindGameObjectWithTag ("p1TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag ("p1TakenItem"));
			}
<<<<<<< HEAD
			flyingCountDown = flyingDuration;
=======
			flyingTimeCountDown = flyingDuration;
>>>>>>> PO3
			herbas.tag = "p1TakenItem";
			isTriggered = true;
			if (!Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Flying boots\n" + "Press L2 to use";
			} else if (Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Flying boots\n" + "Press Tab to use";
			}
			P1ItemIcon.itemSprite = herbasSprite;
			herbas.GetComponent<SphereCollider> ().enabled = false;
			meshRenderer = herbas.GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer mesh in meshRenderer) {
				mesh.enabled = false;
			}
		}
	}
}