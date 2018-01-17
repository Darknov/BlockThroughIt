using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2HintController : MonoBehaviour {

    private bool isItemImageVisible = false;
    private bool wasAnyTimeItemActivated = false;
    public bool wasAnyKKNWTriggered = false;

    public GameObject imageController;

	// Update is called once per frame
	void Update () {

        isItemImageVisible = imageController.GetComponent<P2ItemIcon>().isIconVisible;
        wasAnyTimeItemActivated = P2ItemCountDown.started;

        if ((isItemImageVisible && !wasAnyTimeItemActivated))
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if(!isItemImageVisible || P2ItemCountDown.started == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
