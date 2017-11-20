using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBehaviour : MonoBehaviour {

    // Use this for initialization
    public bool isExpanding = true;
    public bool isChangingHeight = true;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isExpanding)
            this.transform.localScale += new Vector3( Mathf.Sin(Time.timeSinceLevelLoad) / 120.0f, Mathf.Cos(Time.timeSinceLevelLoad) / 120.0f, 0);
        if(isChangingHeight)
            this.transform.position += new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad)/ 300.0f, 0);
	}
}
