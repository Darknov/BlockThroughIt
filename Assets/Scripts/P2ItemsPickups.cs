using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2ItemsPickups : MonoBehaviour {

	public int numbersOfSpawnItems = 3;
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

		StaticOptions.spawnItems = new List<GameObject> ();
		StaticOptions.maxP2ItmesSpawn = numbersOfSpawnItems;
		rotation = new Quaternion();
		rowLength = GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().rowLength;
		InvokeRepeating ("Step", firstPickupSpawnTime, pickupSpawnTime);
		P2ItemIcon.itemSprite = null;
		P2ItemCountDown.itemText = "No item";
		Destroy (GameObject.FindGameObjectWithTag("p2TakenItem"));
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
				Vector3 pickupPlace = new Vector3((float)(i-System.Math.Floor(rowLength/2.0)), 0, (float)(j-System.Math.Floor(rowLength/2.0)));
				if (GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().blocks [i, j] != null) {
					StaticOptions.spawnItems.RemoveAll (x => x.transform.position == pickupPlace);
				}
			}
		}
	}

	void possiblePlacesList() {

		for (int i = 0; i < rowLength; i++) {
			for (int j = 0; j < rowLength; j++) {
				Vector3 pickupPlace = new Vector3((float)(i-System.Math.Floor(rowLength/2.0)), 0, (float)(j-System.Math.Floor(rowLength/2.0)));
				if (GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().blocks [i, j] == null && !StaticOptions.spawnItems.Exists(x => x.transform.position == pickupPlace)) {
					possiblePlaces.Add(pickupPlace);
				}
			}
		}
	}

	void createRandomItem(Vector3 position) {

		if (StaticOptions.spawnItems.Count < StaticOptions.maxP2ItmesSpawn) {
			int randomIndex = randomItems.Next (0, possibleItems.Count);
			GameObject item = Instantiate (possibleItems [randomIndex], position, rotation);
			StaticOptions.spawnItems.Add (item);
		}
	}

	public Vector3 createRandomPlace() {

		int randomIndex = randomPlaces.Next(0, possiblePlaces.Count);
		return possiblePlaces[randomIndex];
	}

	void OnTriggerEnter(Collider col) {

		if(col.gameObject.tag == "platform") {
			Destroy (gameObject);
		}
	}
}