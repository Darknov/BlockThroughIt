using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    public float velocity;
    private float jumpHeight = 1f;
    private float targetX = 0f;
    private float targetZ = 0f;
    private Vector3 TargetPosition = Vector3.zero;

    private bool onAir = true;

    private float horizontalAxis;
    private float vertivalAxis;
    private bool isHorizontalAxisInUse = false;
    private bool isVerticalAxisInUse = false;


    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, velocity * Time.deltaTime);
    }

    void Move()
    {
        if (onAir) return;

        horizontalAxis = Input.GetAxisRaw("HorizontalJoy");
        vertivalAxis = Input.GetAxisRaw("VerticalJoy");

        if (isHorizontalAxisInUse == false)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || horizontalAxis == 1)
            {
                TargetPosition = new Vector3(targetX + 1f, jumpHeight, targetZ);
                targetX += 1f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(1f, 0f, 0f)), 1f);
                isHorizontalAxisInUse = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || horizontalAxis == -1)
            {
                TargetPosition = new Vector3(targetX - 1f, jumpHeight, targetZ);
                targetX -= 1f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(-1f, 0f, 0f)), 1f);
                isHorizontalAxisInUse = true;

            }
        }
        if (isVerticalAxisInUse == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || vertivalAxis == 1)
            {
                TargetPosition = new Vector3(targetX, jumpHeight, targetZ + 1f);
                targetZ += 1f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0f, 0f, 1f)), 1f);
                isVerticalAxisInUse = true;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || vertivalAxis == -1)
            {
                TargetPosition = new Vector3(targetX, jumpHeight, targetZ - 1f);
                targetZ -= 1f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0f, 0f, -1f)), 1f);
                isVerticalAxisInUse = true;
            }
        }

        if (horizontalAxis == 0)
            isHorizontalAxisInUse = false;

        if (vertivalAxis == 0)
            isVerticalAxisInUse = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "platform")
        {
            TargetPosition.y = 0f;
            onAir = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "platform")
        {
            onAir = true;
        }
    }
}