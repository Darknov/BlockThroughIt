using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {

    public GameObject block;

	// Use this for initialization
	void Start () {
        float widthOfABlock = 1;

        Instantiate(block, this.transform);

        for (int i=0; i<2; i++)
        {
            GameObject newBlock = block;
        }




    }
	
	// Update is called once per frame
	void Update () {
    }
}
