using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{

    public Animator transition;


    public void Exit()
    {
    transition.SetTrigger("Start"); 
    Invoke("StartGame2", 1);
    }       


    void StartGame2()
    {
    Application.Quit();
    }
}
