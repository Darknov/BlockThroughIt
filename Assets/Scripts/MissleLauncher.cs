using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLauncher : MonoBehaviour {

    public Rigidbody missle;

	void Update () {
        if (Input.GetMouseButtonDown(0) && (GameObject.FindObjectsOfType<HomingMissle>().Length==0 ) && GameObject.FindObjectOfType<Player2Controller>().GetActiveBlock() != null)
        {           
            Instantiate(missle, GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Player").transform.rotation);
        }
    }
}
