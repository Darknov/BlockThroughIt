using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour {

	Light light;

	int i=0;
	// Use this for initialization
	void Start () {
		light = gameObject.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (i % 10 == 0) {

			var random = new System.Random ();


			Color color = new Color ();
			color.r = (float)random.NextDouble () * 255;
			color.b = (float)random.NextDouble () * 255;
			color.g = (float)random.NextDouble () * 255;

			light.color = color;
		}

		i++;


	}
}
