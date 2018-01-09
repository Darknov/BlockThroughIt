using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1ItemsPickups : MonoBehaviour {

	public int numbersOfSpawnItems = 3;
	public float firstPickupSpawnTime = 0f;
	public float pickupSpawnTime = 10f;
	int rowLength;
	public List<GameObject> possibleItems = new List<GameObject>();
	private System.Random randomItems = new System.Random();
	private Quaternion rotation;
	private List<Vector3> possiblePlaces;
	private System.Random randomPlaces;
    public GameObject partEffect;


    void Start() {

		StaticOptions.p1SpawnItems = new List<GameObject> ();
		StaticOptions.maxP1ItmesSpawn = numbersOfSpawnItems;
		rotation = new Quaternion();
		rowLength = GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().rowLength;
		InvokeRepeating ("Step", firstPickupSpawnTime, pickupSpawnTime);
		P1ItemIcon.itemSprite = null;
		P1ItemCountDown.itemText = "No item";
		Destroy (GameObject.FindGameObjectWithTag("p1TakenItem"));
	}

	void Step() {

		randomPlaces = new System.Random ();
		possiblePlaces = new List<Vector3> ();
		possiblePlacesList ();
		Vector3 place = createRandomPlace ();
		createRandomItem (place);
		possiblePlaces.Clear ();
	}

	void Update() {

		for (int i = 0; i < rowLength; i++) {
			for (int j = 0; j < rowLength; j++) {
				Vector3 pickupPlace = new Vector3((float)(i-System.Math.Floor(rowLength/2.0)), 1, (float)(j-System.Math.Floor(rowLength/2.0)));
				if (GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().blocks [i, j] == null) {
					StaticOptions.p1SpawnItems.RemoveAll (x => x.transform.position == pickupPlace);
				}
			}
		}
	}

	void possiblePlacesList() {

		for (int i = 0; i < rowLength; i++) {
			for (int j = 0; j < rowLength; j++) {
				Vector3 pickupPlace = new Vector3((float)(i-System.Math.Floor(rowLength/2.0)), 1, (float)(j-System.Math.Floor(rowLength/2.0)));
				if (GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().blocks [i, j] != null &&
						!StaticOptions.p1SpawnItems.Exists(x => x.transform.position == pickupPlace &&
						GameObject.FindWithTag ("Player").GetComponent<Transform>().position.x != (float)(i-System.Math.Floor(rowLength/2.0)) &&
						GameObject.FindWithTag ("Player").GetComponent<Transform>().position.z != (float)(j-System.Math.Floor(rowLength/2.0))
					)) {
					possiblePlaces.Add(pickupPlace);
				}
			}
		}
	}

	void createRandomItem(Vector3 position) {

		if (StaticOptions.p1SpawnItems.Count < StaticOptions.maxP1ItmesSpawn) {
			int randomIndex = randomItems.Next (0, possibleItems.Count);
			GameObject item = Instantiate (possibleItems [randomIndex], position, rotation);
			StaticOptions.p1SpawnItems.Add (item);
            Instantiate(partEffect, position, rotation);

        }
    }

	public Vector3 createRandomPlace() {

		int randomIndex = randomPlaces.Next(0, possiblePlaces.Count);
		return possiblePlaces[randomIndex];
	}
}