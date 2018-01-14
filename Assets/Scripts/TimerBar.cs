using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

    public Image currentTime;

    void Update()
    {
        currentTime.rectTransform.localScale = new Vector3(CountDown.timeRemaining / 60, 1, 1);
    }
}
