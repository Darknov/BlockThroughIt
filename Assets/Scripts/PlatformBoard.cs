using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBoard : MonoBehaviour {

    public GameObject block;
    public int rowLength;
    public float widthOfAGap = 0.0f;
    public GameObject[,] blocks;
    public float delayOnDeletingBlock = 3f;

    private float transposeBy;

    // Use this for initialization
    void Start () {
        transposeBy = (float)((rowLength / 2.0) * block.GetComponent<BoxCollider>().size.x + (widthOfAGap * (rowLength - 1.0))/2.0) - block.GetComponent<BoxCollider>().size.x/2;
        this.transform.position = new Vector3(transform.position.x - transposeBy, transform.position.y, transform.position.z - transposeBy);


	}

    void Awake()
    {
        this.blocks = new GameObject[rowLength, rowLength];
    }


    // Update is called once per frame
    void Update () {
		
	}

    public void addBlock(int x, int y)
    {
        Vector3 vector = new Vector3(this.transform.position.x + x + widthOfAGap * x, this.transform.position.y, this.transform.position.z + y + widthOfAGap * y);
        if (x >= this.transform.position.x && x < rowLength && y >= this.transform.position.x && x < rowLength)
        {
            this.blocks[x, y] = Instantiate(block, vector, Quaternion.identity);
            this.blocks[x, y].tag = "platform";
            checkIfThereAreMaxBlocksInRow(); 
        } else
        {
            Debug.Log("x,y:" + x + "," + y + " is out of bounds. Check your code!");
        }
    }

    public void addBlock(AttackBlock activeBlock)
    {

        Transform[] gameObjects = activeBlock.GetComponentsInChildren<Transform>();
        foreach (Transform item in gameObjects)
        {
            int x = Convert.ToInt32(item.position.x + transposeBy);
            int y = Convert.ToInt32(item.position.z + transposeBy);
            Vector3 vector = new Vector3(this.transform.position.x + x + widthOfAGap * x, this.transform.position.y, this.transform.position.z + y + widthOfAGap * y);


            if (x >= transform.position.x + transposeBy && x < rowLength && y >= transform.position.z + transposeBy && y < rowLength)
            {
                Destroy(item.gameObject);

                if (this.blocks[x, y] == null) {
                    this.blocks[x, y] = Instantiate(block, vector, Quaternion.identity);
                    this.blocks[x, y].tag = "platform";
                }
                
                checkIfThereAreMaxBlocksInRow();
            }
            else
            {
                //Debug.Log("x,y:" + x + "," + y + " is out of bounds. You do not belong here. BEGONE!");
                Destroy(item.gameObject);
            }
        }


    }

    public void checkIfThereAreMaxBlocksInRow()
    {
       
        for(int i = 0; i < rowLength; i++)
        {
            bool isBlock = true;
            for(int j = 0; j < rowLength; j++)
            {
                if(this.blocks[i,j] == null)
                {
                    isBlock = false;
                }
            }
            if(isBlock)
            {
                for(int j = 0; j < rowLength; j++)
                {
                    this.blocks[i, j].gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    Destroy(this.blocks[i, j], delayOnDeletingBlock);
                }
            }
            isBlock = true;
            for (int j = 0; j < rowLength; j++)
            {
                if (this.blocks[j, i] == null)
                {
                    isBlock = false;
                }
            }
            if (isBlock)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    this.blocks[j, i].gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    Destroy(this.blocks[j, i], delayOnDeletingBlock);
                }
            }

        }
    }
}
