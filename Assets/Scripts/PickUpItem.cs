using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

	private bool isTriggered = false;
    public GameObject shoesControllerPrefab;
	private Component[] meshRenderer;

	void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponentInChildren<BoostItemContainer>().BoostItem = Instantiate(shoesControllerPrefab, col.gameObject.transform).GetComponent<BoostItem>();
            col.gameObject.GetComponentInChildren<BoostItemContainer>().BoostItem.player = col.gameObject;
            FindObjectOfType<AudioManager>().Play("GettingItem"); //////////
            GameObject.Destroy(this.gameObject);
        }
	}
}


