using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2ItemsPickups : MonoBehaviour {

	public float firstPickupSpawnTime = 0f;
	public float pickupSpawnTime = 10f;
	private int blockSize = 0;
	int rowLength;
	public List<GameObject> possibleItems = new List<GameObject>();
	private System.Random randomItems = new System.Random();
	private Quaternion rotation;
	private List<Vector3> possiblePlaces;
	private System.Random randomPlaces;

	void Start() {

		rotation = new Quaternion();
		rowLength = GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().rowLength;
		InvokeRepeating ("Step", firstPickupSpawnTime, pickupSpawnTime);
	}

	void Step() {

		randomPlaces = new System.Random ();
		possiblePlaces = new List<Vector3>();
		blocksSize ();
		possiblePlacesList ();
		Vector3 place = createRandomPlace ();
		createRandomItem (place);
		possiblePlaces.Clear();
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
					Vector3 pickupPlace = new Vector3((float)(i-System.Math.Floor(rowLength/2.0)), 0, (float)(j-System.Math.Floor(rowLength/2.0)));
					possiblePlaces.Add(pickupPlace);
				}
			}
		}
	}

	void createRandomItem(Vector3 position) {

		int randomIndex = randomItems.Next(0, possibleItems.Count);
		GameObject item = Instantiate(possibleItems[randomIndex], position, rotation).GetComponent<GameObject>();
	}

	public Vector3 createRandomPlace() {

		int randomIndex = randomPlaces.Next(0, possiblePlaces.Count);
		return possiblePlaces[randomIndex];
	}
}