using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public Texture[] textures;


    private int textureNumber;
    private int previousNumber;
    // Use this for initialization
    void Start () {
        textureNumber = 0;
        previousNumber = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown)
        {
            if (textureNumber < textures.Length)
            {
                textureNumber++;
            }
            else
            {
                // tutaj dodac zmiane sceny na menuScene
            }
            

        }
        if(textureNumber != previousNumber)
        {
            previousNumber = textureNumber;
            GetComponent<Renderer>().material.mainTexture = textures[textureNumber];
        }
	}
}
