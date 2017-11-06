using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Toggle : MonoBehaviour {

	public void ToggleChange(bool newValue) {
		AutoMovement.p1KeyBoard = newValue;
	}
}