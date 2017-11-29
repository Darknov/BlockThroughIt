using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2ItemsPickups : MonoBehaviour {

	public Transform[] fromBlocks;
	int rowLength;
	public List<GameObject> possibleItems = new List<GameObject>();
	private System.Random randomItems = new System.Random();
	private List<Transform> possiblePlaces;
	private System.Random randomPlaces;

	void Start() {
		rowLength = GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().rowLength;
		fromBlocks = new Transform[rowLength * rowLength];
		//possibleItems = new List<GameObject>();
		//randomItems = new System.Random();
		possiblePlaces = new List<Transform>();
		randomPlaces = new System.Random();
		//Transform place = createRandomPlace ();
		//GameObject item = createRandomItem (place);
	}

	void Update() {
		
		for (int i = 0; i < rowLength; i++) {
			for (int j = 0; j < rowLength; j++) {
				if (GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().blocks [i, j] != null) {
					possiblePlaces.Add(GameObject.FindGameObjectWithTag ("platformBoard").GetComponent<PlatformBoard> ().blocks [i, j].transform);
				}
			}
		}
	}

	void LateUpdate() {
		
		Transform place = createRandomPlace ();
		GameObject item = createRandomItem (place);
	}

	public GameObject createRandomItem(Transform place) {
		
		int randomIndex = randomItems.Next(0, possibleItems.Count-1);
		GameObject item = Instantiate(possibleItems[randomIndex], place).GetComponent<GameObject>();
		return item;
	}

	public Transform createRandomPlace() {
		
		int randomIndex = randomPlaces.Next(0, possiblePlaces.Count-1);
		return possiblePlaces[randomIndex];
	}
}
