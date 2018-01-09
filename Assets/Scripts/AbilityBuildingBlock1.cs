using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBuildingBlock1 : BoostItem
{
    private PlatformBoard platform;
    int rowLength;
    private bool isActive = false ;
    private bool wasUsed = false;
    public GameObject partEffect;
    private float lastTy = 0;

    void Start()
    {
        this.platform = GameObject.FindGameObjectWithTag("platformBoard").GetComponent<PlatformBoard>();
        this.rowLength = this.platform.rowLength;
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void ActivateItem()
    {
        FindObjectOfType<AudioManager>().Play("AbilityPutBlock"); ///////////
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {

        int x, z;
        float tY;

        if (isActive)
        {
            x = Convert.ToInt32(player.transform.position.x + (this.rowLength - 1) / 2);
            z = Convert.ToInt32(player.transform.position.z + (this.rowLength - 1) / 2);
            tY = this.transform.rotation.eulerAngles.y;
            if (tY == 90)
            {
                Instantiate(partEffect, new Vector3(player.transform.position.x+1, player.transform.position.y, player.transform.position.z), player.transform.rotation, player.transform);
                platform.addBlock(x + 1, z);
            }
            if (tY == 270)
            {
                Instantiate(partEffect, new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z), player.transform.rotation, player.transform);
                platform.addBlock(x - 1, z);
            }
            if (tY == 0)
            {
                Instantiate(partEffect, new Vector3(player.transform.position.x , player.transform.position.y, player.transform.position.z+1), player.transform.rotation, player.transform);
                platform.addBlock(x, z + 1);
            }
            if (tY == 180)
            {
                Instantiate(partEffect, new Vector3(player.transform.position.x , player.transform.position.y, player.transform.position.z-1), player.transform.rotation, player.transform);
                platform.addBlock(x, z - 1);
            }


            wasUsed = true;
            isActive = false;
        }
        else if(!wasUsed)
        {
            tY = this.transform.rotation.eulerAngles.y;


            x = Convert.ToInt32(player.transform.position.x + (this.rowLength - 1) / 2);
            z = Convert.ToInt32(player.transform.position.z + (this.rowLength - 1) / 2);

            if (tY == 90)
            {
                platform.AddShadowBlock(x + 1, z);
            }
            if (tY == 270)
            {
                platform.AddShadowBlock(x - 1, z);
            }
            if (tY == 0)
            {
                platform.AddShadowBlock(x, z + 1);
            }
            if (tY == 180)
            {
                platform.AddShadowBlock(x, z - 1);
            }

            if(tY != lastTy)
            {
                lastTy = tY;
                platform.DestroyShadow();
            }
        }


    }



}

