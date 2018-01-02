using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("backgroundMusic");
    }

    public void ChangeVolumeMaster(float s)
    {
        for (int i = 0; i < 12; i++)
        {
            sounds[i].source.volume = s;
        }
        VolumeControll.volMaster = sounds[11].source.volume;
    }

    public void ChangeVolumeBackground(float s)
    {
        sounds[11].source.volume = s;
        VolumeControll.volBackground = sounds[11].source.volume;
    }
    public void ChangeVolumeEffects(float s)
    {
        for (int i = 0; i <= 10; i++)
        {
            sounds[i].source.volume = s;
        }
        VolumeControll.volEffects = sounds[9].source.volume;
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
