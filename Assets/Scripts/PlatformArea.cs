using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformArea : MonoBehaviour {

    public PlatformBoard platformBoard;

    private void Start()
    {
        gameObject.transform.localScale = new Vector3((float) platformBoard.rowLength, (float) platformBoard.rowLength, 1f);
    }

}
