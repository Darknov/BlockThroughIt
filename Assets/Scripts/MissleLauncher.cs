using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLauncher : MonoBehaviour {

    public Rigidbody missle;

	void Update () {
        if (Input.GetMouseButtonDown(0) && GameObject.FindGameObjectWithTag("active") && (GameObject.FindObjectsOfType<HomingMissle>().Length==0 ))
        {
            Instantiate(missle, GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Player").transform.rotation);
        }
    }
}
