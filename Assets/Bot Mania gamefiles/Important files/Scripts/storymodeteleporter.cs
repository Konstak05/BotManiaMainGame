using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class storymodeteleporter : MonoBehaviour
{
    public Animator transition;
    private bool HasTeleported = false;
    public string SceneName;
    public string Playerprefname;
    public int Playerprefvalue;

    void Start(){
        transition = GameObject.Find("Fading loader").GetComponent<Animator>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !HasTeleported)
        {

            HasTeleported = true;
            Invoke("StartGame",0f);
        }
    }

    void StartGame()
    {
    transition.SetTrigger("Start"); 
    PlayerPrefs.SetInt(Playerprefname,Playerprefvalue);
    Invoke("StartGame2", 2);
        
    }

    void StartGame2()
    {
    Cursor.lockState = CursorLockMode.None;
    SceneManager.LoadScene(SceneName);

    }
}
