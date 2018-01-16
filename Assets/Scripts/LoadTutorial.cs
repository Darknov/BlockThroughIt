using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTutorial : MonoBehaviour {

    public void loadTutorial()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
