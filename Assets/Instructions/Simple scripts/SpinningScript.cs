using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningScript : MonoBehaviour
{

    public float Speed = 0.1f;
    public float Speedx2 = 0f;
    public float Speedz2 = 0f;
    public int WhatWay;
    void Start()
    {
    
    if(WhatWay == 0){
    Invoke("Spinning", 0.01f);
    }
    if(WhatWay == 1){
    Invoke("Spinning2", 0.01f);
    }
    }


    void Spinning()
    {
        transform.eulerAngles = transform.eulerAngles + new Vector3(Speedx2,Speed,Speedz2);
        Invoke("Spinning", 0.02f);
    }
    void Spinning2()
    {
        transform.Rotate(Speedx2, Speed, Speedz2);
        Invoke("Spinning2", 0.02f);
    }
}
