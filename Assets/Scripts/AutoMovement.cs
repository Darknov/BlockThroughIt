using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    public float velocity;
    public float jumpTime;
    public CountDown countDown;
    private float jumpHeight = 1f;
    private float targetX;
    private float targetZ;
    private Vector3 TargetPosition;

    private bool onAir = true;
    private float timeCounter;
    private float horizontalAxis;
    private float vertivalAxis;
    private bool isHorizontalAxisInUse = false;
    private bool isVerticalAxisInUse = false;

    private MoveKey lastKey = MoveKey.None;

    public bool inverseControl;
    public float timeOfInverseControlIfExist;
    private float inverseTimer = 0.0f;

    void Start()
    {
        TargetPosition = transform.position;
        targetX = transform.position.x;
        targetZ = transform.position.z;
        timeCounter = jumpTime;

        countDown = FindObjectOfType<CountDown>();

        if (timeOfInverseControlIfExist == 0.0f)
            timeOfInverseControlIfExist = 5f;
    }

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

        horizontalAxis = Input.GetAxisRaw("HorizontalJoy");
        vertivalAxis = Input.GetAxisRaw("VerticalJoy");

        if (inverseControl)
        {
            inverseTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || horizontalAxis == -1)
                lastKey = MoveKey.Right;
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || horizontalAxis == 1)
                lastKey = MoveKey.Left;
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || vertivalAxis == -1)
                lastKey = MoveKey.Up;
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || vertivalAxis == 1)
                lastKey = MoveKey.Down;

            if (inverseTimer > timeOfInverseControlIfExist)
            {
                inverseControl = false;
                inverseTimer = 0.0f;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || horizontalAxis == 1) lastKey = MoveKey.Right;
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || horizontalAxis == -1) lastKey = MoveKey.Left;
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || vertivalAxis == 1) lastKey = MoveKey.Up;
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || vertivalAxis == -1) lastKey = MoveKey.Down;
        }


        if (lastKey != MoveKey.None)
        {
            this.countDown.started = true;
        }


        if (timeCounter > 0)
        {
            timeCounter -= Time.deltaTime;
            return;
        }

        if (onAir) return;



        if (isHorizontalAxisInUse == false)
        {
            if (lastKey == MoveKey.Right)
            {
                TargetPosition = new Vector3(targetX + 1f, jumpHeight, targetZ);
                targetX += 1f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(1f, 0f, 0f)), 1f);
                //isHorizontalAxisInUse = true;
            }

            if (lastKey == MoveKey.Left)
            {
                TargetPosition = new Vector3(targetX - 1f, jumpHeight, targetZ);
                targetX -= 1f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(-1f, 0f, 0f)), 1f);
                //isHorizontalAxisInUse = true;
            }
        }
        if (isVerticalAxisInUse == false)
        {
            if (lastKey == MoveKey.Up)
            {
                TargetPosition = new Vector3(targetX, jumpHeight, targetZ + 1f);
                targetZ += 1f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0f, 0f, 1f)), 1f);
                //isVerticalAxisInUse = true;
            }

            if (lastKey == MoveKey.Down)
            {
                TargetPosition = new Vector3(targetX, jumpHeight, targetZ - 1f);
                targetZ -= 1f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(0f, 0f, -1f)), 1f);
                //isVerticalAxisInUse = true;
            }
        }

        if (horizontalAxis == 0)
            isHorizontalAxisInUse = false;

        if (vertivalAxis == 0)
            isVerticalAxisInUse = false;

        timeCounter = jumpTime;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "platform")
        {
            TargetPosition.y = 0.5f;
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

    public enum MoveKey
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
}