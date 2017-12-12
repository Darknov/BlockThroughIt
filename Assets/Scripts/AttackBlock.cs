using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlock : MonoBehaviour
{

    private float jumpLength;
    private bool isBlockInMovement;
    public bool wasBlockUsed = false;
    public bool isDestroyBlock = false;

    private float moveSpeed;
    private float jumpTime;
    private float currentTime;

    public delegate void PlatformHitEventHandler(object obj, EventArgs args);
    public PlatformHitEventHandler PlatformHit;

    public delegate void AttackBlockDestroyEventHandler(object obj, EventArgs args);
    public AttackBlockDestroyEventHandler DestroyAttackBlock;

    public virtual void OnPlatformHit()
    {
        if (PlatformHit != null)
        {
            PlatformHit(this, EventArgs.Empty);
        }
    }

    public virtual void OnAttackBlockDestroy()
    {
        if (DestroyAttackBlock != null)
        {
            DestroyAttackBlock(this, EventArgs.Empty);
        }
    }

    public float JumpLength
    {
        set
        {
            this.jumpLength = value;
        }
    }

    public float MoveSpeed
    {
        set
        {
            this.moveSpeed = value;
        }
    }

    public void ChangeSpeed(float speed)
    {
        this.moveSpeed = speed;
        Initialize();
    }

    public Vector3 MoveDirection { set; get; }

    void Start()
    {
        this.isBlockInMovement = false;
        AdaptTriggers();
    }

    void FixedUpdate()
    {
        if (!isBlockInMovement) return;

        currentTime += Time.deltaTime * moveSpeed;

        if (currentTime >= jumpTime)
        {
            gameObject.transform.position += MoveDirection;
            currentTime = 0;
        }

    }

    public void AdaptTriggers()
    {
        List<BoxCollider> childrenTriggers = new List<BoxCollider>();

        foreach (var item in gameObject.GetComponentsInChildren<BoxCollider>())
        {
            if (item.isTrigger) childrenTriggers.Add(item);
        }

        foreach (var item in childrenTriggers)
        {
            item.size = new Vector3(0.3f, 0.3f, 0.3f);
            item.center = Quaternion.Inverse(this.gameObject.transform.rotation) * (MoveDirection * 0.5f);
        }

    }

    public void Activate()
    {
        isBlockInMovement = true;
    }

    public void Deactivate()
    {
        isBlockInMovement = false;
    }

    public bool IfInMovement()
    {
        return isBlockInMovement;
    }
    public void Initialize()
    {
        if (MoveDirection == null || MoveDirection == Vector3.zero) throw new NullReferenceException("You must assign direction to " + this.name);

        if (moveSpeed == 0) throw new ArgumentException("moveSpeed of " + this.name + "cannot be 0");

        jumpTime = (1 / (moveSpeed)) * Time.deltaTime;
        currentTime = jumpTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "platform")
        {
            if (isDestroyBlock)
            {
                Destroy(other.gameObject);
                OnPlatformHit();
            }
            else
            {
                Deactivate();
                BecomePartOfPlatform();
            }
        }
    }

    void BecomePartOfPlatform()
    {
        this.gameObject.tag = "platform";

        var children = this.gameObject.GetComponentsInChildren<Transform>();

        foreach (var child in children)
        {
            child.tag = "platform";
        }

        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (var item in colliders)
        {
            if (item.isTrigger)
            {
                Destroy(item);
            }
        }

        OnPlatformHit();
    }

    public void GoToYourLeft()
    {
        if (MoveDirection == Vector3.forward || MoveDirection == Vector3.back)
        {
            gameObject.transform.localPosition = gameObject.transform.localPosition + Vector3.left;
        }
        else
        {
            gameObject.transform.localPosition = gameObject.transform.localPosition + Vector3.forward;
        }
    }

    public void GoToYourRight()
    {
        if (MoveDirection == Vector3.forward || MoveDirection == Vector3.back)
        {
            gameObject.transform.localPosition = gameObject.transform.localPosition + Vector3.right;
        }
        else
        {
            gameObject.transform.localPosition = gameObject.transform.localPosition + Vector3.back;
        }
    }

    public void TurnNinetyDegreesAndUpdateTriggers()
    {
        gameObject.transform.Rotate(0, 90, 0);
        AdaptTriggers();
    }

}

