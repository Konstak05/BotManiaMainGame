using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TriggerTool : MonoBehaviour

{
    //ControlInputs
    public PlayerInput PlayerInputStarter;
    public InputAction FireAction;
    public InputAction RightAction;
    public bool FireKey;
    public bool RightClickKey;
    public bool FireKey2;
    public bool RightClickKey2;

    public GameObject Player;
    public GameObject PlayerCamera;
    public GravityGun GravityGun;
    public ToggleUI ToggleUI;
    public Gunscript Gunscript;
    public GameObject crosshair1, crosshair2;
    public Transform objTransform;
    public bool pickedup;
    public Rigidbody objRigidbody;
    private Image canvasImage1;
    private Image canvasImage2;
    public CharacterController playerController;
    public GameObject Object;
    public GameObject OwnerObject;
    public AudioClip mySoundClip;
    public float volume;
    public bool ThrusterStarter = false;
    public ParticleSystem Particle;
    public int Thruster = 0;
    public Renderer objRenderer;
    private static AudioClip lastPlayedClip;

    void Start()
    {
        crosshair1 = GameObject.Find("Crosshair 1");

        Player = GameObject.Find("Player");
        PlayerCamera = GameObject.Find("CameraArea223");
        GravityGun = PlayerCamera.GetComponent<GravityGun>();
        Gunscript = PlayerCamera.GetComponent<Gunscript>();
        ToggleUI = Player.GetComponent<ToggleUI>();
        crosshair2 = GameObject.Find("Crosshair 2");
        canvasImage1 = crosshair1.GetComponent<Image>();
        canvasImage2 = crosshair2.GetComponent<Image>();
        objTransform = transform;
        objRigidbody = GetComponent<Rigidbody>();
        objRenderer = GetComponent<Renderer>();
        canvasImage1.enabled = true;
        canvasImage2.enabled = false;
        Particle.Stop();

        lastPlayedClip = null;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera") & ToggleUI.PauseMenu == 0 & Gunscript.GunEquipped == 6){canvasImage1.enabled = true; canvasImage2.enabled = false;}
        else if (other.CompareTag("MainCamera") & ToggleUI.PauseMenu == 1 & Gunscript.GunEquipped == 6){canvasImage1.enabled = true; canvasImage2.enabled = false;}
    }

    void Update()
    {
        FireKey = FireAction.WasPressedThisFrame();
        RightClickKey = RightAction.WasPressedThisFrame();
        FireKey2 = FireAction.WasReleasedThisFrame();
        RightClickKey2 = RightAction.WasReleasedThisFrame();
        if(Player != null && playerController == null & PlayerInputStarter == null){
        playerController = Player.GetComponent<CharacterController>();
        PlayerInputStarter = Player.GetComponent<PlayerInput>();
        FireAction = PlayerInputStarter.actions["Fire"];
        RightAction = PlayerInputStarter.actions["Use"];
        }
        if (FireKey && Gunscript.GunEquipped == 6 && ToggleUI.PauseMenu == 0 && GravityGun.InvertedMode == 0){
            if(Object == null){
                RaycastHit hit;
                Camera camera = Camera.main;
                Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));              
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Objects"))){
                    Object = hit.collider.gameObject;
                }
            }
            if(Object == OwnerObject){
                float AudioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                volume = AudioVolume * masterVolume;
                AudioSource.PlayClipAtPoint(mySoundClip, Camera.main.transform.position, volume);
                var main = Particle.main;
                var color = objRenderer.material.color;
                color.a = 1f;
                main.startColor = color;
                ThrusterStarter = !ThrusterStarter;
            }
            else{
                Object = null;
            }
        }
        else if (RightClickKey && Gunscript.GunEquipped == 6 && ToggleUI.PauseMenu == 0 && GravityGun.InvertedMode == 0){
            if(Object == OwnerObject){
                float AudioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                volume = AudioVolume * masterVolume;
                AudioSource.PlayClipAtPoint(mySoundClip, Camera.main.transform.position, volume);
                ThrusterStarter = false;
                Object = null;
            }
        }
        if (FireKey && Gunscript.GunEquipped == 6 && ToggleUI.PauseMenu == 0 && GravityGun.InvertedMode == 1){
            //Sound
            if (mySoundClip != null && lastPlayedClip != mySoundClip){
                float AudioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                volume = AudioVolume * masterVolume;
                AudioSource.PlayClipAtPoint(mySoundClip, Camera.main.transform.position, volume);
                lastPlayedClip = mySoundClip;
            }
            var main = Particle.main;
            var color = objRenderer.material.color;
            color.a = 1f;
            main.startColor = color;
            ThrusterStarter = true;
        }
        if (RightClickKey && Gunscript.GunEquipped == 6 && ToggleUI.PauseMenu == 0 && GravityGun.InvertedMode == 1){
            //Sound
            if (mySoundClip != null && lastPlayedClip != mySoundClip){
                float AudioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                volume = AudioVolume * masterVolume;
                AudioSource.PlayClipAtPoint(mySoundClip, Camera.main.transform.position, volume);
                lastPlayedClip = mySoundClip;
            }
            ThrusterStarter = false;
        }

        if (FireKey2 && Gunscript.GunEquipped == 6 && ToggleUI.PauseMenu == 0){lastPlayedClip = null;}
        if (RightClickKey2 && Gunscript.GunEquipped == 6 && ToggleUI.PauseMenu == 0){lastPlayedClip = null;}
        if (ThrusterStarter == true){
            objRigidbody.AddForce(transform.forward * 100f, ForceMode.Impulse);
            Particle.Play();
        }
        else if (ThrusterStarter == false){Particle.Stop();}
    }
}