using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KKNW : MonoBehaviour {

	public GameObject kknw;
	public Sprite kknwSprite;
	private bool isTriggered = false;
	private Component[] meshRenderer;

	void Update() {

		if (isTriggered) {
			Player2Controller.isDestroyBlockAvailable = true;
			if (Player2Controller.p2GamePad) {
				if (Input.GetKeyDown ("joystick 2 button 6") && Player2Controller.isDestroyBlockAvailable) {
					Player2Controller.isDestroyBlockAvailable = false;
					Player2Controller.isDestroyBlockActivated = true;
					P2ItemIcon.itemSprite = null;
					P2ItemCountDown.itemText = "No item";
					isTriggered = false;
					Destroy (kknw);
				}
			} else if (!Player2Controller.p2GamePad) {
				if (Input.GetKeyDown (KeyCode.Alpha9) && Player2Controller.isDestroyBlockAvailable)
				{
					Player2Controller.isDestroyBlockAvailable = false;
					Player2Controller.isDestroyBlockActivated = true;
					P2ItemIcon.itemSprite = null;
					P2ItemCountDown.itemText = "No item";
					isTriggered = false;
					Destroy (kknw);
				}
			}
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "p2item") {
			Destroy (col.gameObject);
		}
		if (col.gameObject.tag == "Player") {
			if (P2ItemCountDown.itemText != "No item") {
				Destroy (GameObject.FindGameObjectWithTag("p2TakenItem"));
			}
			kknw.tag = "p2TakenItem";
			isTriggered = true;
			if (Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Block Destroyer\n" + "Press X to use";
			} else if (!Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Block Destroyer\n" + "Press 9 to use";
			}
			P2ItemIcon.itemSprite = kknwSprite;
			kknw.GetComponent<SphereCollider> ().enabled = false;
			meshRenderer = kknw.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer mesh in meshRenderer)
				mesh.enabled = false;
		}
	}
}