using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBuildingBlock1 : BoostItem {

    // Use this for initialization
    private PlatformBoard platform;
    int rowLength;
    private bool isActive = false ;
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
    void Update () {
		if(isActive)
        {

            int x = Convert.ToInt32(player.transform.position.x + (this.rowLength - 1) / 2);
            int z = Convert.ToInt32(player.transform.position.z + (this.rowLength - 1) / 2);
            float tY = this.transform.rotation.eulerAngles.y;
            Debug.Log(tY);
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
	}
}
