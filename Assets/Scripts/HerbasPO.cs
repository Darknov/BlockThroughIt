﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerbasPO : MonoBehaviour {

	public GameObject herbas;
	public Sprite herbasSprite;
	public float flyingDuration = 5f;
	private bool isTriggered = false;
	private Component[] meshRenderer;

	void Update() {

		if (isTriggered) {
			P1ItemCountDown.itemTimeRemaining = flyingDuration;
			flyingDuration -= Time.deltaTime;
			if ((int)flyingDuration <= 0) {
				P1ItemIcon.itemSprite = null;
				P1ItemCountDown.started = false;
				GameObject.FindGameObjectWithTag("Player").GetComponent<Player1Controller>().isFlying = false;
				P1ItemCountDown.itemText = "No item";
				GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = true;
				isTriggered = false;
				Destroy (herbas);
			}
		}
	}

	void OnCollisionEnter(Collision col) {

		if (col.gameObject.tag == "Player") {
			isTriggered = true;
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player1Controller>().isFlying = true;
			GameObject.FindWithTag ("Player").GetComponent<Rigidbody> ().useGravity = false;
			P1ItemCountDown.started = true;
			P1ItemCountDown.itemText = "Herbas Flying Boots\n" + "Time Remaining: ";
			P1ItemIcon.itemSprite = herbasSprite;
			herbas.GetComponent<SphereCollider> ().enabled = false;
			meshRenderer = herbas.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer mesh in meshRenderer)
				mesh.enabled = false;
		}
	}
}