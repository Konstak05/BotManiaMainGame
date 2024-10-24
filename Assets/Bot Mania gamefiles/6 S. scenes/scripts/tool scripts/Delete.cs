using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Delete : MonoBehaviour

{
    public GameObject Player;
    public GravityGun GravityGun;
    public ToggleUI ToggleUI;
    public Gunscript Gunscript;
    public GameObject crosshair1, crosshair2;
    private Image canvasImage1;
    private Image canvasImage2;
    public int MaxRange;
    private Camera mainCamera;
    public GameObject Object;
    public AudioClip Deletingsound;
    public AudioSource soundSource;
    public bool AlreadyChanged;

    void Start()
    {
     canvasImage1 = crosshair1.GetComponent<Image>();
     canvasImage2 = crosshair2.GetComponent<Image>();
     canvasImage1.enabled = true;
     canvasImage2.enabled = false;
     mainCamera = Camera.main;
     Object = null;
    }


    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
      

        if (Physics.Raycast(ray, out hit, MaxRange, LayerMask.GetMask("Objects")) && ToggleUI.PauseMenu == 0 && Gunscript.GunEquipped == 2)
        {
            AlreadyChanged = true;
            canvasImage1.enabled = false;
            if(GravityGun.InvertedMode == 0){
            canvasImage2.color = Color.red;
            }
            if(GravityGun.InvertedMode == 1){
            canvasImage2.color = Color.green;
            }
            canvasImage2.enabled = true;

            Object = hit.collider.gameObject;

            if (Input.GetMouseButtonDown(0))
            {   
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                float volume = audioVolume * masterVolume;
                soundSource.PlayOneShot(Deletingsound);

                if(GravityGun.InvertedMode == 0){
                    Destroy(Object);
                }
                else if(GravityGun.InvertedMode == 1){
                    GameObject newObject = Instantiate(Object, Object.transform.position, Object.transform.rotation);
                }
            }
    }
    else if (Physics.Raycast(ray, out hit, MaxRange, ~LayerMask.GetMask("Objects")) || ToggleUI.PauseMenu == 1 || Gunscript.GunEquipped != 2 && AlreadyChanged == true){
                canvasImage1.enabled = true;
                canvasImage2.enabled = false;
                AlreadyChanged = false;
                if(Object != null){Object = null;}          
    }



    }

}
