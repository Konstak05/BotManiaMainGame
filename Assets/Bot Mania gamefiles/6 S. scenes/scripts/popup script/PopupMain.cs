using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupMain : MonoBehaviour
{
    public GameObject SignMain;
    public TextMeshPro TextName;
    public TextMeshPro TextDesc;
    public AudioSource Audio;

    void Start()
    {
    SignMain.SetActive(false);
    }

    public void ShowText(){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Audio.volume = audioVolume * masterVolume;
        Audio.Play();
        SignMain.SetActive(true);
        CancelInvoke("HideText");
        Invoke("HideText", 7f);
    }

    public void HideText(){
        SignMain.SetActive(false);

    }
}
