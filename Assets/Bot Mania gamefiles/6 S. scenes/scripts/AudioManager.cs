using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip mySoundClip;
    public AudioClip mySoundClip2;
    public float volume;


    public void PlaySound()
    {
            volume = PlayerPrefs.GetFloat("AudioVolume") * PlayerPrefs.GetFloat("MasterVolume");
            AudioSource.PlayClipAtPoint(mySoundClip, Camera.main.transform.position, volume);   
    }

    public void PlaySound2()
    {
            volume = PlayerPrefs.GetFloat("AudioVolume") * PlayerPrefs.GetFloat("MasterVolume");
            AudioSource.PlayClipAtPoint(mySoundClip2, Camera.main.transform.position, volume);   
    }
}
