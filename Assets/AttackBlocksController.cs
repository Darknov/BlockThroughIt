using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlocksController : MonoBehaviour {

	public AttackBlock northBlock, southBlock, eastBlock, westBlock;
    public float blocksSpeed;
    public float jumpLength;

	void Start () {
		if(northBlock == null || southBlock == null || eastBlock == null || westBlock == null)
            throw new NullReferenceException("You must assign all blocks to AttackBlocksController.");
        
        if(blocksSpeed == 0)
            throw new ArgumentException("blocksSpeed cannot be 0");

        if (jumpLength == 0)
            throw new ArgumentException("jumpLength cannot be 0");

        // Setting movespeed doesn't work properly

        northBlock.MoveSpeed = blocksSpeed;
        southBlock.MoveSpeed = blocksSpeed;
        eastBlock.MoveSpeed = blocksSpeed;
        westBlock.MoveSpeed = blocksSpeed;

        northBlock.JumpLength = jumpLength;
        southBlock.JumpLength = jumpLength;
        eastBlock.JumpLength = jumpLength;
        westBlock.JumpLength = jumpLength;

        northBlock.MoveDirection = Vector3.back;
        southBlock.MoveDirection = Vector3.forward;
        eastBlock.MoveDirection = Vector3.left;
        westBlock.MoveDirection = Vector3.right;

        northBlock.Initialize();
        southBlock.Initialize();
        eastBlock.Initialize();
        westBlock.Initialize();


    }
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.W)) northBlock.Activate();
		if (Input.GetKeyDown(KeyCode.S)) southBlock.Activate();
		if (Input.GetKeyDown(KeyCode.A)) westBlock.Activate();
        if (Input.GetKeyDown(KeyCode.D)) eastBlock.Activate();
	}

}
