using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour {

    public float amplitude;
    private float counter = 0;
    private Vector3 initialScale;

    private void Start()
    {
        this.initialScale = this.transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate () {
        this.gameObject.transform.localScale = Vector3.one * Mathf.Sin(counter) * amplitude + initialScale;
        counter += 0.2f;
	}
}
