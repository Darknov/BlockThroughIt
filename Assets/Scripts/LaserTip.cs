using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTip : MonoBehaviour {

	Ray laserRay;
	ParticleSystem laserParticles;
	LineRenderer laserLine;
	Light laserLight;

	void Awake() {
		
		laserParticles = GetComponent<ParticleSystem> ();
		laserLine = GetComponent<LineRenderer> ();
		laserLight = GetComponent<Light> ();
	}

	void Update() {

		if (Laser.isActivated) {
			Shoot ();

		}
		if(!Laser.isActivated) {

			DisableEffects ();
		}
	}

	void DisableEffects() {

		laserLine.enabled = false;
		laserLight.enabled = false;

	}

	void Shoot() {

		laserLight.enabled = true;
		laserLine.enabled = true;
		laserLine.SetPosition (0, transform.position);
		laserRay.origin = transform.position;
		laserRay.direction = transform.forward;
		laserLine.SetPosition (1, laserRay.origin + laserRay.direction * 20f);

		RaycastHit[] hitInfos;
		Vector3 origin = transform.position + Vector3.down;
		Vector3 direction = transform.position + transform.rotation * Vector3.forward * 1000f;
		hitInfos = Physics.RaycastAll (origin, direction, 1000f);
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

	}

}
