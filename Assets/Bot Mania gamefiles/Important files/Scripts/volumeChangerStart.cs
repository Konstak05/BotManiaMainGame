using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeChangerStart : MonoBehaviour
{
    public AudioSource ButtonSound;
    public AudioClip Hoversound;
    public AudioClip PressSound;

    public void HoverSoundPlay()
    {
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                ButtonSound.volume = audioVolume * masterVolume;
                ButtonSound.PlayOneShot(Hoversound);
    }

    public void ClickPlay()
    {
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                ButtonSound.volume = audioVolume * masterVolume;
                ButtonSound.PlayOneShot(PressSound);
    }
}
