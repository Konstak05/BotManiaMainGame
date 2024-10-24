using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettingsmk2 : MonoBehaviour
{
    public Slider audioSlider;
    public Slider musicSlider;
    public Slider masterSlider;

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
    
    public void SetAudioLevel(){
        PlayerPrefs.SetFloat("AudioVolume", audioSlider.value);
    }

    public void SetMusicLevel(){
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetMasterLevel(){ 
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }
}