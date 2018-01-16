using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeBar : MonoBehaviour {
    public Image currentTime;
    public Text tekst;

    private float hitpoint = 0;
    public float maxhitpoint = 5;


    // Use this for initialization
    private void Start () {
       UpdateBar();
        
    }

    private void UpdateBar()
    {
        float ratio = hitpoint / maxhitpoint;
        currentTime.rectTransform.localScale = new Vector3(ratio, 1, 1);
        //tekst.text = (ratio*5).ToString() +  " s";

    }

    private void SubTime(float time)
    {
        UpdateBar();

        hitpoint = time;
        if(time < 0 )
        {
            hitpoint = 0;
        }
        UpdateBar();
    }
}
