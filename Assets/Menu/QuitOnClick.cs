using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClick : MonoBehaviour {

	public void Quit() {
        StaticOptions.load = 0;
        StaticOptions.load1 = 0;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
