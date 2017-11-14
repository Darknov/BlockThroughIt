using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Item : MonoBehaviour {

    public Text items;

    // Use this for initialization
    void Start()
    {
        items = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        items.text = "Movement switch: " + (int)Player2Controller.movementSwitchCounter;

    }
}
