using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour {

    public Vector3 goalPosition;
    public Animator animator;
    public float animationSpeedMultiplier = 1;

    private bool movingUp, movingDown, movingLeft, movingRight;
    public float moveSpeed;
    private float jumpTime;
    private float timeCounter;
    private bool onAir = false; 
    private bool idle;
    private bool canMove = true;

    void Start() {
        idle = true;
        jumpTime = 1.0f/moveSpeed;

        animator.SetFloat("jumpAnimationSpeed", animationSpeedMultiplier * moveSpeed);
    }

    void Update()
    {
        CheckControls();
        UpdateMovement();
    }

    void CheckControls()
    {
        float horizontalAxis = Input.GetAxisRaw("HorizontalJoy");
        float verticalAxis = Input.GetAxisRaw("VerticalJoy");

        movingUp = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || verticalAxis == 1;
        movingDown = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || verticalAxis == -1;
        movingLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || horizontalAxis == -1;
        movingRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || horizontalAxis == 1;
    }

    void UpdateMovement()
    {

        if(timeCounter > jumpTime) canMove = true;
        else canMove = false;
        timeCounter += Time.deltaTime;

        if(canMove)
        {
            if (movingUp) MoveUp();
            else if (movingLeft) MoveLeft();
            else if (movingDown) MoveDown();
            else if (movingRight) MoveRight();

            idle = !(movingDown || movingLeft || movingRight || movingUp);
            onAir = !idle;

            if (onAir) timeCounter = 0;
        } else {
        }
    
        if(onAir) gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goalPosition, Time.deltaTime * moveSpeed + 0.05f);

    }

    void Move(Vector3 Direction)
    {
        goalPosition = gameObject.transform.position + Direction;
        animator.Play("rabbit_move");
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
