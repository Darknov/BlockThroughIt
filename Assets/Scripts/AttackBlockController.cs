using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlockController : MonoBehaviour {

    private float jumpLength;
    private bool isBlockInMovement;
    private Vector3 moveDirection;
	public bool wasBlockUsed = false;

    private float moveSpeed;
    private float jumpTime;
    private float currentTime;

    public float JumpLength
    {
        set
        {
            this.jumpLength = value;
        }
    }

    public float MoveSpeed { set
        {
            this.moveSpeed = value;
        }
    }

    public Vector3 MoveDirection { set
        {
            this.moveDirection = value;
        }
    }

	void Start() {
		this.isBlockInMovement = false;
	}
	
	void Update () {
        if (!isBlockInMovement) return;

        currentTime += Time.deltaTime * moveSpeed;

        if (currentTime >= jumpTime)
        {
            gameObject.transform.position += moveDirection;
            currentTime = 0;
        }

    }


	public void SetActivationState(bool state)
    {
		if (state && wasBlockUsed)
			return;

        isBlockInMovement = state;

		Debug.Log (Time.time.ToString () + ": setting activation state to " + state);

		wasBlockUsed = true;
    }
		

    public void Initialize()
    {
        if (moveDirection == null || moveDirection == Vector3.zero) throw new NullReferenceException("You must assign direction to " + this.name);

        if (moveSpeed == 0) throw new ArgumentException("moveSpeed of " + this.name + "cannot be 0");

        jumpTime = (1 / moveSpeed)*Time.deltaTime;
        currentTime = jumpTime;

		BoxCollider[] colliders = this.gameObject.GetComponentsInChildren<BoxCollider> ();

		foreach (var item in colliders) {
			if (item.isTrigger) {
				
				Vector3 offset = moveDirection;
				offset.Scale (new Vector3 (0.5f, 0.5f, 0.5f));
				item.center = offset;
			}
		}
    }

	void OnTriggerEnter(Collider other) {
		if (other.tag == "platform") {
			SetActivationState (false);
			BecomePartOfPlatform ();
		}
	}

	void BecomePartOfPlatform() {
		this.gameObject.tag = "platform"; 

		var children = this.gameObject.GetComponentsInChildren<Transform> ();

		foreach (var child in children) {
			child.tag = "platform";
		}

		BoxCollider[] colliders = this.gameObject.GetComponentsInChildren<BoxCollider> ();

		foreach (var item in colliders) {
			if (item.isTrigger) {
				Destroy (item);
			}
		}
	}

}
