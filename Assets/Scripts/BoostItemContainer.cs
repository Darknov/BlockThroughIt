using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItemContainer : MonoBehaviour {

	public BoostItem boostItem;

	public BoostItem BoostItem
	{
		set { boostItem = value;}
	}

	public void ActivateItem()
	{
		if(boostItem == null) return;
		boostItem.ActivateItem();
		boostItem.player = this.transform.parent.gameObject;
        this.boostItem = null;
	}
	


}
	
