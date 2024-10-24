using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapchangerteleporter : MonoBehaviour
{
    public Animator transition;
    private bool HasTeleported = false;
    public string SceneName;

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
    Invoke("StartGame2", 1);
        
    }

    void StartGame2()
    {
    SceneManager.LoadScene(SceneName);

    }
}
