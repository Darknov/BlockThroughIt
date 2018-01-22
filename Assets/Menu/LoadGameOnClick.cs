using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameOnClick : MonoBehaviour
{ 
    public void LoadByIndex(int sceneIndex)
    {
        InverseControl.isTriggered = false;
        StaticOptions.mode = sceneIndex;
        SceneManager.LoadScene(1);
        P2ItemCountDown.started = false;
        P2ItemCountDown.itemText = "No item";
        P2ItemIcon.itemSprite = null;
	     if (CountDown.timeRemaining != 60)
        {
            CountDown.timeRemaining = 60;
        }
        CountDown.started = false;

        if (PlayerPrefs.GetInt("isKeyboard") == 1)
        {
            PlayerPrefs.SetInt("Keyboard", 1);
            Player1Controller.p1KeyBoard = true; 
        }
        else
        {
            PlayerPrefs.SetInt("Keyboard", 0);
            Player1Controller.p1KeyBoard = false;
        }

        if (PlayerPrefs.GetInt("isKeyboardpl2") == 1)
        {
            PlayerPrefs.SetInt("Keyboardpl2", 1);
            Player2Controller.p2GamePad = true;
        }
        else
        {
            PlayerPrefs.SetInt("Keyboardpl2", 0);
            Player2Controller.p2GamePad = false;
        }
    }
}