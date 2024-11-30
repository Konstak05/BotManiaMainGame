using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{

    public Slider BounceMul;
    public Slider BounceMul2;
    public float DashMeter;
    public KeyboardControlMk2 Script1;
    public GameObject X;
    public GameObject Tick;
    public GameObject MidSign,DownSign,UpSign;

    void Update()
    {
        BounceMul.value = Script1.CanDash;

        BounceMul2.value = Script1.velocity.y;

        if(Script1.velocity.y > -32f && Script1.velocity.y < 20f){
          MidSign.SetActive(true);
          DownSign.SetActive(false);
          UpSign.SetActive(false);
        }
        if(Script1.velocity.y < -32f){
          MidSign.SetActive(false);
          DownSign.SetActive(true);
          UpSign.SetActive(false);   
        }
        if(Script1.velocity.y > 20f){
          MidSign.SetActive(false);
          DownSign.SetActive(false);
          UpSign.SetActive(true);   
        }

        if(Script1.CanDash < 100f){
        X.SetActive(true);
        Tick.SetActive(false);
        }
        else{
        X.SetActive(false);
        Tick.SetActive(true);  
        }
    }

}
