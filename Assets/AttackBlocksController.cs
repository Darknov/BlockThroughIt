using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlocksController : MonoBehaviour {

	public AttackBlock northBlock, southBlock, eastBlock, westBlock;

    public float blocksSpeed;

	void Start () {
		if(northBlock == null || southBlock == null || eastBlock == null || westBlock == null)
        {
            throw new System.NullReferenceException("You must assign all blocks to AttackBlocksController.");
        }

        // Setting movespeed doesn't work properly

        northBlock.MoveSpeed = blocksSpeed;
        southBlock.MoveSpeed = blocksSpeed;
        eastBlock.MoveSpeed = blocksSpeed;
        westBlock.MoveSpeed = blocksSpeed;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.W)) northBlock.Activate();
		if (Input.GetKeyDown(KeyCode.S)) southBlock.Activate();
		if (Input.GetKeyDown(KeyCode.A)) westBlock.Activate();
        if (Input.GetKeyDown(KeyCode.D)) eastBlock.Activate();
	}

}
