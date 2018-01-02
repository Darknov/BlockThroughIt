using UnityEngine;
using UnityEngine.UI;

public class VolumeControll : MonoBehaviour {


    public static float volBackground = 0.5f;
    public static float volEffects = 0.5f;
    public static float volMaster = 0.5f;
    public Slider volumeBackground;
    public Slider volumeEffects;
    public Slider volumeMaster;

    void Start()
    {
        volumeBackground.value = volBackground;
        volumeEffects.value = volEffects;
        volumeMaster.value = volMaster;
        volumeBackground.onValueChanged.AddListener(delegate { ValueChangeBackgroundCheck(); });
        volumeEffects.onValueChanged.AddListener(delegate { ValueChangeEffectsCheck(); });
        volumeMaster.onValueChanged.AddListener(delegate { ValueChangeMasterCheck(); });
    }

    private void ValueChangeMasterCheck()
    {
        PlayerPrefs.SetInt("whichSlider", 1);
    }

    private void ValueChangeEffectsCheck()
    {
        PlayerPrefs.SetInt("whichSlider", 2);
    }

    private void ValueChangeBackgroundCheck()
    {
        PlayerPrefs.SetInt("whichSlider", 3);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("whichSlider") == 3)
        {
            AudioManager.instance.ChangeVolumeBackground(volumeBackground.value);
            StaticOptions.AudioVolumeBackground = volBackground;
        }
        if (PlayerPrefs.GetInt("whichSlider") == 2)
        {
            AudioManager.instance.ChangeVolumeEffects(volumeEffects.value);
            StaticOptions.AudioVolumeEffects = volEffects;
        }
        if (PlayerPrefs.GetInt("whichSlider") == 1)
        {
            AudioManager.instance.ChangeVolumeMaster(volumeMaster.value);
            StaticOptions.AudioVolumeMaster = volMaster;
        }
    }
}
