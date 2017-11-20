using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerbasFlying : MonoBehaviour {

	public GameObject herbas;
	public float flyingDuration = 5f;
	private bool isTriggered = false;
	private Component[] meshRenderer;
	Text text;

	void Update() {

		if (isTriggered) {
			ItemCountDown.itemTimeRemaining = flyingDuration;
			flyingDuration -= Time.deltaTime;
			if ((int)flyingDuration <= 0) {
				ItemCountDown.started = false;
				Player1Controller.herbasFlying = false;
				GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
				isTriggered = false;
				Destroy (herbas);
			}
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
			isTriggered = true;
			Player1Controller.herbasFlying = true;
			GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = false;
			ItemCountDown.started = true;
			ItemCountDown.itemText = "Herbas Boots\n" + "Time Remaining: ";
			herbas.GetComponent<SphereCollider> ().enabled = false;
			meshRenderer = herbas.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer mesh in meshRenderer)
				mesh.enabled = false;
		}
	}
}
