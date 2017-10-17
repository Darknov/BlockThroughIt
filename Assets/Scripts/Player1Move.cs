using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    public float velocity;
    public float jumpHeight;


    void Update()
    {
        Move();
    }  
    
    void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxisJoystick = Input.GetAxis("HorizontalJoy");
        float verticalAxisJoystick = Input.GetAxis("VerticalJoy");
        bool isGrounded = false;
        RaycastHit hit;
        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
        Vector3 movement = Vector3.zero;

        if(this.velocity == 0.0f)
        {
            this.velocity = 0.025f;
        }

        if (this.jumpHeight == 0.0f)
        {
            this.jumpHeight = 3.0f;
        }

        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.8f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if ((int)horizontalAxisJoystick != 0 || (int)verticalAxisJoystick != 0)
        {
            movement = new Vector3(this.velocity * horizontalAxisJoystick, 0f, this.velocity * verticalAxisJoystick);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }

        movement = new Vector3(horizontalAxis * velocity, 0f, verticalAxis * velocity);
       
        if (!movement.Equals(Vector3.zero))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(movement.x, 0f, movement.z)), 0.15F);
        }
        transform.Translate(movement * Time.deltaTime * 60, Space.World);
    }
}
