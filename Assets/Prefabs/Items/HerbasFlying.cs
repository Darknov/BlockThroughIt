using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbasFlying : MonoBehaviour {
	
	public GameObject herbas;

	void OnCollisionEnter(Collision col) {
		
		if (col.gameObject.tag == "Player") {
			Destroy (herbas);
			AutoMovement.herbasFlaying = true;
		}
	}
}
