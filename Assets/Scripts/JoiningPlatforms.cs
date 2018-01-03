using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoiningPlatforms : MonoBehaviour
{

    // Use this for initialization

    private int rowLength;
    private GameObject[,] blocks;
    PlatformBoard platform;
    public float frequency = 2.0f;

    void Start()
    {
        this.rowLength = this.GetComponent<PlatformBoard>().rowLength;
        this.blocks = this.GetComponent<PlatformBoard>().blocks;
        this.platform = this.GetComponent<PlatformBoard>();

        InvokeRepeating("checkPlatform", 0, frequency);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkPlatform()
    {
        if(!StaticOptions.isFlying)
            checkIfPlatformsApart();
    }


    public void checkIfPlatformsApart()
    {
        Vector3 player1 = GameObject.FindGameObjectWithTag("Player").transform.position;
        float x = player1.x;
        float z = player1.z;
        x = x + (rowLength - 1) / 2;
        z = z + (rowLength - 1) / 2;
        for (int i = 0; i < rowLength; i++)
        {
            bool isBlock = false;
            for (int j = 0; j < rowLength; j++)
            {
                if (blocks[i, j] != null)
                {
                    isBlock = true;
                }
            }
            if (!isBlock)
            {
                if(x < i)
                {
                    for (int j = i; j < rowLength; j++)
                    {
                        for (int k = 0; k < rowLength; k++)
                        {
                            if (j + 1 < rowLength && blocks[j + 1, k] != null)
                            {
                                platform.addBlock(j, k);
                                Destroy(blocks[j + 1, k]);
                            }
                        }
                    }
                }

                if(x > i)
                {
                    for (int j = i; j >= 0; j--)
                    {
                        for (int k = 0; k < rowLength; k++)
                        {
                            if (j - 1 >= 0 && blocks[j - 1, k] != null)
                            {
                                platform.addBlock(j, k);
                                Destroy(blocks[j - 1, k]);
                            }
                        }
                    }
                }

            }

        }
        
        for (int i = 0; i < rowLength; i++)
        {
            bool isBlock = false;
            for (int j = 0; j < rowLength; j++)
            {
                if (blocks[j, i] != null)
                {
                    isBlock = true;
                }
            }
            if (!isBlock)
            {

                if (z < i)
                {
                    for (int j = 0; j < rowLength; j++)
                    {
                        for (int k = i; k < rowLength; k++)
                        {
                            if (k + 1 < rowLength && blocks[j, k + 1] != null)
                            {
                                platform.addBlock(j, k);
                                Destroy(blocks[j, k + 1]);
                            }
                        }
                    }
                }

                if (z > i)
                {
                    for (int j = 0; j < rowLength; j++)
                    {
                        for (int k = i; k >= 0; k--)
                        {
                            if (k - 1 >= 0 && blocks[j, k - 1] != null)
                            {
                                platform.addBlock(j, k);
                                Destroy(blocks[j, k - 1]);
                            }
                        }
                    }
                }


                /*
                for (int j = 0; j < rowLength; j++)
                {
                    for (int k = i; k < rowLength; k++)
                    {
                        if (k + 1 < rowLength && blocks[j, k + 1] != null)
                        {
                            platform.addBlock(j, k);
                            Destroy(blocks[j, k + 1]);
                        }
                    }
                }
                */
            }

        }
    }
}