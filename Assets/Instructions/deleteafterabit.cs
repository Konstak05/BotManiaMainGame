using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteafterabit : MonoBehaviour
{
    public float DeleteTimer;
   
    void Start()
    {
        Invoke("Delete",DeleteTimer);
    }

    // Update is called once per frame
    void Delete()
    {
        Destroy(gameObject);
    }
}
