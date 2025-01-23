using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterMarkButtonColor : MonoBehaviour
{
    public Color ColorIn;
    public Color ColorOut;
    public GameObject Button;
    public Renderer ButtonRenderer;
    public GameObject Bot;
    public Transform Spawner;
    public AudioSource BoopButton;

    void Start(){
    ButtonRenderer = gameObject.GetComponent<Renderer>();
    }


    public void OnMouseEnter()
    {
    
        ButtonRenderer.material.color = ColorIn;
    }

    public void OnMouseExit()
    {
        ButtonRenderer.material.color = ColorOut;
    }

    public void OnMouseDown()
    {
        Debug.Log("Test");
        Instantiate(Bot, Spawner.position, Spawner.rotation);
        BoopButton.Play();
    }
}
