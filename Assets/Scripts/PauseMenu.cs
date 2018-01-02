using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public Transform canvasObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (canvasObject.gameObject.activeInHierarchy == false)
        {
            canvasObject.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
