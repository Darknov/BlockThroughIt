using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{

    public static bool p1KeyBoard = false;
    //public bool isFlying = false;

	public static bool inverseControl = false;
	public static bool inverseControlUsed = false;

    public GameAccelerator gameAccelerator;
    public Vector3 goalPosition;  
    public Animator animator;
    public float animationSpeedMultiplier = 1;
    public static bool IsPlayerStopped = false;
    public static bool IsPlayerStoppedUsed = false;
    
    //public BoostItemContainer boostItemContainer;

    private bool movingUp, movingDown, movingLeft, movingRight;
    public float moveSpeed;
    private float jumpTime;
    private float timeCounter;
    private bool onAir = false;
    private bool outOfPlatform = false;

    public List<RaycastHit> hits;

    public static bool isAutomovementOn = false;
    private bool makeMove = true;
    private float jump;
    public static int hommingMissleCounter = 3;

    private bool isHorizontalAxisInUse = false;
    private bool isVerticalAxisInUse = false;

	void Start() {

	}

    void Update()
    {
        LoadSpeedFromGameAcc();
        CheckControls();
        UpdateMovement();
    }

    void FixedUpdate()
    {
        // move those vars up!

        Vector3 dir = transform.TransformDirection(Vector3.down);

        Vector3 pos = Vector3.down * 0.2f;
        pos += gameObject.transform.position;

        Vector3 boxCenter = Vector3.zero + Vector3.down * 0.2f;
        Vector3 boxSize = Vector3.one * 0.1f;

        hits = new List<RaycastHit>(Physics.BoxCastAll(this.transform.position + boxCenter, boxSize, dir));

        bool atLeastOneOut = false;

        foreach (var item in hits)
        {
            if (item.collider.gameObject.CompareTag("platform"))
            {
                atLeastOneOut = true;
            }
        }

        outOfPlatform = !atLeastOneOut;
    }

    void CheckControls()
    {
        if (IsPlayerStopped) return;

        if (p1KeyBoard)
        {
			if (!inverseControl) {
				
				if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W) ||
				   Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S) ||
				   Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A) ||
				   Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {

					movingUp = Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W);
					movingDown = Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S);
					movingLeft = Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A);
					movingRight = Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D);
					makeMove = true;



                    jump = jumpTime;
					CountDown.started = true;
				}

			} else {

				if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W) ||
					Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S) ||
					Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A) ||
					Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) {

					movingUp = Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S);
					movingDown = Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W);
					movingLeft = Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D);
					movingRight = Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A);
					makeMove = true;
					jump = jumpTime;
					CountDown.started = true;
				}
			}

           /* if (Input.GetKeyDown(KeyCode.Tab))
            {
                boostItemContainer.ActivateItem();  
            }*/
        }


        if (!p1KeyBoard)
        {

            float horizontalAxis = Input.GetAxisRaw("HorizontalJoy");
            float verticalAxis = Input.GetAxisRaw("VerticalJoy");

			if (!inverseControl) {
			    if (horizontalAxis != 0)
			    {
			        if (isHorizontalAxisInUse == false)
			        {
			            movingUp = false;
			            movingDown = false;
			            movingRight = horizontalAxis == 1;
			            movingLeft = horizontalAxis == -1;
			            makeMove = true;
			            jump = jumpTime;
			            isHorizontalAxisInUse = true;
			        }
			    }
			    else if (verticalAxis != 0)
			    {
			        if (isVerticalAxisInUse == false)
			        {
			            movingUp = verticalAxis == 1;
			            movingDown = verticalAxis == -1;
			            movingLeft = false;
			            movingRight = false;
			            makeMove = true;
			            jump = jumpTime;
                        isVerticalAxisInUse = true;
			        }
			    }

			    if (horizontalAxis == 0)
			        isHorizontalAxisInUse = false;
			    if (verticalAxis == 0)
			        isVerticalAxisInUse = false;

			} else {

			    if (horizontalAxis != 0)
			    {
			        if (isHorizontalAxisInUse == false)
			        {
			            movingUp = false;
			            movingDown = false;
			            movingRight = horizontalAxis == -1;
			            movingLeft = horizontalAxis == 1;
			            makeMove = true;
			            jump = jumpTime;
			            isHorizontalAxisInUse = true;
			        }
			    }
			    else if (verticalAxis != 0)
			    {
			        if (isVerticalAxisInUse == false)
			        {
			            movingUp = verticalAxis == -1;
			            movingDown = verticalAxis == 1;
			            movingLeft = false;
			            movingRight = false;
			            makeMove = true;
			            jump = jumpTime;
			            isVerticalAxisInUse = true;
			        }
			    }

			    if (horizontalAxis == 0)
			        isHorizontalAxisInUse = false;
			    if (verticalAxis == 0)
			        isVerticalAxisInUse = false;
            }

			if (verticalAxis != 0 || horizontalAxis != 0) {
				CountDown.started = true;
			}

			/*if (Input.GetKeyDown("joystick 1 button 5") || Input.GetKeyDown("joystick 1 button 7")
                || Input.GetKeyDown("joystick 1 button 6") || Input.GetKeyDown("joystick 1 button 8"))
			{
				boostItemContainer.ActivateItem();  
			}*/
        }

    }

    void UpdateMovement()
    {
        if (onAir) gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goalPosition, Time.deltaTime * moveSpeed);

		//if (IsPlayerStopped) return;
		if (!StaticOptions.isFlying && outOfPlatform) return;

        if (timeCounter > 1.3f * jumpTime)
        {
            MoveAndResetTimer();

            if (!isAutomovementOn)
            {
                movingDown = false;
                movingUp = false;
                movingLeft = false;
                movingRight = false;
            }
        }
        timeCounter += Time.deltaTime;
    }

    void MoveAndResetTimer()
    {
        if (movingUp)
        {
            MoveUp();
            timeCounter = 0;
            onAir = true;
        }
        else if (movingLeft)
        {
            MoveLeft();
            timeCounter = 0;
            onAir = true;
        }
        else if (movingDown)
        {
            MoveDown();
            timeCounter = 0;
            onAir = true;

        }
        else if (movingRight)
        {
            MoveRight();
            timeCounter = 0;
            onAir = true;

        }
    }

    void Move(Vector3 Direction)
    {
        goalPosition = gameObject.transform.position + Direction;
        animator.Play("rabbit_move");
    }

    private void LoadSpeedFromGameAcc()
    {
        moveSpeed = gameAccelerator.player1Speed;
        jumpTime = 1.0f / moveSpeed;
        animator.SetFloat("jumpAnimationSpeed", animationSpeedMultiplier * moveSpeed);
    }

    #region MoveMethods

    void MoveUp()
    {
        Move(Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 1f);
    }

    void MoveLeft()
    {
        Move(Vector3.left);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 1f);
    }

    void MoveDown()
    {
        Move(Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 1f);
    }

    void MoveRight()
    {
        Move(Vector3.right);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 1f);
    }

    #endregion

}
