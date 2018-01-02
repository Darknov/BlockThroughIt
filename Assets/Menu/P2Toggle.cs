using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2Toggle : MonoBehaviour {

    Toggle toggleKey;
    Toggle togglePad;
    private void Start()
    {
        togglePad = GameObject.FindGameObjectWithTag("p2togglepad").GetComponent<Toggle>();
        toggleKey = GameObject.FindGameObjectWithTag("p2togglePM").GetComponent<Toggle>();
        if (StaticOptions.load1 == 2)
        {
            if (PlayerPrefs.GetInt("Keyboardpl2") == 1)
            {
                toggleKey.isOn = false;
                togglePad.isOn = true;
                Player2Controller.p2GamePad = true;
            }
            else
            {
                toggleKey.isOn = true;
                togglePad.isOn = false;
                Player2Controller.p2GamePad = false;
            }
        }
    }
    public void ToggleChange(bool newValue) {
        if (newValue)
        {
            PlayerPrefs.SetInt("isKeyboardpl2", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isKeyboardpl2", 0);
        }
        Player2Controller.p2GamePad = !newValue;
        StaticOptions.load1 = 2;
    }
}
