using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    public PlayerInput PlayerInputStarter;
    public InputAction FireAction;
    public bool FireKey;

    public GameObject Player;
    public KeyboardControlMk2 PlayerScript;
    public Gunscript Gunscript;
    public ToggleUI ToggleUI;
    public Transform Spawner;
    public GameObject bulletPrefab;
    public int bulletSpeed;
    public int Shoot,Shoot2;
    public AudioClip Clip1;
    public AudioClip Clip2;
    public AudioClip Clip3;
    public AudioSource Sound;
    public AudioSource Sound2;
    public AudioSource HomingMissileAudioSource;
    public ParticleSystem Bulletthrow;
    public GameObject Flash;
    public GameObject Gun1;


    public Animator ShieldAnim;
    public float Guarding;
    public int GuardingMode;
    public int GuardingBot;

    //Melee
    public GameObject Melee1;
    public int MeleeFix;
    public GameObject MeleePrefab1;
    public Transform MeleeSpawner;
    public int MeleeChargedSound;

    //Healing
    public ParticleSystem HealingParticle;
    public Animator HealingAnimation;
    public float HealAnim;
    public float Healmode;
    public int IsHealingPlayer;

    //HomingMissile
    public GameObject HomingMissileObject,HMsmoke,HMsmoke2,MissileGameObject;
    public Transform MissileSpawner;
    public int HasLaunchedMissile,HomingMissileCharged;
    public float HomingMissileLauncherFix;


    void Start()
    {
        FireAction = PlayerInputStarter.actions["Fire"];
        Player = GameObject.Find("Player");
        PlayerScript = Player.GetComponent<KeyboardControlMk2>();

        Flash.SetActive(false);
        GuardingBot = 0;
        HomingMissileCharged = 1;
        HMsmoke.SetActive(false);
        HMsmoke2.SetActive(false);
    }


    void FixedUpdate()
    {
        FireKey = FireAction.IsPressed();
        if(Shoot < 500){Shoot += 1;}
        if(Shoot2 < 500){Shoot2 += 1;}

        if (FireKey && Gunscript.GunEquipped == 7 && ToggleUI.PauseMenu == 0)
        {
        ShieldAnim.SetFloat("Guard", Guarding);
        if(Guarding < 45){Guarding = Guarding + 1f;}
        if(Guarding >= 45){GuardingBot = 1; GuardingMode = 1;}
        }

        else if (Gunscript.GunEquipped == 7 && !FireKey | ToggleUI.PauseMenu == 1){
        ShieldAnim.SetFloat("Guard", Guarding);
        if(Guarding > 0){Guarding = Guarding - 1f;}
        else if(Guarding <= 0){GuardingMode = 0;}
        GuardingBot = 0;}

        
    


        if (FireKey && Gunscript.GunEquipped == 1 && ToggleUI.PauseMenu == 0 && Shoot >= 25 && PlayerScript.HP > 0)
        {
            bulletSpeed = 800;
            SpawnBullet();
            Flash.SetActive(true);
            Invoke("StopFlash",0.050f);
            Shoot = 0;
        }







        if (FireKey && Gunscript.GunEquipped == 8 && ToggleUI.PauseMenu == 0 && Shoot2 >= 400 && PlayerScript.HP > 0)
        {
        MeleeAttack1();
        Shoot2 = 0;
        MeleeChargedSound = 1;
        }

        if(Shoot2 >= 400 && PlayerScript.HP > 0 && MeleeChargedSound == 1){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound2.volume = audioVolume * masterVolume;
        Sound2.PlayOneShot(Clip3);

        MeleeChargedSound = 0;
        }





        if (FireKey && Gunscript.GunEquipped == 10 && ToggleUI.PauseMenu == 0 && PlayerScript.HP > 0)
        {
        if(Healmode >= 45 && HealAnim == 1f){HealingParticle.Play(); HealAnim = 0f; IsHealingPlayer = 1;}
        if(Healmode < 45){Healmode = Healmode + 0.75f; HealAnim = 1f;}


        }
        else{
        if(Healmode > 0){Healmode = Healmode - 0.75f; HealAnim = 1f; HealingParticle.Stop();}
        if(Healmode < 25){ IsHealingPlayer = 0;}
        }

        if(Gunscript.GunEquipped == 10){HealingAnimation.SetFloat("Eat", Healmode);}




        if (FireKey && Gunscript.GunEquipped == 11 && ToggleUI.PauseMenu == 0 && PlayerScript.HP > 0 && HomingMissileCharged == 1)
        {
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        HomingMissileAudioSource.volume = audioVolume * masterVolume;
        HomingMissileAudioSource.Play();
        HomingMissileObject.transform.Rotate(-3.5f, 0f, 0f);
        HomingMissileCharged = 0;
        HasLaunchedMissile = 0;
        HomingMissileLauncherFix = 100f;
        HMsmoke.SetActive(true);
        HMsmoke2.SetActive(false);

        GameObject MissileEntity = Instantiate(MissileGameObject, MissileSpawner.position, MissileSpawner.rotation);
        MissileEntity.GetComponent<Rigidbody>().velocity = MissileSpawner.forward * 250;

        Invoke("HomingMissileGoback1",0.05f);
        Invoke("HomingMissileGoback2",0.5f);
        Invoke("HomingMissileHasCharged",1.37f);
        }

    }

    void SpawnBullet(){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip1);
        GameObject bullet = Instantiate(bulletPrefab, Spawner.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().useGravity = false;
        bullet.GetComponent<Rigidbody>().velocity = Spawner.forward * bulletSpeed;
        Bulletthrow.Play();
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Gun1.transform.Rotate(6f, 0f, 0f);}

    void StopFlash(){Flash.SetActive(false); Gun1.transform.Rotate(-2f, 0f, 0f); Invoke("StopFlash2",0.025f);}
    void StopFlash2(){Gun1.transform.Rotate(-2f, 0f, 0f); Invoke("StopFlash3",0.025f);}
    void StopFlash3(){Gun1.transform.Rotate(-2f, 0f, 0f);}


    void MeleeAttack1(){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip2);
        Melee1.transform.Rotate(24f, 0f, 0f);
        Instantiate(MeleePrefab1, MeleeSpawner.position , transform.rotation);
        Invoke("Goback", 0.01f);}


    void Goback(){

    if(MeleeFix < 120){
    Melee1.transform.Rotate(-0.2f, 0f, 0f);
    MeleeFix += 1;
    Invoke("Goback", 0.01f);}
    if(MeleeFix >= 120){
    MeleeFix = 0;
    CancelInvoke("Goback");}
    }

    void HomingMissileGoback1(){HomingMissileObject.transform.Rotate(-3.5f, 0f, 0f); HMsmoke.SetActive(false); HMsmoke2.SetActive(true); Invoke("HomingMissileGoback1a",0.1f);}
    void HomingMissileGoback1a(){HMsmoke2.SetActive(false);}

    void HomingMissileGoback2(){
    
    if(HomingMissileLauncherFix > 80 && HasLaunchedMissile == 0){
     HomingMissileLauncherFix = HomingMissileLauncherFix - 0.5f;
     HomingMissileObject.transform.Rotate(-0.025f, 0f, 0f);
     Invoke("HomingMissileGoback2",0.01f);
    }
    if(HomingMissileLauncherFix <= 80 && HasLaunchedMissile == 0){HasLaunchedMissile = 1;}
    if(HomingMissileLauncherFix > 0 && HasLaunchedMissile == 1){HomingMissileLauncherFix = HomingMissileLauncherFix - 1f; HomingMissileObject.transform.Rotate(0.1f, 0f, 0f); Invoke("HomingMissileGoback2",0.01f);}}

    void HomingMissileHasCharged(){HomingMissileCharged = 1;}
}
