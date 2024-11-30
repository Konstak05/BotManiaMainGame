using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEyes : MonoBehaviour
{
    public GameObject Eye1;
    public GameObject Eye2;


    void Update()
    {
        if(PlayerPrefs.GetInt("GuardingBot") == 1 | PlayerPrefs.GetInt("PauseMenu", 0) == 1){
            Eye1.SetActive(false);
            Eye2.SetActive(false);
        }
        else{
            Eye1.SetActive(true);
            Eye2.SetActive(true);            
        }
    }
}
