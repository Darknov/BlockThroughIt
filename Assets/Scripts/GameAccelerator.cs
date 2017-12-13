using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAccelerator : MonoBehaviour {

	public bool isOn = true;
	public float player1Speed;
	public float player2Speed;
	public float player1Acceleration;
	public float player2Acceleration;
	public float speedUpInterval;
	private float timeCounter = 0f;

    void Start()
    {
        if(StaticOptions.mode == 2)
        {
            isOn = true;
        }
    }

    void Update() {

		if(!isOn) return;

		if(timeCounter > speedUpInterval)
		{
			player1Speed += player1Acceleration;
			player2Speed += player2Acceleration;
			timeCounter = 0;
		}

		timeCounter += Time.deltaTime;
	}

}
