using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Musicplay : MonoBehaviour
{
    public AudioSource audiosource;
    private float audiovolume = 1f;
    public Slider volumeslide;
    void Start()
    {
        audiosource.Play();
        audiovolume = PlayerPrefs.GetFloat("MVolume");
        audiosource.volume = audiovolume;
        volumeslide.value = audiovolume;
    }

    void Update()
    {
        audiosource.volume = audiovolume;
        PlayerPrefs.SetFloat("MVolume", audiovolume);
    }
    public void setvolume(float volume) 
    {
        audiovolume = volume;
    }
}
