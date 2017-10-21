using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBoard : MonoBehaviour {

    public GameObject block;
    public int rowLength;
    public float widthOfAGap = 0.2f;
    public GameObject[,] blocks;

    // Use this for initialization
    void Start () {
        
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
        if (x >= 0 && x < rowLength && y >= 0 && x < rowLength)
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
        Debug.Log(blocks.Length);
        foreach (Transform item in gameObjects)
        {
            int x = Convert.ToInt32(item.position.x);
            int y = Convert.ToInt32(item.position.z);
            //Debug.Log("BLOCK: " + x + ", " + y);
            Vector3 vector = new Vector3(this.transform.position.x + x + widthOfAGap * x, this.transform.position.y, this.transform.position.z + y + widthOfAGap * y);


            if (x >= transform.position.x && x < rowLength && y >= transform.position.z && y < rowLength)
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
                    Destroy(this.blocks[i, j], 3);
                    Debug.Log("DESTROYED: " + i + ", " + j);
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
                    this.blocks[i, j].gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    Destroy(this.blocks[j, i], 3);
                    Debug.Log("DESTROYED: " + j + ", " + j);
                }
            }

        }
    }
}
