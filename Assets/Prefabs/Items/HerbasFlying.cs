using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbasFlying : MonoBehaviour {
	
	public GameObject herbas;

	void OnCollisionEnter(Collision col) {
		
		if (col.gameObject.tag == "Player") {
			AutoMovement.herbasFlying = true;
			AutoMovement.trueTime = Time.time;
			Destroy (herbas);
		}
	}
}
