using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlock : MonoBehaviour {

    public float jumpLength = 1;
    public bool isBlockInMovement = false;
    public Vector3 moveDirection;

    private float moveSpeed;
    private float jumpTime = 1;
    private float currentTime;

    public float MoveSpeed
    {
        set
        {
            this.moveSpeed = value;
        }
    }

    void Start () {
        if (moveDirection == null || moveDirection == Vector3.zero) throw new NullReferenceException("You must assign direction to " + this.name);

        if (moveSpeed >= jumpTime) throw new ArgumentException("moveSpeed is too big. It should be < " + jumpTime);

        if (moveSpeed == 0) throw new ArgumentException("moveSpeed of " + this.name + "cannot be 0");

        jumpTime = 1 / moveSpeed;
        currentTime = jumpTime;
	}
	
	void Update () {

        if (!isBlockInMovement) return;
        
        if (currentTime >= jumpTime)
        {
            gameObject.transform.position += moveDirection;
            currentTime = 0;
        }

        currentTime += Time.deltaTime;// * moveSpeed;
    }


    public void Activate()
    {
        isBlockInMovement = true;
    }
}
