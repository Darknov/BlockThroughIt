using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    public float velocity;


    void Update()
    {
        Move();
    }  
    
    void Move()
    {
        float horizontal = Input.GetAxis("HorizontalJoy");
        float vertical = Input.GetAxis("VerticalJoy");

        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
        Vector3 velocityVector = Vector3.zero;

        velocityVector = new Vector3(this.velocity * vertical, 0f, this.velocity * horizontal);

        if (this.velocity == 0.0f)
        {
            this.velocity = 0.025f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocityVector = new Vector3(0f, 0f, -this.velocity);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocityVector = new Vector3(0f, 0f, this.velocity);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocityVector = new Vector3(this.velocity, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocityVector = new Vector3(-this.velocity, 0f, 0f);
        }

       
        rigidbody.MovePosition(rigidbody.position + velocityVector);
    }
}
