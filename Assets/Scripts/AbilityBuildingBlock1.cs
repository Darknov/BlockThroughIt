using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBuildingBlock1 : BoostItem
{
    private PlatformBoard platform;
    int rowLength;
    private bool isActive = false;

    private float lastTy = 0;

    void Start()
    {
        this.platform = GameObject.FindGameObjectWithTag("platformBoard").GetComponent<PlatformBoard>();
        this.rowLength = this.platform.rowLength;
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void ActivateItem()
    {
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
                platform.addBlock(x + 1, z);
            }
            if (tY == 270)
            {
                platform.addBlock(x - 1, z);
            }
            if (tY == 0)
            {
                platform.addBlock(x, z + 1);
            }
            if (tY == 180)
            {
                platform.addBlock(x, z - 1);
            }

            isActive = false;
        }
        else
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

