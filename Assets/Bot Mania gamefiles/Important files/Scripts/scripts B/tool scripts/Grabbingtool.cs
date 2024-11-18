using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbingtool : MonoBehaviour
{
    public GameObject GunSpin;
    public objPickup2 objPickup2;
    //public GameObject SecondObject;
    public float SpinFaster = 0f;
    public float SpinSpeed = 5f;
    public float ScaleFactor = 0.5f;
    public float MinTransparency = 0.1f;
    public float MaxTransparency = 0.5f;
    public Vector3 Object2SpawnMax;
    public Color objColor;

    void Start()
    {
        //Object2SpawnMax = SecondObject.transform.localScale;
        //objColor = SecondObject.GetComponent<Renderer>().material.color;
    } 


    void FixedUpdate()
    {
        if (objPickup2.Grabbing == 1)
        {
            SpinFaster = Mathf.Min(SpinFaster + Time.fixedDeltaTime * SpinSpeed, 15f);
        }
        else
        {
            SpinFaster = Mathf.Max(SpinFaster - Time.fixedDeltaTime * SpinSpeed, 0f);
        }

        GunSpin.transform.Rotate(0f, 0f, SpinFaster);
        //SecondObject.transform.localScale = Object2SpawnMax * SpinFaster / 15;
        objColor.a = Mathf.Lerp(MinTransparency, MaxTransparency, SpinFaster / 15);
        //SecondObject.GetComponent<Renderer>().material.color = objColor;
    }
}