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
                    || Input.GetKeyDown("joystick 1 button 5") || Input.GetKeyDown("joystick 1 button 7"))
                {
                    FindObjectOfType<AudioManager>().Play("herbasSound");
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

					flyingTimeCountDown -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", flyingTimeCountDown);

                    if ((int)flyingTimeCountDown <= 0) {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
						isTriggered = false;
						StaticOptions.p1SpawnItems.Remove (herbas);
						partEffect.transform.parent = null;
<<<<<<< Updated upstream
						//Destroy (partEffect.gameObject, 3);
					}
				}
=======
						Destroy (partEffect.gameObject, 3);
                        GameObject.FindWithTag("Player").transform.Find("herbasOH").gameObject.SetActive(false);

                    }
                }
>>>>>>> Stashed changes
			} else if (Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
                    FindObjectOfType<AudioManager>().Play("herbasSound");
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
					P1ItemCountDown.itemTimeRemaining = flyingTimeCountDown;
					flyingTimeCountDown -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", flyingTimeCountDown);

                    if ((int)flyingTimeCountDown <= 0) {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
						isTriggered = false;
						StaticOptions.p1SpawnItems.Remove (herbas);
						partEffect.transform.parent = null;
<<<<<<< Updated upstream
						//Destroy(partEffect.gameObject, 3);
					}
				}
=======
						Destroy(partEffect.gameObject, 3);
                        GameObject.FindWithTag("Player").transform.Find("herbasOH").gameObject.SetActive(false);

                    }
                }
>>>>>>> Stashed changes
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
			partEffect.transform.parent = null;
			///Destroy(partEffect.gameObject, 3);
			Destroy (herbas);
        }
    }

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
<<<<<<< Updated upstream
			if (P1ItemCountDown.itemText != "No item") {
                P1ItemIcon.iconColor = Color.white;
=======
            GameObject.FindWithTag("Player").transform.Find("laserOH").gameObject.SetActive(false);
            GameObject.FindWithTag("Player").transform.Find("herbasOH").gameObject.SetActive(true);
            GameObject.FindWithTag("Player").transform.Find("blockOH").gameObject.SetActive(false);
            if (P1ItemCountDown.itemText != "No item") {
				P1ItemIcon.iconColor = Color.white;
>>>>>>> Stashed changes
				P1ItemIcon.itemSprite = null;
				P1ItemCountDown.started = false;
				StaticOptions.isFlying = false;
				P1ItemCountDown.itemText = "No item";
                GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", 0f);

                GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
				StaticOptions.p1SpawnItems.Remove (GameObject.FindGameObjectWithTag ("p1TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag ("p1TakenItem"));
			}
            flyingTimeCountDown = flyingDuration;
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