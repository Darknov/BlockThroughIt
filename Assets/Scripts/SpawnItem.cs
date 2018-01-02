using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour {
    public GameObject[] items;
    public float frequency = 1;
    public int maxItems = 3;
    public PlatformBoard platformBoard;
    public GameObject SoundPlayer;
    private GameObject[,] generatedItems;
    private SoundEffectPlayOnce sound;
    

	// Use this for initialization
	void Start () {
        generatedItems = new GameObject[platformBoard.rowLength, platformBoard.rowLength];
        InvokeRepeating("spawnRandomItem", 1, frequency);
        sound = SoundPlayer.GetComponent<SoundEffectPlayOnce>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnRandomItem()
    {
        int howMany = 0;
        for(int i = 0; i < platformBoard.rowLength; i++)
        {
            for(int j = 0; j < platformBoard.rowLength; j++)
            {
                if(generatedItems[i,j] != null)
                {
                    howMany++;
                }
            }
        }
        int howManyBlocks = 0;
        for (int i = 0; i < platformBoard.rowLength; i++)
        {
            for (int j = 0; j < platformBoard.rowLength; j++)
            {
                if (platformBoard.blocks[i, j] != null)
                {
                    howManyBlocks++;
                }
            }
        }
        if (howMany < maxItems && howMany < howManyBlocks)
        {
            int number = Random.Range(0, items.Length);
            int x;
            int y;
            do
            {
                x = Random.Range(0, platformBoard.rowLength);
                y = Random.Range(0, platformBoard.rowLength);
            } while (platformBoard.blocks[x, y] == null || generatedItems[x, y] != null);
            Vector3 vector = new Vector3(platformBoard.transform.position.x + x + platformBoard.widthOfAGap * x, platformBoard.transform.position.y + 1, platformBoard.transform.position.z + y + platformBoard.widthOfAGap * y);

            generatedItems[x, y] = Instantiate(items[number], vector, Quaternion.identity);
            sound.PlaySound();
        }
    }
}
