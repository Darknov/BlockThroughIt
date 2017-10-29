using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour {
    public Rigidbody missle;

    private GameObject target;
    private float speed = 15f;

    void Start () {
        missle = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("active");
    }
	
	void FixedUpdate () {
        if(!GameObject.FindGameObjectWithTag("active") )
        {
            Destroy(missle.gameObject);
        }       
        missle.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    
    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.tag == "active")
        {
            Destroy(missle.gameObject);
            target.GetComponentInParent<AttackBlock>().OnAttackBlockDestroy();
            Destroy(target.transform.gameObject);

        }
    }

}
