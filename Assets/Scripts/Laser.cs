using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour {

	public GameObject laser;
	public Sprite laserSprite;
	private bool isTriggered = false;
	private Component[] meshRenderer;
	public LineRenderer lineRenderer;
	public float laserGrowingSpeed;
	public float laserRange;
	public bool isActivated = false;
	public float duration;
	private float timeCounter = 0;
	private Vector3 shootDirection;

	void Update() {

		if (isTriggered) {
			if (!Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown ("joystick 2 button 6")) {
					GameObject.FindWithTag ("Player").GetComponent<Player1Controller>().IsPlayerStopped = isActivated;
					P1ItemIcon.iconColor = Color.green;
					isActivated = true;
				}
				if (isActivated) {
					if (timeCounter < duration) {
						if (GameObject.FindWithTag ("Player").transform.localScale.z < laserRange) {
							GameObject.FindWithTag ("Player").transform.localScale = new Vector3 (
								GameObject.FindWithTag ("Player").transform.localScale.x, 
								GameObject.FindWithTag ("Player").transform.localScale.y, 
								GameObject.FindWithTag ("Player").transform.localScale.z + 1f * laserGrowingSpeed);
						}
						RaycastHit[] hitInfos;
						Vector3 pos = GameObject.FindWithTag ("Player").transform.position + Vector3.down;
						Vector3 dir = this.transform.position + this.transform.rotation * Vector3.forward * 1000f;
						hitInfos = Physics.RaycastAll (pos, dir, 1000f);
						Debug.DrawLine (pos, dir, Color.cyan, 1000f);
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
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						isTriggered = false;
						isActivated = false;
						StaticOptions.p1SpawnItems.Remove (laser);
						DeactivateLaser ();
						Destroy (laser);
					}
					timeCounter += Time.deltaTime;
				}
			} else if (Player1Controller.p1KeyBoard) {
				if (Input.GetKeyDown (KeyCode.Tab)) {
					GameObject.FindWithTag ("Player").GetComponent<Player1Controller>().IsPlayerStopped = isActivated;
					P1ItemIcon.iconColor = Color.green;
					isActivated = true;
				}
				if (isActivated) {
					if (timeCounter < duration) {
						if (GameObject.FindWithTag ("Player").transform.localScale.z < laserRange) {
							GameObject.FindWithTag ("Player").transform.localScale = new Vector3 (
								GameObject.FindWithTag ("Player").transform.localScale.x, 
								GameObject.FindWithTag ("Player").transform.localScale.y, 
								GameObject.FindWithTag ("Player").transform.localScale.z + 1f * laserGrowingSpeed);
						}
						RaycastHit[] hitInfos;
						Vector3 pos = GameObject.FindWithTag ("Player").transform.position + Vector3.down;
						Vector3 dir = this.transform.position + this.transform.rotation * Vector3.forward * 1000f;
						hitInfos = Physics.RaycastAll (pos, dir, 1000f);
						Debug.DrawLine (pos, dir, Color.cyan, 1000f);
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
						StaticOptions.isFlying = false;
						P1ItemCountDown.itemText = "No item";
						isTriggered = false;
						isActivated = false;
						StaticOptions.p1SpawnItems.Remove (laser);
						DeactivateLaser ();
						Destroy (laser);
					}
					timeCounter += Time.deltaTime;
				}
			}
		}
	}

	private void DeactivateLaser() {
		isActivated = false;
		gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, 0);
		timeCounter = 0;
	}

	void LateUpdate() {
		if (!StaticOptions.p1SpawnItems.Exists (x => x == laser)) {
			Destroy (laser);
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
			if (P1ItemCountDown.itemText != "No item") {
				Destroy (GameObject.FindGameObjectWithTag ("p1TakenItem"));
			}
			laser.tag = "p1TakenItem";
			isTriggered = true;
			if (!Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Laser\n" + "Press L2 to use";
			} else if (Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Laser\n" + "Press Tab to use";
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