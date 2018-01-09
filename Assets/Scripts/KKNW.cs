using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KKNW : MonoBehaviour {

	public GameObject kknw;
	public Sprite kknwSprite;
	private bool isTriggered = false;

	void Update() {

		isDestroyed ();
		
		if (isTriggered) {
			if (Player2Controller.p2GamePad) {
				if (Input.GetKeyDown ("joystick 2 button 6") && Player2Controller.isDestroyBlockAvailable) {                 
					Player2Controller.isDestroyBlockAvailable = false;
					Player2Controller.isDestroyBlockActivated = true;
					P2ItemIcon.itemSprite = null;
					P2ItemCountDown.itemText = "No item";
					isTriggered = false;
					StaticOptions.p2SpawnItems.Remove (kknw);
				}
			} else if (!Player2Controller.p2GamePad) {
				if (Input.GetKeyDown (KeyCode.Alpha9) && Player2Controller.isDestroyBlockAvailable) {
					Player2Controller.isDestroyBlockAvailable = false;
					Player2Controller.isDestroyBlockActivated = true;
					P2ItemIcon.itemSprite = null;
					P2ItemCountDown.itemText = "No item";
					isTriggered = false;
					StaticOptions.p2SpawnItems.Remove (kknw);
				}
			}
		}
	}

	void LateUpdate() {
		if (!StaticOptions.p2SpawnItems.Exists (x => x.transform.position.y == kknw.transform.position.y)) {
			Destroy (kknw);
		}
	}

	void isDestroyed() {

		if(!StaticOptions.p1SpawnItems.Exists(x => x == kknw)) {
			Player2Controller.isDestroyBlockAvailable = false;
			Player2Controller.isDestroyBlockActivated = false;
			P2ItemIcon.itemSprite = null;
			P2ItemCountDown.itemText = "No item";
			isTriggered = false;
			StaticOptions.p2SpawnItems.Remove (kknw);
			Destroy (kknw);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			StaticOptions.p2SpawnItems.Remove (kknw);
			Destroy (kknw);
		}
	}

	void OnTriggerEnter(Collider col) {
		
<<<<<<< HEAD
		if (col.gameObject.tag == "block") {
            FindObjectOfType<AudioManager>().Play("godGetItem");
			StaticOptions.p2SpawnItems.Remove (kknw);
=======
		if (col.gameObject.tag == "block") {
>>>>>>> PO3
			if (P2ItemCountDown.itemText != "No item") {
				Destroy (GameObject.FindGameObjectWithTag("p2TakenItem"));
			}
			kknw.tag = "p2TakenItem";
			isTriggered = true;
			if (Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Block Destroyer\n" + "Press L2 to use";
			} else if (!Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Block Destroyer\n" + "Press 9 to use";
			}
			Player2Controller.isDestroyBlockAvailable = true;
			P2ItemIcon.itemSprite = kknwSprite;
			kknw.GetComponent<SphereCollider> ().enabled = false;
			for (int i = 0; i < kknw.transform.childCount; i++) {
				kknw.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
}