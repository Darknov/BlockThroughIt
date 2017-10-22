using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Float : MonoBehaviour {

    float x = 0;
    float floatingSpeed = 0.05f;
    float amplitude = 0.1f;

	void Update () {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,  amplitude * Mathf.Sin(x) , gameObject.transform.position.z);
        x += floatingSpeed;
	}
}
