using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MissleLauncher : MonoBehaviour {

    public Rigidbody missle;
    
    void Update () {
        if (Input.GetMouseButtonDown(0) && (GameObject.FindObjectsOfType<HomingMissle>().Length==0 ) && GameObject.FindObjectOfType<Player2Controller>().GetActiveBlock() != null && Player1Controller.hommingMissleCounter>0)
        {
            Player1Controller.hommingMissleCounter--;
            Instantiate(missle, GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Player").transform.rotation);
        }
    }
}
