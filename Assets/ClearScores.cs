using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScores : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ClearScore()
    {
        StaticOptions.godswin = 0;
        StaticOptions.rabbitswin = 0;
    }
}
