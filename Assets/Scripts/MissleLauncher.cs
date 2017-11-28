using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MissleLauncher : BoostItem {

    public Rigidbody missle;

    private bool isActive = false;

    public override void ActivateItem()
    {
        isActive = true;
    }

    void Update () {
        if (isActive && (GameObject.FindObjectsOfType<HomingMissle>().Length==0 ) && GameObject.FindObjectOfType<Player2Controller>().GetActiveBlock() != null && Player1Controller.hommingMissleCounter>0)
        {
            Player1Controller.hommingMissleCounter--;
            Instantiate(missle, GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Player").transform.rotation);
            isActive = false;
        }
    }
}
