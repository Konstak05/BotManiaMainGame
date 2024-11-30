using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundWithVolume : MonoBehaviour
{
    public AudioSource AudioSource;

    void Start()
    {
                float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                AudioSource.volume = musicVolume * masterVolume;
                AudioSource.Play();
    }
}
