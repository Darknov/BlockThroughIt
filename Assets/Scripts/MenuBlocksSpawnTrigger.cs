using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBlocksSpawnTrigger : MonoBehaviour {

    public MenuBlocksGenerator spawner;

	void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject.transform.parent.gameObject);
        spawner.SpawnNewBlock();
    }
}
