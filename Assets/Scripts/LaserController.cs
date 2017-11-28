using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : BoostItem {

	public LineRenderer lineRenderer;
	public float laserGrowingSpeed;
	public float laserRange;
	public bool isActivated = false;

	public float duration;
	private float timeCounter = 0;
	private Vector3 shootDirection;


	public override void ActivateItem() {
		isActivated = true;
	}


    void Update() {

		if(player != null) player.GetComponent<Player1Controller>().IsPlayerStopped = isActivated;

		if(!isActivated) return;

		if(timeCounter < duration)
		{
			if(gameObject.transform.localScale.z < laserRange)
			{
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z  + 1f * laserGrowingSpeed);
			}

			RaycastHit[] hitInfos;


			Vector3 pos = gameObject.transform.position + Vector3.down;
			Vector3 dir = this.transform.position + this.transform.rotation * Vector3.forward * 1000f;


			hitInfos = Physics.RaycastAll(pos, dir, 1000f);

			Debug.DrawLine(pos, dir, Color.cyan, 1000f);

			foreach (var hitInfo in hitInfos)
			{
				if(hitInfo.collider.gameObject.CompareTag("block")) 
				{
					Destroy(hitInfo.collider.gameObject);
				}
			}




		} else {
			isActivated = false;
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, 0);
			timeCounter = 0;
		}
		timeCounter += Time.deltaTime;




	}

}
