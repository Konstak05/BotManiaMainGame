using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windowlightsswitch : MonoBehaviour
{
    public Color Color1;
    public Color Color2;
    private Renderer Renderer;
    public float Max;
    public float Min;

    void Start()
    {
    Renderer = gameObject.GetComponent<Renderer>();
    Renderer.material.color = Color1;
    Invoke("WindowClosing",UnityEngine.Random.Range(Min, Max));
    }

  
    void WindowClosing()
    {
        Renderer.material.color = Color1;
        Invoke("WindowOpening",UnityEngine.Random.Range(Min, Max));
    }

    void WindowOpening()
    {
        Renderer.material.color = Color2;
        Invoke("WindowClosing",UnityEngine.Random.Range(Min, Max));
    }
}
