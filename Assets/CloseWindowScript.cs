using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CloseWindowScript : MonoBehaviour
{
    
    public bool StartClosed = false;
    public GameObject CloseObject;

    void Start()
    {
        if(StartClosed == true){
          ClosingObject();
        }
        else{
          OpeningObject();
        }
    }

    public void OpeningObject()
    {
        CloseObject.SetActive(true);
    }

    public void ClosingObject()
    {
        CloseObject.SetActive(false);
    }
}