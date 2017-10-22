using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestroyer : MonoBehaviour {

	void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.GetComponentInParent<AttackBlock>() != null)
        {
            collider.GetComponentInParent<AttackBlock>().OnAttackBlockDestroy();
            Destroy(collider.transform.parent.gameObject);
        }
        else
        {
            Destroy(collider.gameObject);
        }

    }
}
