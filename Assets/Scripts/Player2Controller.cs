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

    private float horizontalAxisPlayer2;
    private float verticalAxisPlayer2;
    private bool isAxisHorizontalInUse;
    private bool isAxisVerticalInUse;


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

        isAxisHorizontalInUse = false;
        isAxisVerticalInUse = false;

    }
	
	void Update () {
        
        horizontalAxisPlayer2 = Input.GetAxisRaw("HorizontalJoyPlayer2");
        verticalAxisPlayer2 = Input.GetAxisRaw("VerticalJoyPlayer2");

        bool canFire = activeBlock == null;// && cooldownTimeCounter <= cooldownTime;

       

        if(canFire)
        {
			if (verticalAxisPlayer2 == -1) ActivateBlockAndResetCooldown(northBlock);
            else if (verticalAxisPlayer2 == 1) ActivateBlockAndResetCooldown(southBlock);
            else if (horizontalAxisPlayer2 == -1) ActivateBlockAndResetCooldown(westBlock);
            else if (horizontalAxisPlayer2 == 1) ActivateBlockAndResetCooldown(eastBlock);
        }
        else
        {
           // cooldownTimeCounter += Time.deltaTime;
        }

		cooldownSlider.value = cooldownTimeCounter;

        bool canTurn = !canFire;

        if(canTurn)
        {
            if(isVertical(activeBlock))
            {
                if (isAxisHorizontalInUse == false)
                {
                    if (horizontalAxisPlayer2 == -1)
                    {
                        activeBlock.GoToYourLeft();
                        isAxisHorizontalInUse = true;
                    }
                    else if (horizontalAxisPlayer2 == 1)
                    {
                        activeBlock.GoToYourRight();
                        isAxisHorizontalInUse = true;
                    }
                }
            } else
            {
                if (isAxisVerticalInUse == false)
                {
                    if (verticalAxisPlayer2 == 1)
                    {
                        if (activeBlock == westBlock)
                            activeBlock.GoToYourRight();
                        else
                            activeBlock.GoToYourLeft();

                        isAxisVerticalInUse = true;
                    }
                    else if (verticalAxisPlayer2 == -1)
                    {
                        if (activeBlock == westBlock)
                            activeBlock.GoToYourLeft();
                        else
                            activeBlock.GoToYourRight();

                        isAxisVerticalInUse = true;
                    }
                }
            }

            if (horizontalAxisPlayer2 == 0)
                isAxisHorizontalInUse = false;

            if (verticalAxisPlayer2 == 0)
                isAxisVerticalInUse = false;

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Joystick2Button5))
            {
                activeBlock.TurnNinetyDegrees();
            }

        }

    }

    bool isVertical(AttackBlockController attackBlock)
    {
        return attackBlock == northBlock || attackBlock == southBlock;
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
        if (activeBlock == null) return;

        SetAttackBlockColor(Color.white, activeBlock);
        this.activeBlock = null;
    }

    void SetAttackBlockColor(Color color, AttackBlockController activeBlock)
    {
        var childrenMaterials = activeBlock.gameObject.GetComponentsInChildren<Renderer>();

        foreach (var item in childrenMaterials)
        {
            item.material.color = color;
        }
    }

}
