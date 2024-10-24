using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class objPickup2 : MonoBehaviour

{
    public GameObject Player;
    public GravityGun GravityGun;
    public ToggleUI ToggleUI;
    public Gunscript Gunscript;
    public GameObject crosshair1, crosshair2;
    public float throwAmount;
    private Image canvasImage1;
    private Image canvasImage2;
    public int MaxRange;
    private Camera mainCamera;
    public GameObject Object;
    public Rigidbody ObjectR;
    public int Pickedup;
    public int Grabbing;
    void Start()
    {
     canvasImage1 = crosshair1.GetComponent<Image>();
     canvasImage2 = crosshair2.GetComponent<Image>();
     canvasImage1.enabled = true;
     canvasImage2.enabled = false;
     mainCamera = Camera.main;
     Grabbing = 0;
    }


    void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
      

        if (Physics.Raycast(ray, out hit, MaxRange, LayerMask.GetMask("Objects")) && ToggleUI.PauseMenu == 0 && Gunscript.GunEquipped == 0 && Player.GetComponent<KeyboardControlMk2>().HP > 0)
        {
            canvasImage1.enabled = false;
            canvasImage2.color = Color.blue;
            canvasImage2.enabled = true;

            if(Object != null && Object != hit.collider.gameObject){
            ObjectR = Object.GetComponent<Rigidbody>(); 
            Object.transform.parent = null; 
            Pickedup = 0;}

            Object = hit.collider.gameObject;

            if(Input.GetKeyDown(KeyCode.R)){
            ObjectR = Object.GetComponent<Rigidbody>(); 
            ObjectR.useGravity = true;
            ObjectR.isKinematic = false;}

            if (Input.GetMouseButtonDown(0))
            {ObjectR = Object.GetComponent<Rigidbody>(); 
            Object.transform.parent = mainCamera.transform; 
            Pickedup = 1; 
            Grabbing = 1;}
            else if (Input.GetMouseButtonUp(0)){
            ObjectR = Object.GetComponent<Rigidbody>();
            Object.transform.parent = null; 
            ObjectR.useGravity = true; 
            if(GravityGun.InvertedMode == 0){ObjectR.isKinematic = false;} 
            Grabbing = 0; 
            Pickedup = 0;
            }




            if (Input.GetMouseButtonDown(1))
            {
                if(GravityGun.InvertedMode == 0){
                ObjectR = Object.GetComponent<Rigidbody>();
                Vector3 throwDirection = mainCamera.transform.forward;

                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                  {
                  Pickedup = 0;
                  ObjectR.isKinematic = false;
                  throwDirection = mainCamera.transform.forward;
                  if(Object.CompareTag("Bot")){
                  ObjectR.AddForce(throwDirection * throwAmount * 0.3f, ForceMode.Impulse);
                  }
                  else{
                  ObjectR.AddForce(throwDirection * throwAmount * 3, ForceMode.Impulse);
                  }
                  }
                  else{
                  Pickedup = 0;
                  ObjectR.isKinematic = false;
                  throwDirection = mainCamera.transform.forward;
                  if(Object.CompareTag("Bot")){ObjectR.AddForce(throwDirection * throwAmount * 0.1f, ForceMode.Impulse);}
                  else {ObjectR.AddForce(throwDirection * throwAmount * 1, ForceMode.Impulse);}
                  }
                  }
                  else if(GravityGun.InvertedMode == 1){Object = hit.collider.gameObject; ObjectR = Object.GetComponent<Rigidbody>(); ObjectR.isKinematic = true;}
            }
        }
        else if (Physics.Raycast(ray, out hit, MaxRange, ~LayerMask.GetMask("Objects")) | Input.GetMouseButtonUp(0) | ToggleUI.PauseMenu == 1 | Gunscript.GunEquipped != 0 | Player.GetComponent<KeyboardControlMk2>().HP < 0){
                canvasImage1.enabled = true;
                canvasImage2.enabled = false;
                if(Pickedup == 1){Pickedup = 0;}
        }

       if(Pickedup == 1){
              ObjectR.velocity = Vector3.zero;
              ObjectR.angularVelocity = Vector3.zero;
              ObjectR.useGravity = false;

        if (Input.GetKey(KeyCode.E)){Object.transform.Rotate(0f, -1f, 0f);}
        if (Input.GetKey(KeyCode.Q)){Object.transform.Rotate(0f, 1f, 0f);}

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f){Object.transform.Rotate(5f, 0f, 0f);}
        else if (scroll < 0f){Object.transform.Rotate(-5f, 0f, 0f);}

       }
       else if(Pickedup == 0){
        if(Grabbing == 1){Grabbing = 0;}

        if(Object != null){
        Object.transform.parent = null;
        if(ObjectR != null){ObjectR.useGravity = true; ObjectR = null;}
        Object = null;
        }          

       }
    }
}
