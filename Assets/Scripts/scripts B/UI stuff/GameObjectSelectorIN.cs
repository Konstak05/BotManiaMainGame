using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameObjectSelectorIN : MonoBehaviour
{
    public int Index;
    public TextMeshProUGUI MapText;
    public string[] MapNames;
    public string PlayerPrefID;
    public int PlayerprefIDindex;
    public float Soundreset = 2f;
    public AudioClip Clip;
    public AudioSource Sound;

    void Start()
    {
        Index = PlayerPrefs.GetInt(PlayerPrefID);
        PlayerprefIDindex = PlayerPrefs.GetInt(PlayerPrefID);

        for (int j = 0; j < MapNames.Length; j++)
        {
            if (j == Index)
            {
                MapText.text = MapNames[j];
                PlayerprefIDindex = Index;
            }
        }   
    }

    public void Update()
    {
      if(Soundreset < 2f){
         Soundreset += 0.1f;
      }
    }

    private void OnDestroy()
    {
    PlayerPrefs.SetInt(PlayerPrefID, PlayerprefIDindex);
    }

    public void LeftButtonFunction()
    {
        if(Soundreset >= 2f){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip);
        Soundreset = 0f;
        }

        if(Index > 0){
        Index = Index - 1;
        }
        else{
        Index = MapNames.Length - 1;
        }

        for (int j = 0; j < MapNames.Length; j++)
        {
            if (j == Index)
            {
                MapText.text = MapNames[j];
                PlayerprefIDindex = Index;
            }
        }
    }

    public void RightButtonFunction()
    {
        if(Soundreset >= 2f){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip);
        Soundreset = 0f;
        }


        if(Index == MapNames.Length - 1){
        Index = 0;
        }
        else{
        Index += 1;
        }


        for (int j = 0; j < MapNames.Length; j++)
        {
            if (j == Index)
            {
                MapText.text = MapNames[j];
                PlayerprefIDindex = Index;
            }
        }
    }
}
