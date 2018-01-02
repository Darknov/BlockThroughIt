using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItemContainer : MonoBehaviour {

	private BoostItem boostItem;

	public BoostItem BoostItem
	{
		set
        {
            if(this.boostItem != null) Destroy(this.boostItem.gameObject);
            boostItem = value;
        }
        get
        {
            return boostItem;
        }
	}

	public void ActivateItem()
	{
		if(boostItem == null) return;
		boostItem.ActivateItem();
		boostItem.player = this.transform.parent.gameObject;
        this.boostItem = null;
	}
	


}
	
