using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KKNW : MonoBehaviour {

	public GameObject kknw;
	public Sprite kknwSprite;
	private bool isTriggered = false;

	void Update() {
		
		if (isTriggered) {
			if (Player2Controller.p2GamePad) {
				if ((Input.GetKeyDown ("joystick 2 button 6") || Input.GetKeyDown("joystick 2 button 8")) && Player2Controller.isDestroyBlockAvailable) {                 
					Player2Controller.isDestroyBlockAvailable = false;
					Player2Controller.isDestroyBlockActivated = true;
					P2ItemIcon.itemSprite = null;
					P2ItemCountDown.itemText = "No item";
					isTriggered = false;
					StaticOptions.p2SpawnItems.Remove (kknw);
					Destroy (kknw);
				}
			} else if (!Player2Controller.p2GamePad) {
				if (Input.GetKeyDown (KeyCode.Alpha9) && Player2Controller.isDestroyBlockAvailable) {
					Player2Controller.isDestroyBlockAvailable = false;
					Player2Controller.isDestroyBlockActivated = true;
					P2ItemIcon.itemSprite = null;
					P2ItemCountDown.itemText = "No item";
					isTriggered = false;
					StaticOptions.p2SpawnItems.Remove (kknw);
					Destroy (kknw);
				}
			}
		}
	}

	/*void LateUpdate() {
		if (!StaticOptions.p2SpawnItems.Exists (x => x == kknw)) {
			Destroy (kknw);
		}
	}*/

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			StaticOptions.p2SpawnItems.Remove (kknw);
			Destroy (kknw);
		}
	}

	void OnTriggerEnter(Collider col) {
		
		if (col.gameObject.tag == "block") {

            var audioManager = FindObjectOfType<AudioManager>();
            if(audioManager != null) audioManager.Play("godGetItem");

            if (P2ItemCountDown.itemText != "No item") {
                GameObject.FindGameObjectWithTag("evilTimeBar").SendMessage("SubTime", 0f);
                P2ItemIcon.iconColor = Color.white;
                Player1Controller.inverseControlUsed = false;
                Player1Controller.inverseControl = false;
                Player1Controller.IsPlayerStoppedUsed = false;
                Player1Controller.IsPlayerStopped = false;
                Player2Controller.isDestroyBlockAvailable = false;
				Player2Controller.isDestroyBlockActivated = false;
				P2ItemIcon.itemSprite = null;
				P2ItemCountDown.itemText = "No item";
				isTriggered = false;
				StaticOptions.p2SpawnItems.Remove (GameObject.FindGameObjectWithTag("p2TakenItem"));
				Destroy (GameObject.FindGameObjectWithTag("p2TakenItem"));
			}
			kknw.tag = "p2TakenItem";
			isTriggered = true;
			if (Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Destroy Blocks";
			} else if (!Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Destroy Blocks";
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