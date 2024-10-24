using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialFinishTeleporter : MonoBehaviour
{
    public Animator transition;
    private bool HasTeleported = false;
    public string SceneName;

    void Start(){
        transition = GameObject.Find("Fading loader2").GetComponent<Animator>();

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
    PlayerPrefs.SetInt("FirstTime",1);
    Invoke("StartGame2", 4);
        
    }

    void StartGame2()
    {
    Cursor.lockState = CursorLockMode.None;
    SceneManager.LoadScene(SceneName);

    }
}
