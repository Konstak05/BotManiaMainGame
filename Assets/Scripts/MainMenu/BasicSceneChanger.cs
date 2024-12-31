using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BasicSceneChanger : MonoBehaviour
{

    public Animator transition;
    public string SceneName;
    public int HasStarted;


    public void IsPressed()
    {
        transition.SetTrigger("Start"); 
        Invoke("MoveScene", 1);
        HasStarted = 1;
    }

    void MoveScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
