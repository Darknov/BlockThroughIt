﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2ItemsPickups : MonoBehaviour {

	public float pickupSpawnTime = 10f;
	private int blockSize = 0;
	int rowLength;
	public List<GameObject> possibleItems = new List<GameObject>();
	private System.Random randomItems = new System.Random();
	private List<Transform> possiblePlaces;
	private System.Random randomPlaces;

	void Start() {

		rowLength = GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().rowLength;
		randomPlaces = new System.Random ();
		possiblePlaces = new List<Transform>();
		InvokeRepeating ("Step", pickupSpawnTime, pickupSpawnTime);
	}

	void Step() {

		blocksSize ();
		possiblePlacesList ();
		Transform place = createRandomPlace ();
		GameObject item = createRandomItem (place);
	}

	void blocksSize() {

		for (int i = 0; i < rowLength; i++) {
			for (int j = 0; j < rowLength; j++) {
				if (GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().blocks [i, j] == null) {
					blockSize++;
				}
			}
		}
	}

	void possiblePlacesList() {

		for (int i = 0; i < rowLength; i++) {
			for (int j = 0; j < rowLength; j++) {
				if (GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().blocks [i, j] == null) {
					GameObject pickupPlace = new GameObject ();
					pickupPlace.GetComponent<Transform> ().position = new Vector3((float)(i-System.Math.Floor(rowLength/2.0)), 0, (float)(j-System.Math.Floor(rowLength/2.0)));
					possiblePlaces.Add(pickupPlace.GetComponent<Transform>());
				}
			}
		}
	}

	public GameObject createRandomItem(Transform place) {

		int randomIndex = randomItems.Next(0, possibleItems.Count);
		GameObject item = Instantiate(possibleItems[randomIndex], place).GetComponent<GameObject>();
		return item;
	}

	public Transform createRandomPlace() {

		int randomIndex = randomPlaces.Next(0, possiblePlaces.Count);
		return possiblePlaces[randomIndex];
	}
}