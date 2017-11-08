using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyCube : MonoBehaviour {

	public Material[] material;
	Renderer rend;

	void Start ()
	{
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
		rend.sharedMaterial = material [0];
	}

	void Update ()
	{
		//MakeItShiny ();
		// MakeItShiny2 (); 
	}

	void MakeItShiny()
	{
		if (AutoMovement.shinyCubePosition == transform.position) {
			rend.sharedMaterial = material [1];
		} else {
			rend.sharedMaterial = material [0];
		}
	}

	void MakeItShiny2()
	{
		if (AutoMovement.shinyCubePosition2 == transform.position) {
			rend.sharedMaterial = material [1];
		} else {
			rend.sharedMaterial = material [0];
		}
	}
}
