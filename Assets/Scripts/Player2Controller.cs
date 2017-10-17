using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player2Controller : MonoBehaviour {

	public AttackBlockController northBlock, southBlock, eastBlock, westBlock;
    public float blocksSpeed;
    public float cooldownTime;

    public Slider cooldownSlider;

	private AttackBlockController activeBlock;

    private float cooldownTimeCounter;
	private float jumpLength;


    void Start () {
		if(northBlock == null || southBlock == null || eastBlock == null || westBlock == null)
            throw new NullReferenceException("You must assign all blocks to " +this.GetType().Name);
        
        if(blocksSpeed == 0)
            throw new ArgumentException("blocksSpeed cannot be 0");

        if (cooldownSlider == null)
            throw new NullReferenceException("You must assign a slider to " + this.GetType().Name);

		cooldownTimeCounter = cooldownTime;

		jumpLength = 1;

		if (jumpLength == 0)
			throw new ArgumentException("jumpLength cannot be 0");

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

		cooldownSlider.minValue = 0;
		cooldownSlider.maxValue = cooldownTime;

    }
	
	void Update () {

		bool canFire = activeBlock == null && cooldownTimeCounter <= cooldownTime;

        if(canFire)
        {
			if (Input.GetKeyDown(KeyCode.Alpha1)) ActivateBlockAndResetCooldown(northBlock);
            if (Input.GetKeyDown(KeyCode.Alpha2)) ActivateBlockAndResetCooldown(southBlock);
            if (Input.GetKeyDown(KeyCode.Alpha3)) ActivateBlockAndResetCooldown(westBlock);
            if (Input.GetKeyDown(KeyCode.Alpha4)) ActivateBlockAndResetCooldown(eastBlock);
        }
        else
        {
            cooldownTimeCounter += Time.deltaTime;
        }

		cooldownSlider.value = cooldownTimeCounter;

    }

    void ActivateBlockAndResetCooldown(AttackBlockController block)
    {
        block.SetActivationState(true);

		this.activeBlock = block;
        SetAttackBlockColor(Color.red, activeBlock);
        block.PlatformHit += OnActiveBlockPlatformHit;

        cooldownTimeCounter = 0;
    }

    void OnActiveBlockPlatformHit(object source, EventArgs args)
    {
        SetAttackBlockColor(Color.white, activeBlock);
        this.activeBlock = null;
    }

    void SetAttackBlockColor(Color color, AttackBlockController activeBlock)
    {
        var childrenMaterials = activeBlock.gameObject.GetComponentsInChildren<Renderer>();

        if (activeBlock == null) return;

        foreach (var item in childrenMaterials)
        {
            item.material.color = color;
        }
    }

}
