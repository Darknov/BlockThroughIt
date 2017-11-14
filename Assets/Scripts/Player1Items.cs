using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Items : MonoBehaviour {

    Text items;

    // Use this for initialization
    void Start () {
        items = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        items.text = "Homming missle: " + (int)Player1Controller.hommingMissleCounter;

    }
}
