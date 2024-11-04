using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeldTool2 : MonoBehaviour
{
    //ControlInputs
    public PlayerInput PlayerInputStarter;
    public InputAction FireAction;
    public InputAction RightAction;
    public InputAction ResetAction;
    public bool FireKey;
    public bool RightClickKey;
    public bool FireKey2;
    public bool RightClickKey2;
    public bool ResetKey;
  
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
    public GameObject Object1Save;
    public GameObject Object2Save;
    public AudioClip ClickSound;
    public AudioSource Sound;
    public bool AlreadyChanged;

    public bool HasSticked;
    public bool HasSticked2;
    public bool HastoStick;
    public bool Cantclick;
    public Rigidbody Object2SaveRigid;
    private Joint targetJoint;

    void Start()
    {
      FireAction = PlayerInputStarter.actions["Fire"];
      RightAction = PlayerInputStarter.actions["Use"];
      ResetAction = PlayerInputStarter.actions["Reset"];

      canvasImage1 = crosshair1.GetComponent<Image>();
      canvasImage2 = crosshair2.GetComponent<Image>();
      canvasImage1.enabled = true;
      canvasImage2.enabled = false;
      mainCamera = Camera.main;
      AlreadyChanged = true;
    }


    void Update()
    {
      FireKey = FireAction.WasPressedThisFrame();
      RightClickKey = RightAction.WasPressedThisFrame();
      FireKey2 = FireAction.WasReleasedThisFrame();
      RightClickKey2 = RightAction.WasReleasedThisFrame();
      ResetKey = ResetAction.WasReleasedThisFrame();

      RaycastHit hit;
      Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
      
      if (Physics.Raycast(ray, out hit, MaxRange, LayerMask.GetMask("Objects")) && ToggleUI.PauseMenu == 0 && Gunscript.GunEquipped == 3)
      {
        Cantclick = true;
        canvasImage1.enabled = false;
        canvasImage2.color = Color.green;
        canvasImage2.enabled = true;
        Object = hit.collider.gameObject;
        AlreadyChanged = true;

        if (FireKey && !HasSticked){   
          float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
          float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
          Sound.volume = audioVolume * masterVolume;
          Sound.PlayOneShot(ClickSound);
          Object1Save = Object;
          HasSticked = true;
          HastoStick = false;
        }
        if (FireKey2 && HasSticked && !HastoStick){HastoStick = true;}

        if (FireKey && HastoStick && !HasSticked2){   
          if(hit.collider.gameObject != Object1Save){
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(ClickSound);
            Object2Save = Object;
            HasSticked2 = true;
            HastoStick = false;

            if(GravityGun.InvertedMode == 0){
              Object2SaveRigid = Object2Save.GetComponent<Rigidbody>();
              ConfigurableJoint joint = Object2Save.AddComponent<ConfigurableJoint>();
              joint.connectedBody = Object1Save.GetComponent<Rigidbody>();
              joint.autoConfigureConnectedAnchor = true;
              joint.xMotion = ConfigurableJointMotion.Locked;
              joint.yMotion = ConfigurableJointMotion.Locked;
              joint.zMotion = ConfigurableJointMotion.Locked;
              joint.angularXMotion = ConfigurableJointMotion.Locked;
              joint.angularYMotion = ConfigurableJointMotion.Locked;
              joint.angularZMotion = ConfigurableJointMotion.Locked;
              Object2SaveRigid.isKinematic = false;
            }
            else{
              Object2SaveRigid = Object2Save.GetComponent<Rigidbody>();
              Object2Save.transform.parent = Object1Save.transform;
              Object2SaveRigid.isKinematic = true;
            }
              Object2SaveRigid = null;
              Object1Save = null;
              Object2Save = null;
              HasSticked = false;
              HasSticked2 = false;
              HastoStick = false;
          }
        }
      }
      else if (Physics.Raycast(ray, out hit, MaxRange, ~LayerMask.GetMask("Objects")) | ToggleUI.PauseMenu == 1 | Gunscript.GunEquipped != 3 && AlreadyChanged == true){
        Cantclick = false;
        canvasImage1.enabled = true;
        canvasImage2.enabled = false;
        AlreadyChanged = false;
      }

      if (ToggleUI.PauseMenu == 1 | Gunscript.GunEquipped != 3 && AlreadyChanged == true){
        if(Object != null){Object = null;}          
        if(Object1Save != null){Object1Save = null;}
        if(Object2Save != null){Object2Save = null;}
        AlreadyChanged = false;
      }   
      if(ResetKey && ToggleUI.PauseMenu == 0 && Gunscript.GunEquipped == 3){
        if(Cantclick == true){
          float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
          float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
          Sound.volume = audioVolume * masterVolume;
          Sound.PlayOneShot(ClickSound);
          Object1Save = null;
          Object2Save = null;
          HasSticked = false;
          HasSticked2 = false;
          HastoStick = false;
          if(hit.collider.GetComponent<Joint>() != null){targetJoint = hit.collider.GetComponent<Joint>(); if(targetJoint != null){Destroy(targetJoint);}}
        }
      }
    }
}