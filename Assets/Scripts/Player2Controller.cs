using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player2Controller : MonoBehaviour
{

    public float blocksSpeed;
	public float boostSpeed;
    private float jumpLength = 1;

    public RandomAttackBlockGenerator randomBlockGenerator;
    private AttackBlock activeBlock;
    public Transform northSpawn, southSpawn, eastSpawn, westSpawn;

    private AttackBlock northBlock, southBlock, eastBlock, westBlock;
    public PlatformBoard platformBoard;


    private float horizontalAxisPlayer2;
    private float verticalAxisPlayer2;
    private bool isAxisHorizontalInUse;
    private bool isAxisVerticalInUse;

    void Start()
    {

        if (northSpawn == null || southSpawn == null || eastSpawn == null || null == westSpawn)
            throw new NullReferenceException("You must assign all spawn points to " + this.GetType().Name);

        if (blocksSpeed == 0)
            throw new ArgumentException("blocksSpeed cannot be 0");

        CreateInitialAttackBlocks();

        isAxisHorizontalInUse = false;
        isAxisVerticalInUse = false;
    }

    void Update()
    {

        horizontalAxisPlayer2 = Input.GetAxisRaw("HorizontalJoyPlayer2");
        verticalAxisPlayer2 = Input.GetAxisRaw("VerticalJoyPlayer2");

        Debug.Log("Horizonal: " + horizontalAxisPlayer2);
        Debug.Log("Vertical:" + verticalAxisPlayer2);

        bool canFire = activeBlock == null;// && cooldownTimeCounter <= cooldownTime;

        if (canFire)
        {
            if (Input.GetKeyDown("joystick 2 button 0") || Input.GetKeyDown("i"))
            {
                ActivateBlock(northBlock);
                northBlock = null;
            }
            else if (Input.GetKeyDown("joystick 2 button 2") || Input.GetKeyDown("k"))
            {
                ActivateBlock(southBlock);
                southBlock = null;
            }
            else if (Input.GetKeyDown("joystick 2 button 3") || Input.GetKeyDown("j"))
            {
                ActivateBlock(westBlock);
                westBlock = null;
            }
            else if (Input.GetKeyDown("joystick 2 button 1") || Input.GetKeyDown("l"))
            {
                ActivateBlock(eastBlock);
                eastBlock = null;
            }
        }

        bool canTurn = !canFire;

        if (canTurn)
        {
			if (Input.GetKey ("joystick 2 button 4") || Input.GetKey (KeyCode.Q)) {
				activeBlock.ChangeSpeed (boostSpeed);
			} else {
				activeBlock.MoveSpeed = blocksSpeed;
			}

            if (IsVertical(activeBlock))
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
            }
            else
            {
                if (isAxisVerticalInUse == false)
                {
                    if (verticalAxisPlayer2 == -1)
                    {
                        if (activeBlock == westBlock)
                            activeBlock.GoToYourRight();
                        else
                            activeBlock.GoToYourLeft();

                        isAxisVerticalInUse = true;
                    }
                    else if (verticalAxisPlayer2 == 1)
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
                activeBlock.TurnNinetyDegreesAndUpdateTriggers();
            }

        }

    }

    bool IsVertical(AttackBlock attackBlock)
    {
        if (northBlock == null || southBlock == null) return true;
        else return false;
    }

    void ActivateBlock(AttackBlock block)
    {
        block.SetActivationState(true);
        this.activeBlock = block;
        this.activeBlock.tag = "active";

        SetAttackBlockColor(Color.red, activeBlock);
        block.PlatformHit += OnActiveBlockPlatformHit;
        block.DestroyAttackBlock += RespawnEmptyBlocks;
    }

    void OnActiveBlockPlatformHit(object source, EventArgs args)
    {
        if (activeBlock == null) return;
        SetAttackBlockColor(Color.white, activeBlock);
        platformBoard.addBlock(activeBlock);

        RespawnEmptyBlocks(source, args);

    }

    void RespawnEmptyBlocks(object source, EventArgs args)
    {
        if (activeBlock == null) return;

        if (activeBlock.MoveDirection.Equals(Vector3.back))
        {
            northBlock = randomBlockGenerator.createRandomBlock(activeBlock.transform.parent, blocksSpeed, jumpLength, activeBlock.MoveDirection);
        }
        else if (activeBlock.MoveDirection == Vector3.forward) southBlock = randomBlockGenerator.createRandomBlock(activeBlock.transform.parent, blocksSpeed, jumpLength, activeBlock.MoveDirection);
        else if (activeBlock.MoveDirection == Vector3.left) eastBlock = randomBlockGenerator.createRandomBlock(activeBlock.transform.parent, blocksSpeed, jumpLength, activeBlock.MoveDirection);
        else if (activeBlock.MoveDirection == Vector3.right) westBlock = randomBlockGenerator.createRandomBlock(activeBlock.transform.parent, blocksSpeed, jumpLength, activeBlock.MoveDirection);

        this.activeBlock.transform.parent = null;
        this.activeBlock = null;
    }

    void SetAttackBlockColor(Color color, AttackBlock activeBlock)
    {
        var childrenMaterials = activeBlock.gameObject.GetComponentsInChildren<Renderer>();

        foreach (var item in childrenMaterials)
        {
            item.material.color = color;
        }
    }

    void CreateInitialAttackBlocks()
    {
        northBlock = randomBlockGenerator.createRandomBlock(northSpawn, blocksSpeed, jumpLength, Vector3.back);
        southBlock = randomBlockGenerator.createRandomBlock(southSpawn, blocksSpeed, jumpLength, Vector3.forward);
        eastBlock = randomBlockGenerator.createRandomBlock(eastSpawn, blocksSpeed, jumpLength, Vector3.left);
        westBlock = randomBlockGenerator.createRandomBlock(westSpawn, blocksSpeed, jumpLength, Vector3.right);
    }
}
