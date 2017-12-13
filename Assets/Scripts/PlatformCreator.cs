using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    public GameObject block;
    public GameObject player1;
    public int numberOfInstatiatedBlocks;
    public PlatformBoard platformBoard;
    public bool generatePlatform = true;
    private int numberOfBlocksInRow;
    private ArrayList colorPalette = new ArrayList() {Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };
    

    bool isPlayer1Initialized = false;

    void Start()
    {

        numberOfBlocksInRow = platformBoard.rowLength - 1;

		float widthOfAGap = 0;
        block.GetComponent<Rigidbody>().isKinematic = true;

        if(numberOfInstatiatedBlocks > (numberOfBlocksInRow*numberOfBlocksInRow -1))
        {
            numberOfInstatiatedBlocks = numberOfBlocksInRow * numberOfBlocksInRow - 1;
        }

        bool[,] isBlockOnPosition;

        if (generatePlatform)
        {
            isBlockOnPosition = getRandomizedIsBlockOnPositionArray();
        }
        else
        {
            isBlockOnPosition = getStaticIsBlockOnPositionArray();
        }

        for (int i = 0; i < numberOfBlocksInRow; i++)
        {
            for (int j = 0; j < numberOfBlocksInRow; j++)
            {
                if (isBlockOnPosition[i, j])
                {

                    this.platformBoard.addBlock(i, j);
                    this.platformBoard.blocks[i, j].GetComponent<MeshRenderer>().material.color = (Color)colorPalette[Random.Range(0, colorPalette.Count)];//Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f, 0f, 0f);

                    Vector3 vector = new Vector3(platformBoard.transform.position.x, 0, platformBoard.transform.position.z);

                    if (!isPlayer1Initialized)
                    {
                        player1.transform.position = new Vector3(vector.x + i, vector.y + 1f, vector.z + j);
                        isPlayer1Initialized = true;
                    }
                }
            }
        }


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

    bool[,] getStaticIsBlockOnPositionArray()
    {
        bool[,] isBlockOnPosition = new bool[this.numberOfBlocksInRow, this.numberOfBlocksInRow];

        char[] blocks0 ={ ' ', 'B', ' ', ' ',' ',
                          ' ', 'B', ' ', 'B',' ',
                          ' ', 'B', 'B', 'B','B',
                          ' ', 'B', 'B', 'B',' ',
                          ' ', ' ', ' ', 'B',' '};

        char[] blocks1 ={ ' ', 'B', ' ', ' ',' ',
                          ' ', 'B', 'B', ' ',' ',
                          ' ', 'B', ' ', 'B','B',
                          ' ', 'B', 'B', 'B',' ',
                          ' ', ' ', ' ', ' ',' '};

        char[] blocks2 ={ ' ', 'B', ' ', ' ',' ',
                          ' ', 'B', ' ', 'B',' ',
                          ' ', 'B', 'B', 'B','B',
                          ' ', 'B', 'B', 'B',' ',
                          ' ', ' ', ' ', 'B',' '};

        char[] blocks3 ={ ' ', 'B', ' ', ' ',' ',
                          'B', 'B', ' ', 'B','B',
                          ' ', 'B', 'B', 'B','B',
                          ' ', 'B', ' ', 'B',' ',
                          'B', ' ', ' ', 'B',' '};

        char[] blocks4 ={ ' ', 'B', 'B', ' ','B',
                          'B', 'B', ' ', 'B',' ',
                          ' ', 'B', 'B', 'B','B',
                          ' ', 'B', 'B', 'B',' ',
                          'B', ' ', ' ', 'B',' '};

        char[] blocks5 ={ 'B', 'B', ' ', ' ',' ',
                          'B', 'B', ' ', 'B',' ',
                          ' ', 'B', 'B', 'B','B',
                          ' ', 'B', 'B', 'B',' ',
                          ' ', ' ', ' ', 'B','B'};


        List<char[]> blocksList = new List<char[]>();
        blocksList.Add(blocks0);
        blocksList.Add(blocks1);
        blocksList.Add(blocks2);
        blocksList.Add(blocks3);
        blocksList.Add(blocks4);
        blocksList.Add(blocks5);

        char[] winnerBlock = blocksList[Random.Range(0, blocksList.Count)];

        for (int i = 0; i < numberOfBlocksInRow; i++)
        {
            for (int j = 0; j < numberOfBlocksInRow; j++)
            {
                if(i >= 5 || j >= 5)
                {
                    isBlockOnPosition[i, j] = false;
                }
                else
                {
                    if(winnerBlock[i*5 + j] == 'B')
                    {
                        isBlockOnPosition[i, j] = true;
                    }
                    else
                    {
                        isBlockOnPosition[i, j] = false;
                    }
                }
            }
        }

        return isBlockOnPosition;
    }

}
