using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1MoveandAutoJump : MonoBehaviour
{
    public float velocity;
    public float jumpHeight = 3.0f;

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

        Vector3 movement = Vector3.zero;

        if ((int)horizontalAxisJoystick != 0 || (int)verticalAxisJoystick != 0)
        {
            movement = new Vector3(this.velocity * horizontalAxisJoystick, 0f, this.velocity * verticalAxisJoystick);
        }

        if (this.velocity == 0.0f)
        {
            this.velocity = 0.025f;
        }

        movement = new Vector3(horizontalAxis * velocity, 0f, verticalAxis * velocity);

        if (!movement.Equals(Vector3.zero))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(movement.x, 0f, movement.z)), 0.15F);
        }

        transform.Translate(movement * Time.deltaTime * 60, Space.World);
    }

    void OnTriggerExit(Collider col)
    {
        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
        if (col.gameObject.tag == "platform")
        {
            rigidbody.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }
}