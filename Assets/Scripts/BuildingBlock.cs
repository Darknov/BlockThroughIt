using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour {

	public GameObject builder;
	public Sprite builderSprite;
	private bool isTriggered = false;
	private Component[] meshRenderer;
	private bool wasUsed = false;
	public GameObject partEffect;
	private float lastTy = 0;
	private PlatformBoard platform;
	int rowLength;


    void Start() {
		this.platform = GameObject.FindGameObjectWithTag("platformBoard").GetComponent<PlatformBoard>();
		this.rowLength = this.platform.rowLength;
	}

	void Update() {
		isDestroyed ();

		int x, z;
		float tY;

		if (isTriggered) {
			this.builder.transform.SetPositionAndRotation (GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ().position, GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ().rotation);
			if (!Player1Controller.p1KeyBoard) { //dodałam tu uruchamianie joystickiem, nie bylo wczesniej, a chyba dzialalo :O
				if (Input.GetKeyDown("joystick 1 button 5") || Input.GetKeyDown("joystick 1 button 7")
                    || Input.GetKeyDown("joystick 1 button 4") || Input.GetKeyDown("joystick 1 button 6"))
                {
                    if (StaticOptions.specialEffects)
                    {
                        if (FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().Play("AbilityPutBlock");
                        StaticOptions.specialEffects = false;
                    }
                    x = Convert.ToInt32 (GameObject.FindGameObjectWithTag ("Player").transform.position.x + (this.rowLength - 1) / 2);
					z = Convert.ToInt32 (GameObject.FindGameObjectWithTag ("Player").transform.position.z + (this.rowLength - 1) / 2);
					tY = builder.transform.rotation.eulerAngles.y;
					if (tY == 90) {
						Instantiate (partEffect, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x + 1, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.y, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.z), 
							GameObject.FindGameObjectWithTag ("Player").transform.rotation, 
							GameObject.FindGameObjectWithTag ("Player").transform);
						platform.DestroyShadow ();
						platform.addBlock (x + 1, z);
					}
					if (tY == 270) {
						Instantiate (partEffect, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x - 1, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.y, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.z), 
							GameObject.FindGameObjectWithTag ("Player").transform.rotation, 
							GameObject.FindGameObjectWithTag ("Player").transform);
						platform.DestroyShadow ();
						platform.addBlock (x - 1, z);
					}
					if (tY == 0) {
						Instantiate (partEffect, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.y, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.z + 1), 
							GameObject.FindGameObjectWithTag ("Player").transform.rotation, 
							GameObject.FindGameObjectWithTag ("Player").transform);
						platform.DestroyShadow ();
						platform.addBlock (x, z + 1);
					}
					if (tY == 180) {
						Instantiate (partEffect, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.y, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.z - 1), 
							GameObject.FindGameObjectWithTag ("Player").transform.rotation, 
							GameObject.FindGameObjectWithTag ("Player").transform);
						platform.DestroyShadow ();
						platform.addBlock (x, z - 1);
					}
					wasUsed = true;
					isTriggered = false;
					P1ItemIcon.itemSprite = null;
					P1ItemCountDown.itemText = "No item";
					isTriggered = false;
					StaticOptions.p1SpawnItems.Remove (GameObject.FindGameObjectWithTag ("p1TakenItem"));
                 //   Destroy(GameObject.FindGameObjectWithTag("p1particle"));
                }
                else if (!wasUsed) {
					tY = builder.GetComponent<Transform> ().rotation.eulerAngles.y;
					x = Convert.ToInt32 (GameObject.FindGameObjectWithTag ("Player").transform.position.x + (this.rowLength - 1) / 2);
					z = Convert.ToInt32 (GameObject.FindGameObjectWithTag ("Player").transform.position.z + (this.rowLength - 1) / 2);
					if (tY == 90) {
						platform.AddShadowBlock (x + 1, z);
					}
					if (tY == 270) {
						platform.AddShadowBlock (x - 1, z);
					}
					if (tY == 0) {
						platform.AddShadowBlock (x, z + 1);
					}
					if (tY == 180) {
						platform.AddShadowBlock (x, z - 1);
					}
					if (tY != lastTy) {
						lastTy = tY;
						platform.DestroyShadow ();
					}
				}
			} else if (Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
                    if (StaticOptions.specialEffects)
                    {
                        if (FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().Play("AbilityPutBlock");
                        StaticOptions.specialEffects = false;
                    }
                    x = Convert.ToInt32 (GameObject.FindGameObjectWithTag ("Player").transform.position.x + (this.rowLength - 1) / 2);
					z = Convert.ToInt32 (GameObject.FindGameObjectWithTag ("Player").transform.position.z + (this.rowLength - 1) / 2);
					tY = builder.transform.rotation.eulerAngles.y;
					if (tY == 90) {
						Instantiate (partEffect, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x + 1, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.y, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.z), 
							GameObject.FindGameObjectWithTag ("Player").transform.rotation, 
							GameObject.FindGameObjectWithTag ("Player").transform);
						platform.DestroyShadow ();
						platform.addBlock (x + 1, z);
					}
					if (tY == 270) {
						Instantiate (partEffect, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x - 1, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.y, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.z), 
							GameObject.FindGameObjectWithTag ("Player").transform.rotation, 
							GameObject.FindGameObjectWithTag ("Player").transform);
						platform.DestroyShadow ();
						platform.addBlock (x - 1, z);
					}
					if (tY == 0) {
						Instantiate (partEffect, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.y, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.z + 1), 
							GameObject.FindGameObjectWithTag ("Player").transform.rotation, 
							GameObject.FindGameObjectWithTag ("Player").transform);
						platform.DestroyShadow ();
						platform.addBlock (x, z + 1);
					}
					if (tY == 180) {
						Instantiate (partEffect, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.y, 
							GameObject.FindGameObjectWithTag ("Player").transform.position.z - 1), 
							GameObject.FindGameObjectWithTag ("Player").transform.rotation, 
							GameObject.FindGameObjectWithTag ("Player").transform);
						platform.DestroyShadow ();
						platform.addBlock (x, z - 1);
					}
					wasUsed = true;
					isTriggered = false;
					P1ItemIcon.itemSprite = null;
					P1ItemCountDown.itemText = "No item";
					isTriggered = false;
					StaticOptions.p1SpawnItems.Remove (GameObject.FindGameObjectWithTag ("p1TakenItem"));
                   // Destroy(GameObject.FindGameObjectWithTag("p1particle"));
                }
                else if (!wasUsed) {
					tY = builder.GetComponent<Transform> ().rotation.eulerAngles.y;
					x = Convert.ToInt32 (GameObject.FindGameObjectWithTag ("Player").transform.position.x + (this.rowLength - 1) / 2);
					z = Convert.ToInt32 (GameObject.FindGameObjectWithTag ("Player").transform.position.z + (this.rowLength - 1) / 2);
					if (tY == 90) {
						platform.AddShadowBlock (x + 1, z);
					}
					if (tY == 270) {
						platform.AddShadowBlock (x - 1, z);
					}
					if (tY == 0) {
						platform.AddShadowBlock (x, z + 1);
					}
					if (tY == 180) {
						platform.AddShadowBlock (x, z - 1);
					}
					if (tY != lastTy) {
						lastTy = tY;
						platform.DestroyShadow ();
					}
				}
			}
		}
	}

	void LateUpdate() {
        if (!StaticOptions.p1SpawnItems.Exists (x => x == builder)) {
			//gameObject.GetComponent<Rigidbody> ().useGravity = true;
			Destroy (builder);
		}
	}

	void isDestroyed() {

		if(!StaticOptions.p1SpawnItems.Exists(x => x == builder)) {
            GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", 0f);
			Laser.isActivated = false;
			P1ItemIcon.iconColor = Color.white;
			P1ItemIcon.itemSprite = null;
			P1ItemCountDown.itemText = "No item";
			isTriggered = false;
			StaticOptions.p1SpawnItems.Remove (builder);
			if (GameObject.FindWithTag ("p1particle") != null) {
				GameObject.FindWithTag ("p1particle").transform.parent = null;
				Destroy (GameObject.FindWithTag ("p1particle"));
			}
			Destroy (builder);
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
            if (P1ItemCountDown.itemText != "No item")
            {
                GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", 0f);
                P1ItemIcon.iconColor = Color.white;
				P1ItemIcon.itemSprite = null;
				P1ItemCountDown.started = false;
				StaticOptions.isFlying = false;
				Laser.isActivated = false;
				P1ItemCountDown.itemText = "No item";
				GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
				StaticOptions.p1SpawnItems.Remove (GameObject.FindGameObjectWithTag ("p1TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag ("p1TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag ("p1particle"));
			}
            StaticOptions.specialEffects = true;
            builder.tag = "p1TakenItem";
			isTriggered = true;
			if (!Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Build 1 block";
			} else if (Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Build 1 block";
			}
			P1ItemIcon.itemSprite = builderSprite;
			builder.GetComponent<SphereCollider> ().enabled = false;
			meshRenderer = builder.GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer mesh in meshRenderer) {
				mesh.enabled = false;
			}
		}
	}
}