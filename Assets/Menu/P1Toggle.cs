using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Toggle : MonoBehaviour {

	public void ToggleChange(bool newValue) {
		Player1Controller.p1KeyBoard = newValue;
	}
}