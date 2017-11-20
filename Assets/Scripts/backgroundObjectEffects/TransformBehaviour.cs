using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBehaviour : MonoBehaviour {

    // Use this for initialization

    public float expandScaleDivider = 120.0f;
    public bool isExpanding = true;

    public float changingHeightDivider = 300.0f;
    public bool isChangingHeight = true;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isExpanding)
            this.transform.localScale += new Vector3( Mathf.Sin(Time.timeSinceLevelLoad) / expandScaleDivider, Mathf.Cos(Time.timeSinceLevelLoad) / expandScaleDivider, 0);
        if(isChangingHeight)
            this.transform.position += new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad)/ changingHeightDivider, 0);
	}
}
