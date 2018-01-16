using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Herbas : MonoBehaviour {

	public GameObject herbas;
	public Sprite herbasSprite;
	public float flyingDuration = 5f;
	private float flyingTimeCountDown;
	private bool isTriggered = false;
	public GameObject partEffect;
	private Component[] meshRenderer;

	void Update() {

		isDestroyed ();

		if (isTriggered) {
			if (!Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown ("joystick 1 button 6") || Input.GetKeyDown("joystick 1 button 8") 
					|| Input.GetKeyDown("joystick 1 button 5") || Input.GetKeyDown("joystick 1 button 7")) {
					P1ItemIcon.iconColor = Color.cyan;
					StaticOptions.isFlying = true;
					GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = false;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Flying Boots";
					P1ItemCountDown.itemTimeRemaining = flyingTimeCountDown;
					if (StaticOptions.specialEffects) {
						FindObjectOfType<AudioManager>().Play("herbasSound");
						Instantiate(partEffect, 
							new Vector3(GameObject.FindWithTag ("Player").GetComponent<Transform>().position.x, 
								GameObject.FindWithTag ("Player").GetComponent<Transform>().position.y, 
								GameObject.FindWithTag ("Player").GetComponent<Transform>().position.z
							), 
							GameObject.FindWithTag ("Player").GetComponent<Transform>().rotation,  
							GameObject.FindWithTag ("Player").GetComponent<Transform>().transform
						);
						StaticOptions.specialEffects = false;
					}
				}
				if (StaticOptions.isFlying) {
					flyingTimeCountDown -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("Timebar").GetComponent<TimeBar>().maxhitpoint = flyingDuration;
                    GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", flyingTimeCountDown);
					P1ItemCountDown.itemTimeRemaining = flyingTimeCountDown;
					if (flyingTimeCountDown <= 0) {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
						isTriggered = false;
						StaticOptions.p1SpawnItems.Remove (herbas);
						partEffect.transform.parent = null;
						Destroy (partEffect);
                        GameObject.FindWithTag("Player").transform.Find("herbasOH").gameObject.SetActive(false);

                    }
                }
			} else if (Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
                    P1ItemIcon.iconColor = Color.cyan;
					StaticOptions.isFlying = true;
					GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = false;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Flying Boots";
					P1ItemCountDown.itemTimeRemaining = flyingTimeCountDown;
					if (StaticOptions.specialEffects) {
						if(FindObjectOfType<AudioManager>()!=null) FindObjectOfType<AudioManager>().Play("herbasSound");
						Instantiate(partEffect, 
							new Vector3(GameObject.FindWithTag ("Player").GetComponent<Transform>().position.x, 
								GameObject.FindWithTag ("Player").GetComponent<Transform>().position.y, 
								GameObject.FindWithTag ("Player").GetComponent<Transform>().position.z
							), 
							GameObject.FindWithTag ("Player").GetComponent<Transform>().rotation,  
							GameObject.FindWithTag ("Player").GetComponent<Transform>().transform
						);
						StaticOptions.specialEffects = false;
					}
				}
				if (StaticOptions.isFlying) {
					flyingTimeCountDown -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("Timebar").GetComponent<TimeBar>().maxhitpoint = flyingDuration;
                    GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", flyingTimeCountDown);
					P1ItemCountDown.itemTimeRemaining = flyingTimeCountDown;
                    if (flyingTimeCountDown <= 0) {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
						isTriggered = false;
						StaticOptions.p1SpawnItems.Remove (herbas);
						partEffect.transform.parent = null;
						DestroyImmediate (partEffect);
                        GameObject.FindWithTag("Player").transform.Find("herbasOH").gameObject.SetActive(false);

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
            GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", 0f);

            P1ItemIcon.iconColor = Color.white;
			P1ItemIcon.itemSprite = null;
			P1ItemCountDown.started = false;
			StaticOptions.isFlying = false;
			P1ItemCountDown.itemText = "No item";
			GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
			isTriggered = false;
			StaticOptions.p1SpawnItems.Remove (herbas);
			if (GameObject.FindWithTag ("p1particle") != null) {
				GameObject.FindWithTag ("p1particle").transform.parent = null;
				Destroy (GameObject.FindWithTag ("p1particle"));
			}
			Destroy (herbas);
        }
    }

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
            GameObject.FindWithTag("Player").transform.Find("laserOH").gameObject.SetActive(false);
            GameObject.FindWithTag("Player").transform.Find("herbasOH").gameObject.SetActive(true);
            GameObject.FindWithTag("Player").transform.Find("blockOH").gameObject.SetActive(false);
            if (P1ItemCountDown.itemText != "No item") {
				Laser.isActivated = false;
				P1ItemIcon.iconColor = Color.white;
				P1ItemIcon.itemSprite = null;
				P1ItemCountDown.started = false;
				StaticOptions.isFlying = false;
				P1ItemCountDown.itemText = "No item";
                GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", 0f);
                GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
				StaticOptions.p1SpawnItems.Remove (GameObject.FindGameObjectWithTag ("p1TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag ("p1TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag ("p1particle"));
			}
			StaticOptions.specialEffects = true;
            flyingTimeCountDown = flyingDuration;
			herbas.tag = "p1TakenItem";
			isTriggered = true;
			if (!Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Flying boots";
			} else if (Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Flying boots";
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