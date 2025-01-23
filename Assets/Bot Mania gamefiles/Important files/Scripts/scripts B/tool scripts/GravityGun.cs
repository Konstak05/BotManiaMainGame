using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GravityGun : MonoBehaviour
{
    //ControlInputs
    public PlayerInput PlayerInputStarter;
    public InputAction FireAction;
    public InputAction RightAction;
    public bool FireKey;
    public bool RightClickKey;

    public Gunscript Gunscript;
    public ToggleUI ToggleUI;
    public GameObject crosshair1, crosshair2;
    private Image canvasImage1;
    private Image canvasImage2;
    public GameObject Object1;
    public AudioClip ChangeSound;
    public AudioSource Sound;
    public float volume;
    private Renderer objectrender;
    public GameObject[] noGravityObjects;
    public int InvertedMode;

    void Start(){
        FireAction = PlayerInputStarter.actions["Fire"];
        RightAction = PlayerInputStarter.actions["Use"];
        InvertedMode = 0;
        crosshair1 = GameObject.Find("Crosshair 1");
        crosshair2 = GameObject.Find("Crosshair 2");
        canvasImage1 = crosshair1.GetComponent<Image>();
        canvasImage2 = crosshair2.GetComponent<Image>();
        canvasImage1.enabled = true;
        canvasImage2.enabled = false;
        Renderer objectrender = Object1.GetComponent<Renderer>();

        for (int i = 0; i < noGravityObjects.Length; i++){noGravityObjects[i].SetActive(false);}
        if(InvertedMode == 0){objectrender.material.color = Color.red;}
        else{objectrender.material.color = Color.green;}
    }



    void Update(){
        FireKey = FireAction.IsPressed();
        RightClickKey = RightAction.IsPressed();
        if (ToggleUI.PauseMenu == 0 & Gunscript.GunEquipped == 4){
            canvasImage1.enabled = true;
            canvasImage2.enabled = false;
        }
        else if (ToggleUI.PauseMenu == 1 & Gunscript.GunEquipped == 4){
            canvasImage1.enabled = true;
            canvasImage2.enabled = false;
        }
        if(FireKey && !RightClickKey && ToggleUI.PauseMenu == 0 && Gunscript.GunEquipped == 4 && InvertedMode == 0){
            InvertedMode = 1;
            Renderer objectrender = Object1.GetComponent<Renderer>();
            objectrender.material.color = Color.green;          
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(ChangeSound);

            for (int i = 0; i < noGravityObjects.Length; i++){noGravityObjects[i].SetActive(true);}
        }
        if(RightClickKey && !FireKey && ToggleUI.PauseMenu == 0 && Gunscript.GunEquipped == 4 && InvertedMode == 1){
            InvertedMode = 0;
            Renderer objectrender = Object1.GetComponent<Renderer>();
            objectrender.material.color = Color.red;            
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(ChangeSound);

            for (int i = 0; i < noGravityObjects.Length; i++){noGravityObjects[i].SetActive(false);}
        }
    }
}