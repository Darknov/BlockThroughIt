using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoostItem : MonoBehaviour {

	public Sprite image;
	public bool wasUsed = false;
	public GameObject player;
	public abstract void ActivateItem();


}
