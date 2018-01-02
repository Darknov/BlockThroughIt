using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour {

    public Transform canvasObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        if (canvasObject.gameObject.activeInHierarchy == true)
        {
            canvasObject.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
