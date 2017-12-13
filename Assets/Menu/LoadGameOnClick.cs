using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex)
	{
        StaticOptions.mode = sceneIndex;
		SceneManager.LoadScene (1);

	}
}
