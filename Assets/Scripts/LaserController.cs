using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

	public LineRenderer lineRenderer;
	public float laserGrowingSpeed;
	public float laserRange;
	public bool isActivated = false;

	public float duration;
	private float timeCounter = 0;
	private Vector3 shootDirection;

	public void ActivateLaser(Vector3 direction) {
		isActivated = true;
		shootDirection = direction * 100f;

		// shootDirection += Vector3.down;
	}

	void Update() {

		if(!isActivated) return;

		if(timeCounter < duration)
		{
			if(gameObject.transform.localScale.z < laserRange)
			{
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z  + 1f * laserGrowingSpeed);
			}

			RaycastHit[] hitInfos;


			Vector3 pos = gameObject.transform.position + Vector3.down * 0.5f;
			Vector3 dir = shootDirection + Vector3.down * 0.5f;


			Debug.LogError("Position: " + pos);
			Debug.LogError("Direction: " + dir);

			hitInfos = Physics.RaycastAll(pos, dir, 1000f);

			Debug.DrawLine(pos, dir, Color.cyan, 100f);

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
