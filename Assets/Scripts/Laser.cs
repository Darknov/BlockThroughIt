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
	static bool isActivated = false;
	public float duration;
	private float timeCounter;
	private Vector3 shootDirection;

	void Update() {

		isDestroyed ();

		if (isTriggered) {
			laser.transform.SetPositionAndRotation (GameObject.FindGameObjectWithTag ("Player").transform.position, GameObject.FindGameObjectWithTag ("Player").transform.rotation);
			if (!Player1Controller.p1KeyBoard) {
                
                if (Input.GetKeyDown ("joystick 1 button 6") || Input.GetKeyDown("joystick 1 button 8")
                    || Input.GetKeyDown("joystick 1 button 5") || Input.GetKeyDown("joystick 1 button 7")) {
                    FindObjectOfType<AudioManager>().Play("AbilityLaser");
                    P1ItemIcon.iconColor = Color.green;
					isActivated = true;
					timeCounter = duration;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Destroying Beam\n" + "Time Remaining: ";
				}
				P1ItemCountDown.itemTimeRemaining = timeCounter;
				if (isActivated) {
					timeCounter -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", timeCounter);

                    if ((int)timeCounter >= 0) {
						if (laser.transform.localScale.z < laserRange) {
							laser.transform.localScale = new Vector3 (
								laser.transform.localScale.x, 
								laser.transform.localScale.y, 
								laser.transform.localScale.z + 1f * laserGrowingSpeed);
						}
						RaycastHit[] hitInfos;
						Vector3 pos = GameObject.FindWithTag ("Player").transform.position + Vector3.down;
						Vector3 dir = laser.transform.position + laser.transform.rotation * Vector3.forward * 1000f;
						hitInfos = Physics.RaycastAll (pos, dir, 1000f);
						Dictionary<int, int> childrenRegister = new Dictionary<int, int> ();
						foreach (var hitInfo in hitInfos) {
							if (hitInfo.collider.gameObject.CompareTag ("block")) {
								var parent = hitInfo.collider.transform.parent;
								int parentId = parent.GetInstanceID ();
								if (childrenRegister.ContainsKey (parentId)) {
									childrenRegister [parentId] = childrenRegister [parentId] - 1;
								} else {
									childrenRegister.Add (parentId, parent.childCount - 1);
								}
								if (childrenRegister [parentId] == 0) {
									Destroy (parent.gameObject);
								}
								Destroy (hitInfo.collider.gameObject);
							}
						}
					} else {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						P1ItemCountDown.itemText = "No item";
						isTriggered = false;
						isActivated = false;
						StaticOptions.p1SpawnItems.Remove (laser);
						DeactivateLaser ();
						Destroy (laser);
					}
				}
			} else if (Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
                    if(FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().Play("AbilityLaser");

                    P1ItemIcon.iconColor = Color.green;
					isActivated = true;
					timeCounter = duration;
					P1ItemCountDown.started = true;
					P1ItemCountDown.itemText = "Destroying Beam\n" + "Time Remaining: ";
				}
				P1ItemCountDown.itemTimeRemaining = timeCounter;
				if (isActivated) {
					timeCounter -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", timeCounter);

                    if ((int)timeCounter >= 0) {
						if (laser.transform.localScale.z < laserRange) {
							laser.transform.localScale = new Vector3 (
								laser.transform.localScale.x, 
								laser.transform.localScale.y, 
								laser.transform.localScale.z + 1f * laserGrowingSpeed);
						}
						RaycastHit[] hitInfos;
						Vector3 pos = GameObject.FindWithTag ("Player").transform.position + Vector3.down;
						Vector3 dir = laser.transform.position + laser.transform.rotation * Vector3.forward * 1000f;
						hitInfos = Physics.RaycastAll (pos, dir, 1000f);
						Dictionary<int, int> childrenRegister = new Dictionary<int, int> ();
						foreach (var hitInfo in hitInfos) {
							if (hitInfo.collider.gameObject.CompareTag ("block")) {
								var parent = hitInfo.collider.transform.parent;
								int parentId = parent.GetInstanceID ();
								if (childrenRegister.ContainsKey (parentId)) {
									childrenRegister [parentId] = childrenRegister [parentId] - 1;
								} else {
									childrenRegister.Add (parentId, parent.childCount - 1);
								}
								if (childrenRegister [parentId] == 0) {
									Destroy (parent.gameObject);
								}
								Destroy (hitInfo.collider.gameObject);
							}
						}
					} else {
						P1ItemIcon.iconColor = Color.white;
						P1ItemIcon.itemSprite = null;
						P1ItemCountDown.started = false;
						P1ItemCountDown.itemText = "No item";
						isTriggered = false;
						isActivated = false;
						StaticOptions.p1SpawnItems.Remove (laser);
						DeactivateLaser ();
						Destroy (laser);
					}
				}
			}
		}
	}

	private void DeactivateLaser() {
		isActivated = false;
		laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, 0);
		timeCounter = duration;
	}

	void LateUpdate() {
		if (!StaticOptions.p1SpawnItems.Exists (x => x == laser)) {
			Destroy (laser);
		}
	}

	void isDestroyed() {

		if(!StaticOptions.p1SpawnItems.Exists(x => x == laser)) {
            GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", 0f);

            P1ItemIcon.iconColor = Color.white;
			P1ItemIcon.itemSprite = null;
			P1ItemCountDown.started = false;
			P1ItemCountDown.itemText = "No item";
			isTriggered = false;
			StaticOptions.p1SpawnItems.Remove (laser);
			Destroy (laser);
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
			if (P1ItemCountDown.itemText != "No item") {
                GameObject.FindGameObjectWithTag("Timebar").SendMessage("SubTime", 0f);

                P1ItemIcon.iconColor = Color.white;
				P1ItemIcon.itemSprite = null;
				P1ItemCountDown.started = false;
				StaticOptions.isFlying = false;
				P1ItemCountDown.itemText = "No item";
				GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
				StaticOptions.p1SpawnItems.Remove (GameObject.FindGameObjectWithTag ("p1TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag ("p1TakenItem"));
			}
           
            laser.tag = "p1TakenItem";
			isTriggered = true;
			if (!Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Destroying Beam\n" + "Press L2 to use";
			} else if (Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Destroying Beam\n" + "Press Tab to use";
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