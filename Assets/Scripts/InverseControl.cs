using UnityEngine;

public class InverseControl : MonoBehaviour {

	public float timeOfInverseControlOfPlayer2 = 3.0f;
	private float inverseControlTimeCountDown;
	private bool isTriggered = false;
	public GameObject inverseControl;
	public Sprite inverseControlSprite;
    public Material inversedControlMaterial;
    public Material standardMaterial;

    void Update () {
		
		if (isTriggered) {
			if (Player2Controller.p2GamePad) {
				if ((Input.GetKeyDown ("joystick 2 button 5") || Input.GetKeyDown("joystick 2 button 7")) && !Player1Controller.inverseControlUsed) {
                    FindObjectOfType<AudioManager>().Play("inverseControl");
					P2ItemIcon.iconColor = Color.red;
					Player1Controller.inverseControl = true;
					P2ItemCountDown.started = true;
					Player1Controller.inverseControlUsed = true;
					P2ItemCountDown.itemText = "Inverse Control Used\n" + "Time Remaining: ";
				}
				if (Player1Controller.inverseControl) {
				    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = inversedControlMaterial; 
					P2ItemCountDown.itemTimeRemaining = inverseControlTimeCountDown;
					inverseControlTimeCountDown -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("evilTimeBar").SendMessage("SubTime", inverseControlTimeCountDown);

                    if (inverseControlTimeCountDown < 0) {
						P2ItemIcon.iconColor = Color.white;
						P2ItemIcon.itemSprite = null;
						P2ItemCountDown.started = false;
						Player1Controller.inverseControlUsed = false;
						Player1Controller.inverseControl = false;
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						StaticOptions.p2SpawnItems.Remove (inverseControl);
					    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
						Destroy (inverseControl);
                    }
				}
			} else if (!Player2Controller.p2GamePad) {
				if (Input.GetKeyDown (KeyCode.Alpha9) && !Player1Controller.inverseControlUsed) {
                    FindObjectOfType<AudioManager>().Play("inverseControl");
					P2ItemIcon.iconColor = Color.red;
					Player1Controller.inverseControl = true;
					P2ItemCountDown.started = true;
					Player1Controller.inverseControlUsed = true;
					P2ItemCountDown.itemText = "Inverse Control Used\n" + "Time Remaining: ";
				}

				if (Player1Controller.inverseControl) {
				    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = inversedControlMaterial;
					P2ItemCountDown.itemTimeRemaining = inverseControlTimeCountDown;
					inverseControlTimeCountDown -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("evilTimeBar").SendMessage("SubTime", inverseControlTimeCountDown);

                    if (inverseControlTimeCountDown < 0) {
						P2ItemIcon.iconColor = Color.white;
						P2ItemIcon.itemSprite = null;
						P2ItemCountDown.started = false;
						Player1Controller.inverseControlUsed = false;
						Player1Controller.inverseControl = false;
						P2ItemCountDown.itemText = "No item";
						isTriggered = false;
						StaticOptions.p2SpawnItems.Remove (inverseControl);
					    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
						Destroy (inverseControl);
                    }
				}
			}
		}
	}

	void LateUpdate() {
		if (!StaticOptions.p2SpawnItems.Exists (x => x == inverseControl)) {
			Destroy (inverseControl);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			StaticOptions.p2SpawnItems.Remove (inverseControl);
			Destroy (inverseControl);
		}
	}

	void OnTriggerEnter(Collider col) {
		
		if (col.gameObject.tag == "block") {
			
            FindObjectOfType<AudioManager>().Play("godGetItem");
			if (P2ItemCountDown.itemText != "No item") {
                GameObject.FindGameObjectWithTag("evilTimeBar").SendMessage("SubTime", 0f);

                P2ItemIcon.iconColor = Color.white;
				P2ItemIcon.itemSprite = null;
				P2ItemCountDown.started = false;
				Player1Controller.inverseControlUsed = false;
				Player1Controller.inverseControl = false;
				P2ItemCountDown.itemText = "No item";
				isTriggered = false;
				StaticOptions.p2SpawnItems.Remove (GameObject.FindGameObjectWithTag("p2TakenItem"));
				GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SkinnedMeshRenderer>().material = standardMaterial;
				Destroy (GameObject.FindGameObjectWithTag("p2TakenItem"));
			}
			inverseControlTimeCountDown = timeOfInverseControlOfPlayer2;
			inverseControl.tag = "p2TakenItem";
			isTriggered = true;
			if (Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Inverse Control\n" + "Press L2 to use";
			} else if (!Player2Controller.p2GamePad) {
				P2ItemCountDown.itemText = "Inverse Control\n" + "Press 9 to use";
			}
			P2ItemIcon.itemSprite = inverseControlSprite;
			inverseControl.GetComponent<SphereCollider> ().enabled = false;
			for (int i = 0; i < inverseControl.transform.childCount; i++) {
				inverseControl.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
}