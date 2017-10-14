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
            checkIfThereAreMaxBlocksInRow(); 
        } else
        {
            Debug.Log("x,y:" + x + "," + y + " is out of bounds. Check your code!");
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
                    Destroy(this.blocks[i, j], 3);
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
                    Destroy(this.blocks[j, i], 3);
                }
            }

        }
    }
}
