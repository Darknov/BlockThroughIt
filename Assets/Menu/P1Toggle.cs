using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1Toggle : MonoBehaviour {
  /*  Toggle toggleKey;
    Toggle togglePad;*/
    private void Start()
    {/*
        toggleKey = GameObject.FindGameObjectWithTag("p1togglePM").GetComponent<Toggle>();
        togglePad = GameObject.FindGameObjectWithTag("p1togglepad").GetComponent<Toggle>();
        if (PlayerPrefs.GetInt("loaded") == 1)
        {         
            if (PlayerPrefs.GetInt("Keyboard") == 1)
            {
                toggleKey.isOn = true;
                togglePad.isOn = false;
                Player1Controller.p1KeyBoard = true;
            }
            else
            {
                toggleKey.isOn = false;
                togglePad.isOn = true;
                Player1Controller.p1KeyBoard = false;
            }
        }*/
    }
    public void ToggleChange(bool newValue) {

        if (newValue)
        {
            PlayerPrefs.SetInt("isKeyboard", 1);
        }      
        else
        {
            PlayerPrefs.SetInt("isKeyboard", 0);        
        }
        Player1Controller.p1KeyBoard = newValue;
      //  StaticOptions.load = 1;
    }
}