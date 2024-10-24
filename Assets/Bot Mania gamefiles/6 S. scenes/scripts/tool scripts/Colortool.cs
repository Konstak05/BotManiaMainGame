using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colortool : MonoBehaviour
{
    public Color[] colors;
    public GravityGun GravityGun;
    public ToggleUI ToggleUI;
    public Gunscript Gunscript;
    public Renderer rend;
    private GameObject Object;
    private int Value = 1;
    public AudioClip ChangeSound;
    public AudioSource Sound;
    public AudioClip mySoundClip2;
    public float volume;
    public Renderer Renderer2;

    private Camera mainCamera;

    private void Awake()
    {
        rend.material.color = colors[Value-1];
        mainCamera = Camera.main;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(1) && Gunscript.GunEquipped == 5 && ToggleUI.PauseMenu != 1)
        {   
            if(GravityGun.InvertedMode == 1){
            RaycastHit hit;
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Objects") | LayerMask.GetMask("ObjectsColor")))
            {
                Object = hit.collider.gameObject;
                Renderer2 = Object.GetComponent<Renderer>();
                Renderer2.material.color = Color.black;
                rend.material.color = Color.black;
                
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume;
                Sound.PlayOneShot(ChangeSound);
                
                Object = null;
            }
            }
            else{
                Value = (Value == colors.Length) ? 1 : Value + 1;
                rend.material.color = colors[Value-1];

                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume * 0.5f;
                Sound.PlayOneShot(mySoundClip2);
            }
        
        }
        else if (Input.GetMouseButtonDown(0))
        {   
            RaycastHit hit;
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Objects") | LayerMask.GetMask("ObjectsColor")) && ToggleUI.PauseMenu != 1)
            {
                Object = hit.collider.gameObject;
                Renderer2 = Object.GetComponent<Renderer>();
                Renderer2.material.color = (GravityGun.InvertedMode == 1) ? Color.white : colors[Value-1];
                rend.material.color = (GravityGun.InvertedMode == 1) ? Color.white : colors[Value-1];
                
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume;
                Sound.PlayOneShot(ChangeSound);

                Object = null;
            }
        }
    }
}
