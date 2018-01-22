using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour {

	public GameObject laser;
	public Sprite laserSprite;
	private bool isTriggered = false;
	private Component[] meshRenderer;
	public float laserGrowingSpeed;
	public float laserRange;
	public static bool isActivated = false;
	public float duration = 2f;
	private float timeCounter;
	private Vector3 shootDirection;
	public GameObject partEffect;

	void Update() {

		isDestroyed ();

		if (isTriggered) {
			if (!Player1Controller.p1KeyBoard) {     
                if (Input.GetKeyDown("joystick 1 button 5") || Input.GetKeyDown("joystick 1 button 7")
                    || Input.GetKeyDown("joystick 1 button 4") || Input.GetKeyDown("joystick 1 button 6"))
                {
                    P1ItemIcon.iconColor = Color.green;
					isActivated = true;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Destroying Beam";
					P1ItemCountDown.itemTimeRemaining = timeCounter;
					if (StaticOptions.specialEffects) {
						FindObjectOfType<AudioManager>().Play("AbilityLaser");
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

				if (isActivated) {
					timeCounter -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("Timebar").GetComponent<TimeBar>().maxhitpoint = duration;

                    GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", timeCounter);
					P1ItemCountDown.itemTimeRemaining = timeCounter;
					if(timeCounter <= 0) {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						isActivated = false;
						P1ItemCountDown.itemText = "No item";
						isTriggered = false;
						StaticOptions.p1SpawnItems.Remove (laser);
						GameObject.FindWithTag ("p1particle").transform.parent = null;
						Destroy (GameObject.FindWithTag ("p1particle"));
                    }
                }
			} else if (Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
                    P1ItemIcon.iconColor = Color.green;
					isActivated = true;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Destroying Beam";
					P1ItemCountDown.itemTimeRemaining = timeCounter;
					if (StaticOptions.specialEffects) {
						if(FindObjectOfType<AudioManager>()!=null) FindObjectOfType<AudioManager>().Play("AbilityLaser");
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
				if (isActivated) {
					timeCounter -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("Timebar").GetComponent<TimeBar>().maxhitpoint = duration;
                    GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", timeCounter);
					P1ItemCountDown.itemTimeRemaining = timeCounter;
                    if (timeCounter <= 0) {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						P1ItemCountDown.itemText = "No item";
						isTriggered = false;
						isActivated = false;
						StaticOptions.p1SpawnItems.Remove (laser);
						GameObject.FindWithTag ("p1particle").transform.parent = null;
						Destroy (GameObject.FindWithTag ("p1particle"));
                    }
                }
			}
		}
	}

    void LateUpdate() {
		if (!StaticOptions.p1SpawnItems.Exists (x => x == laser)) {
			gameObject.GetComponent<Rigidbody> ().useGravity = true;
			//Destroy (laser);
		}
	}

	void isDestroyed() {

		if(!StaticOptions.p1SpawnItems.Exists(x => x == laser)) {
            GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", 0f);

            P1ItemIcon.iconColor = Color.white;
			P1ItemIcon.itemSprite = null;
			P1ItemCountDown.started = false;
			isActivated = false;
			P1ItemCountDown.itemText = "No item";
			isTriggered = false;
			StaticOptions.p1SpawnItems.Remove (laser);
			if (GameObject.FindWithTag ("p1particle") != null) {
				GameObject.FindWithTag ("p1particle").transform.parent = null;
				Destroy (GameObject.FindWithTag ("p1particle"));
			}
			Destroy (laser);
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
            if (P1ItemCountDown.itemText != "No item") {
                GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", 0f);
				isActivated = false;
                P1ItemIcon.iconColor = Color.white;
				P1ItemIcon.itemSprite = null;
				P1ItemCountDown.started = false;
				StaticOptions.isFlying = false;
				P1ItemCountDown.itemText = "No item";
				GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
				StaticOptions.p1SpawnItems.Remove (GameObject.FindGameObjectWithTag ("p1TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag ("p1TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag ("p1particle"));
			}
			StaticOptions.specialEffects = true;
			timeCounter = duration;
            laser.tag = "p1TakenItem";
			isTriggered = true;
			if (!Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Destroying Beam";
			} else if (Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Destroying Beam";
			}
			P1ItemIcon.itemSprite = laserSprite;
			laser.GetComponent<SphereCollider> ().enabled = false;
			meshRenderer = laser.GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer mesh in meshRenderer) {
				mesh.enabled = false;
			}
		}
	}
}