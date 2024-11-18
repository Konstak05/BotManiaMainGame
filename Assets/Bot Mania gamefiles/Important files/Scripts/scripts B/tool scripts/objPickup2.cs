using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class objPickup2 : MonoBehaviour
{
    //ControlInputs
    public PlayerInput PlayerInputStarter;
    public InputAction MovingAction;
    public InputAction FireAction;
    public InputAction UseAction;
    public InputAction ResetAction;
    public InputAction RotateLeftAction;
    public InputAction RotateRightAction;
    public InputAction RotateUpAction;
    public InputAction RotateDownAction;
    public Vector2 newcontrolls;
    public bool FireKey;
    public bool RightClickKey;
    public bool FireKey2;
    public bool RightClickKey2;
    public bool ResetKey;
    public bool RotateLeftKey;
    public bool RotateRightKey;
    public bool RotateUpKey;
    public bool RotateDownKey;

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

    void Start(){
        MovingAction = PlayerInputStarter.actions["Move"];
        FireAction = PlayerInputStarter.actions["Fire"];
        UseAction = PlayerInputStarter.actions["Use"];
        ResetAction = PlayerInputStarter.actions["Reset"];
        RotateLeftAction = PlayerInputStarter.actions["Rotate Left"];
        RotateRightAction = PlayerInputStarter.actions["Rotate Right"];
        RotateUpAction = PlayerInputStarter.actions["Rotate Up"];
        RotateDownAction = PlayerInputStarter.actions["Rotate Down"];

        canvasImage1 = crosshair1.GetComponent<Image>();
        canvasImage2 = crosshair2.GetComponent<Image>();
        canvasImage1.enabled = true;
        canvasImage2.enabled = false;
        mainCamera = Camera.main;
        Grabbing = 0;
    }


    void Update(){
        newcontrolls = MovingAction.ReadValue<Vector2>();
        FireKey = FireAction.WasPressedThisFrame();
        RightClickKey = UseAction.WasPressedThisFrame();
        FireKey2 = FireAction.WasReleasedThisFrame();
        RightClickKey2 = UseAction.WasReleasedThisFrame();
        ResetKey = ResetAction.WasPressedThisFrame();
        RotateLeftKey = RotateLeftAction.IsPressed();
        RotateRightKey = RotateRightAction.IsPressed();
        RotateUpKey = RotateUpAction.IsPressed();
        RotateDownKey = RotateDownAction.IsPressed();

        RaycastHit hit;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
      

        if (Physics.Raycast(ray, out hit, MaxRange, LayerMask.GetMask("Objects")) && ToggleUI.PauseMenu == 0 && Gunscript.GunEquipped == 0 && Player.GetComponent<KeyboardControlMk2>().HP > 0){
            canvasImage1.enabled = false;
            canvasImage2.color = Color.blue;
            canvasImage2.enabled = true;

            if(Object != null && Object != hit.collider.gameObject){
            ObjectR = Object.GetComponent<Rigidbody>(); 
            Object.transform.parent = null; 
            Pickedup = 0;}

            Object = hit.collider.gameObject;

            if(ResetKey){
                ObjectR = Object.GetComponent<Rigidbody>(); 
                ObjectR.useGravity = true;
                ObjectR.isKinematic = false;
            }

            if (FireKey){
                ObjectR = Object.GetComponent<Rigidbody>(); 
                Object.transform.parent = mainCamera.transform; 
                Pickedup = 1; 
                Grabbing = 1;
            }
            else if (FireKey2){
                ObjectR = Object.GetComponent<Rigidbody>();
                Object.transform.parent = null; 
                ObjectR.useGravity = true; 
                if(GravityGun.InvertedMode == 0){ObjectR.isKinematic = false;} 
                Grabbing = 0; 
                Pickedup = 0;
            }




            if (RightClickKey)
            {
                if(GravityGun.InvertedMode == 0){
                ObjectR = Object.GetComponent<Rigidbody>();
                Vector3 throwDirection = mainCamera.transform.forward;

                if (newcontrolls.x != 0 || newcontrolls.y != 0){
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
        else if (Physics.Raycast(ray, out hit, MaxRange, ~LayerMask.GetMask("Objects")) | FireKey2 | ToggleUI.PauseMenu == 1 | Gunscript.GunEquipped != 0 | Player.GetComponent<KeyboardControlMk2>().HP < 0){
                canvasImage1.enabled = true;
                canvasImage2.enabled = false;
                if(Pickedup == 1){Pickedup = 0;}
        }

        if(Pickedup == 1){
            ObjectR.velocity = Vector3.zero;
            ObjectR.angularVelocity = Vector3.zero;
            ObjectR.useGravity = false;

            if (RotateRightKey){Object.transform.Rotate(0f, -1f, 0f);}
            if (RotateLeftKey){Object.transform.Rotate(0f, 1f, 0f);}

            if (RotateUpKey){Object.transform.Rotate(1f, 0f, 0f);}
            else if (RotateDownKey){Object.transform.Rotate(-1f, 0f, 0f);}
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
