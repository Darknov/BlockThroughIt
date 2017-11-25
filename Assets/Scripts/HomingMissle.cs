using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour {
    public Rigidbody missle;

    private AttackBlock target;
    private float speed = 15f;

    void Start () {
        missle = GetComponent<Rigidbody>();
      /* foreach (var item in GameObject.FindObjectsOfType<AttackBlock>())
        {
            if(item.IfInMovement())
            {
                target = item;
            }
        }*/
        target = GameObject.FindObjectOfType<Player2Controller>().GetActiveBlock();
    }
	
	void FixedUpdate () {       
        missle.gameObject.transform.Rotate(Vector3.forward *8);
        missle.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    
    void OnCollisionEnter(Collision theCollision)
    {
       if(theCollision.transform.position == target.transform.position)
        {
            Destroy(missle.gameObject);

            target.GetComponentInParent<AttackBlock>().OnAttackBlockDestroy();
            Destroy(target.transform.gameObject);

            GameObject.FindObjectOfType<ShadowBlock>().DestroyShadow();
        }
    }

}
