using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WaterMarkStart : MonoBehaviour
{
    public Animator transition;
    public string SceneName;
    public string SceneName2;
    
    void Start()
    {
     if(PlayerPrefs.GetFloat("AudioVolume", 0) == 0 && PlayerPrefs.GetFloat("MusicVolume") == 0 && PlayerPrefs.GetFloat("MasterVolume") == 0){
    PlayerPrefs.SetFloat("AudioVolume", 1);
    PlayerPrefs.SetFloat("MusicVolume", 1);
    PlayerPrefs.SetFloat("MasterVolume", 1);
     }


     Invoke("StartGame",7.5f);
    }

    // Update is called once per frame
    void StartGame()
    {
    transition.SetTrigger("Start"); 
    Invoke("StartGame2", 1);
        
    }

    void StartGame2()
    {
    if(PlayerPrefs.GetInt("FirstTime") == 1){
    SceneManager.LoadScene(SceneName);
    }
    else{
    SceneManager.LoadScene(SceneName2);
    }

    }
}
