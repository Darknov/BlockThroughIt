using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2HintController : MonoBehaviour {

    private bool isItemImageVisible = false;
    private bool wasItemActivated = false;

    public GameObject imageController;
    private GameObject kknwController;

	// Update is called once per frame
	void Update () {

        isItemImageVisible = imageController.GetComponent<P2ItemIcon>().isIconVisible;
        wasItemActivated = P2ItemCountDown.started;
		
        if(isItemImageVisible && !wasItemActivated)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
