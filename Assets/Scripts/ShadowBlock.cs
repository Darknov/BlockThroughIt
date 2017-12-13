using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBlock : MonoBehaviour {

    public Material shadowBlockMaterial;
    public PlatformBoard platformBoard;
    private GameObject activeBlockShadow;

    private enum Axis
    {
        Horizontal,
        Vertical
    }

    public void CreateShadow(GameObject attackBlock)
    {
        GameObject[,] platformBlocks = platformBoard.blocks;
        AttackBlock attackBlockComponent = attackBlock.GetComponent<AttackBlock>();

        GameObject[] platformBlocksOneD = ConvertToGameObjectArray(platformBlocks);
        GameObject[] attackBlockChildren = GetChildren(attackBlock);
        GameObject[] closestPair = FindOneClosestPair(platformBlocksOneD, attackBlockChildren, GetAxis(attackBlockComponent));

        if (closestPair == null) return;

        System.Random random = new System.Random();

        Color randomColor = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

        Vector3 position = closestPair[0].transform.position - attackBlockComponent.MoveDirection;
        position = position - (attackBlock.transform.rotation * closestPair[1].transform.localPosition);
        this.activeBlockShadow = Instantiate(attackBlock, position, attackBlock.transform.rotation);
		activeBlockShadow.transform.tag = "shadow";
		for (int i = 0; i < activeBlockShadow.transform.childCount; i++) {
			activeBlockShadow.transform.GetChild (i).tag = "shadow";
		}

        DestroyColliders();
        ImmobilizeShadow();
        ColorizeShadow();

    }
    public void DestroyShadow()
    {
        Destroy(this.activeBlockShadow);
    }
    private GameObject[] GetChildren(GameObject gObj)
    {
        Transform[] transforms = gObj.GetComponentsInChildren<Transform>();
        List<GameObject> gobjs = new List<GameObject>();
        foreach (var item in transforms)
        {
            if(item.gameObject != gObj)
                gobjs.Add(item.gameObject);
        }
        return gobjs.ToArray();
    } 
    private GameObject[] ConvertToGameObjectArray(GameObject[,] objects)
    {
        List<GameObject> transforms = new List<GameObject>();

        for(int i=0; i<objects.GetLength(0); i++)
            for(int j=0; j<objects.GetLength(1); j++)
            {
                if(objects[i,j]!=null)
                    transforms.Add(objects[i, j]);
            }

        return transforms.ToArray();
    }
    private double GetDistanceOfObjectClosestToPerpendicularAxis(Transform[] objects, Axis moveDirection)
    {
        if(moveDirection == Axis.Horizontal)
        {
            double minDistance = Double.MaxValue;

            for (int i = 0; i < objects.Length; i++)
            {
                double distance = Mathf.Abs(objects[i].position.x);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            return minDistance;
        }
        else
        {
            double minDistance = Double.MaxValue;

            for (int i = 0; i < objects.Length; i++)
            {
                double distance = Mathf.Abs(objects[i].position.z);
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }


            return minDistance;
        }

    }
    private GameObject[] FindOneClosestPair(GameObject[] firstGroup, GameObject[] secondGroup, Axis moveDirection)
    {
        double minDistance = Double.MaxValue;
        GameObject[] closestPair = null;

        foreach (var first in firstGroup)
        {
            foreach (var second in secondGroup)
            {
                bool areOnSameWay = false;

                if (moveDirection == Axis.Vertical) areOnSameWay = (Mathf.Abs(first.transform.position.x - second.transform.position.x) < 0.5f);
                else areOnSameWay = (Mathf.Abs(first.transform.position.z - second.transform.position.z) < 0.5f);

                if(areOnSameWay)
                {
                    double distance = Vector3.Distance(first.transform.position, second.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestPair = new GameObject[]
                        {
                            first, second
                        };
                    }
                }

            }
        }

        return closestPair;
    }
    private void ColorizeShadow()
    {
        Renderer[] renderers = activeBlockShadow.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.material = shadowBlockMaterial;
        }
    }
    private void DestroyColliders()
    {
        foreach (var item in activeBlockShadow.GetComponentsInChildren<BoxCollider>())
        {
            if (!item.isTrigger) Destroy(item);
        }
    }
    private void ImmobilizeShadow()
    {
        activeBlockShadow.GetComponent<AttackBlock>().MoveDirection = Vector3.zero;
    }
    private Axis GetAxis(AttackBlock attackBlock)
    {
        if (attackBlock.MoveDirection == Vector3.forward || attackBlock.MoveDirection == Vector3.back) return Axis.Vertical;
        else return Axis.Horizontal;
    }



}
