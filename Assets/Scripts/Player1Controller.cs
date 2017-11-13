using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {

    public LaserController laserActivator;
    public GameAccelerator gameAccelerator;
    public Vector3 goalPosition;
    public Animator animator;
    public float animationSpeedMultiplier = 1;

    private bool movingUp, movingDown, movingLeft, movingRight;
    public float moveSpeed;
    private float jumpTime;
    private float timeCounter;
    private bool onAir = false; 
    private bool outOfPlatform = false;

    public List<RaycastHit> hits;

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

        Vector3 boxCenter = Vector3.zero + Vector3.down*0.2f;
        Vector3 boxSize = Vector3.one*0.1f;

        hits = new List<RaycastHit>(Physics.BoxCastAll(this.transform.position + boxCenter, boxSize,dir));

        bool atLeastOneOut = false;

        foreach (var item in hits)
        {
            if(item.collider.gameObject.CompareTag("platform"))
            {
                atLeastOneOut = true;
            }
        }

        outOfPlatform = !atLeastOneOut;

    }

    void CheckControls()
    {
        if(laserActivator.isActivated) return;

        float horizontalAxis = Input.GetAxisRaw("HorizontalJoy");
        float verticalAxis = Input.GetAxisRaw("VerticalJoy");

        if(Input.anyKeyDown)
        {
            movingUp = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || verticalAxis == 1;
            movingDown = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || verticalAxis == -1;
            movingLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || horizontalAxis == -1;
            movingRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || horizontalAxis == 1;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            laserActivator.ActivateLaser();
        }


    }

    void UpdateMovement()
    {

        if(onAir) gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goalPosition, Time.deltaTime * moveSpeed);

        if(outOfPlatform) return;

        if(timeCounter > 1.3f*jumpTime)
        {
            MoveAndResetTimer();        
        } 


        timeCounter += Time.deltaTime;

    }

    void MoveAndResetTimer() {
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
        jumpTime = 1.0f/moveSpeed;
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
