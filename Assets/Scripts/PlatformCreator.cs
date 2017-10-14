using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    public GameObject block;
    public GameObject player1;

    public int numberOfBlocksInRow;
    public int numberOfInstatiatedBlocks;

    bool isPlayer1Initialized = false;

    void Start()
    {
		float widthOfAGap = 0;
        block.GetComponent<Rigidbody>().isKinematic = true;

        if(numberOfInstatiatedBlocks > (numberOfBlocksInRow*numberOfBlocksInRow -1))
        {
            numberOfInstatiatedBlocks = numberOfBlocksInRow * numberOfBlocksInRow;
        }

        bool[,] isBlockOnPosition = getRandomizedIsBlockOnPositionArray();

        for (int i = 0; i < numberOfBlocksInRow; i++)
        {
            for (int j = 0; j < numberOfBlocksInRow; j++)
            {
                if (isBlockOnPosition[i, j])
                {
					Vector3 startPosition = new Vector3(this.transform.position.x - numberOfBlocksInRow/2, this.transform.position.y, this.transform.position.z - numberOfBlocksInRow/2);

					Vector3 vector = new Vector3(startPosition.x + i + widthOfAGap * i, startPosition.y, startPosition.z + j + widthOfAGap * j);
                    Instantiate(block, vector, Quaternion.identity);

                    if (!isPlayer1Initialized)
                    {
                        Instantiate(player1, new Vector3(vector.x, vector.y + 3f, vector.z), Quaternion.identity);
                        isPlayer1Initialized = true;
                    }
                }
            }
        }


    }

    void Update()
    {

    }

    bool[,] getRandomizedIsBlockOnPositionArray()
    {
        System.Random random = new System.Random();

        bool[,] isBlockOnPosition = new bool[this.numberOfBlocksInRow, this.numberOfBlocksInRow];
        Vector2 currentIsBlock = new Vector2((int)(numberOfBlocksInRow / 2 + 1), (int)(numberOfBlocksInRow / 2 + 1));


        int i = 0;
        do
        {
            isBlockOnPosition[(int)currentIsBlock.x, (int)currentIsBlock.y] = true;
            
            currentIsBlock = findFreeBlock(isBlockOnPosition);
            switch (random.Next(4))
            {
                case 0: // up
                    if (currentIsBlock.y > 0)
                    {
                        if (!isBlockOnPosition[(int)currentIsBlock.x, (int)currentIsBlock.y - 1])
                        {
                            currentIsBlock.y -= 1;
                            i++;
                        }
                    }
                    break;
                case 1: // right
                    if (currentIsBlock.x < numberOfBlocksInRow - 1)
                    {
                        if (!isBlockOnPosition[(int)currentIsBlock.x + 1, (int)currentIsBlock.y])
                        {
                            currentIsBlock.x += 1;
                            i++;
                        }
                    }
                    break;
                case 2: // down
                    if (currentIsBlock.y < numberOfBlocksInRow - 1)
                    {
                        if (!isBlockOnPosition[(int)currentIsBlock.x, (int)currentIsBlock.y + 1])
                        {
                            currentIsBlock.y += 1;
                            i++;
                        }
                    }
                    break;
                case 3: // left
                    if (currentIsBlock.x > 0)
                    {
                        if (!isBlockOnPosition[(int)currentIsBlock.x - 1, (int)currentIsBlock.y])
                        {
                            currentIsBlock.x -= 1;
                            i++;
                        }
                    }
                    break;

            }
            
        } while (i < numberOfInstatiatedBlocks);



        return isBlockOnPosition;
    }

    Vector2 findFreeBlock(bool[,] isBlockOnPosition)
    {
        Vector2 currentIsBlock = new Vector2(0, 0);

        bool leftBlock;
        bool rightBlock;
        bool upBlock;
        bool downBlock;
        for (int i = 0; i < numberOfBlocksInRow; i++)
        {
            for (int j = 0; j < numberOfBlocksInRow; j++)
            {
                if (isBlockOnPosition[i, j])
                {

                    leftBlock = isBlockOnPosition[(int)currentIsBlock.x, (int)currentIsBlock.y];
                    rightBlock = isBlockOnPosition[(int)currentIsBlock.x, (int)currentIsBlock.y];
                    upBlock = isBlockOnPosition[(int)currentIsBlock.x, (int)currentIsBlock.y];
                    downBlock = isBlockOnPosition[(int)currentIsBlock.x, (int)currentIsBlock.y];
                    if (i > 0)
                        leftBlock = isBlockOnPosition[i - 1, j];
                    if (i < numberOfBlocksInRow - 1)
                        rightBlock = isBlockOnPosition[i + 1, j];
                    if (j > 0)
                        upBlock = isBlockOnPosition[i, j - 1];
                    if (j < numberOfBlocksInRow - 1)
                        downBlock = isBlockOnPosition[i, j + 1];

                    if (!leftBlock || !rightBlock || !upBlock || !downBlock)
                    {
                        currentIsBlock = new Vector2(i, j);
                        i = numberOfBlocksInRow;
                        j = numberOfBlocksInRow;
                        
                    }
                }
            }
        }

        return currentIsBlock;
    }

}
