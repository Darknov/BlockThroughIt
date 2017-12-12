using UnityEngine;
using UnityEngine.UI;

public class VolumeControll : MonoBehaviour {

    public static float vol = 0.5f;
    public Slider volume;

    void Start () {
       volume.value = vol;
    }

    private void Update()
    {
        AudioManager.instance.ChangeVolume(volume.value);
    }
}
