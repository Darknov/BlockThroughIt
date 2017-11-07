using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Toggle : MonoBehaviour {

	public void ToggleChange(bool newValue) {
		Player2Controller.p2GamePad = newValue;
	}
}
