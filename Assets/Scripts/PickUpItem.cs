using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

	private bool isTriggered = false;
    public GameObject shoesControllerPrefab;
	private Component[] meshRenderer;

	void OnTriggerEnter(Collider col) {
            Debug.LogError("TRIGGERED!");

        if (col.gameObject.tag == "Player") {

            Debug.LogError("TRIGGERED if!");

            col.gameObject.GetComponentInChildren<BoostItemContainer>().boostItem = Instantiate(shoesControllerPrefab, col.gameObject.transform).GetComponent<BoostItem>();
            col.gameObject.GetComponentInChildren<BoostItemContainer>().boostItem.player = col.gameObject;

            GameObject.Destroy(this.gameObject);
        }
	}
}


