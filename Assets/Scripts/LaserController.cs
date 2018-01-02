using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : BoostItem
{

    public LineRenderer lineRenderer;
    public float laserGrowingSpeed;
    public float laserRange;
    public bool isActivated = false;

    public float duration;
    private float timeCounter = 0;
    private Vector3 shootDirection;


    public override void ActivateItem()
    {
        isActivated = true;
    }


    void Update()
    {

        if (player != null) player.GetComponent<Player1Controller>().IsPlayerStopped = isActivated;

        if (!isActivated) return;

        if (timeCounter < duration)
        {
            if (gameObject.transform.localScale.z < laserRange)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z + 1f * laserGrowingSpeed);
            }

            RaycastHit[] hitInfos;


            Vector3 pos = gameObject.transform.position + Vector3.down;
            Vector3 dir = this.transform.position + this.transform.rotation * Vector3.forward * 1000f;


            hitInfos = Physics.RaycastAll(pos, dir, 1000f);

            Debug.DrawLine(pos, dir, Color.cyan, 1000f);

            Dictionary<int, int> childrenRegister = new Dictionary<int, int>();

            foreach (var hitInfo in hitInfos)
            {
                if (hitInfo.collider.gameObject.CompareTag("block"))
                {

                    var parent = hitInfo.collider.transform.parent;
                    int parentId = parent.GetInstanceID();

                    if(childrenRegister.ContainsKey(parentId))
                    {
                        childrenRegister[parentId] = childrenRegister[parentId] - 1;
                    }
                    else
                    {
                        childrenRegister.Add(parentId, parent.childCount - 1);
                    }

                    if(childrenRegister[parentId] == 0)
                    {
                        Destroy(parent.gameObject);
                    }

                    Destroy(hitInfo.collider.gameObject);


                }

            }

        }
        else
        {
            DeactivateLaser();
        }
        timeCounter += Time.deltaTime;




    }

    private void DeactivateLaser()
    {
        isActivated = false;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, 0);
        timeCounter = 0;
    }

}
