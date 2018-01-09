using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missle : MonoBehaviour {

	public GameObject missle;
	public Sprite missleSprite;
	private bool isTriggered = false;
	public GameObject partEffect;
	private Component[] meshRenderer;

	void Update() {
	}

	void LateUpdate() {
		if (!StaticOptions.p1SpawnItems.Exists (x => x == missle)) {
			Destroy (missle);
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
			if (P1ItemCountDown.itemText != "No item") {
				Destroy (GameObject.FindGameObjectWithTag ("p1TakenItem"));
			}
			missle.tag = "p1TakenItem";
			isTriggered = true;
			if (!Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Homing Missle\n" + "Press L2 to use";
			} else if (Player1Controller.p1KeyBoard) {
				P1ItemCountDown.itemText = "Homing Missle\n" + "Press Tab to use";
			}
			P1ItemIcon.itemSprite = missleSprite;
			missle.GetComponent<SphereCollider> ().enabled = false;
			meshRenderer = missle.GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer mesh in meshRenderer) {
				mesh.enabled = false;
			}
		}
	}
}