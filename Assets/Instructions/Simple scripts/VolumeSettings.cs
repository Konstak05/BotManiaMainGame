using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public Slider audioSlider;
    public Slider musicSlider;
    public Slider masterSlider;
    public AudioSource ButtonSound;
    public AudioClip Hoversound;
    public AudioClip clicksound;

    private void Start()
    {
        audioSlider.value = PlayerPrefs.GetFloat("AudioVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }
    private void OnDestroy()
    {
    PlayerPrefs.SetFloat("AudioVolume", audioSlider.value);
    PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }
    public void ToMainMenu()
    {
    PlayerPrefs.SetFloat("AudioVolume", audioSlider.value);
    PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }
    public void SetAudioLevel(float volume)
    {
       PlayerPrefs.SetFloat("AudioVolume", audioSlider.value);
    }

    public void SetMusicLevel(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetMasterLevel(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

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
                ButtonSound.PlayOneShot(clicksound);
    }
}