using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBlocksGenerator : MonoBehaviour {

    public Vector3 spawnCubePosition;
    public float spawnCubeSize;
    public int numberOfInitialBlocks;
    public GameObject[] kindsOfBlocks;
    public int limit;


    private void Start()
    {

        for(int i=0; i<numberOfInitialBlocks; i++)
        {
            SpawnNewBlock();
        }



    }

    public void SpawnNewBlock()
    {



        

        if (this.transform.childCount > limit)
        {
            return;
        }

        Random lotto = new Random();

        float x = Random.Range(spawnCubePosition.x - (spawnCubeSize / 2.0f), spawnCubePosition.x + (spawnCubeSize / 2.0f));
        float y = Random.Range(spawnCubePosition.y - (spawnCubeSize / 2.0f), spawnCubePosition.y + (spawnCubeSize / 2.0f));
        float z = Random.Range(spawnCubePosition.z - (spawnCubeSize / 2.0f), spawnCubePosition.z + (spawnCubeSize / 2.0f));

        int index = Random.Range(0, kindsOfBlocks.Length - 1);

        GameObject block = kindsOfBlocks[index];

        Vector3 position = new Vector3(x,y,z);

        GameObject temp = Instantiate(block, position, Quaternion.Euler(90,0,0));

        temp.GetComponent<Rigidbody>().useGravity = true;
        temp.GetComponent<Rigidbody>().isKinematic = false;

        temp.transform.parent = this.transform;

    }
}
