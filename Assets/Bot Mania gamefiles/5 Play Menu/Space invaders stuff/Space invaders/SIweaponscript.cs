using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIweaponscript : MonoBehaviour
{
    public Transform TurretSpawnedUp;
    public Transform TurretSpawnedDown;
    public Transform TurretSpawnedMiddle;
    public GameObject Bullet;
    public int BulletUp, BulletMid, BulletDown, BulletSpeed;
    public float BulletTimer,BulletTimerIncreaser;
    public AudioClip Clip1;
    public AudioSource Sound;

    void Start(){
        Invoke("StartShip",1f);
    }

    void StartShip()
    {

        if (Input.GetMouseButton(0)){
            if(BulletTimer >= 30){
                BulletTimer = 0;
                BulletUp = 0;
                BulletMid = 0;
                BulletDown = 0;
            }

            if(BulletTimer > 21 && BulletDown == 0){
                Sound.PlayOneShot(Clip1);
                BulletDown = 1;
                GameObject BulletEntity = Instantiate(Bullet, TurretSpawnedDown.position, TurretSpawnedDown.rotation);
                BulletEntity.GetComponent<Rigidbody>().velocity = TurretSpawnedDown.forward * BulletSpeed;
            }

            if(BulletTimer > 11 && BulletUp == 0){
                Sound.PlayOneShot(Clip1);
                BulletUp = 1;
                GameObject BulletEntity = Instantiate(Bullet, TurretSpawnedUp.position, TurretSpawnedUp.rotation);
                BulletEntity.GetComponent<Rigidbody>().velocity = TurretSpawnedUp.forward * BulletSpeed;
            }

            if(BulletTimer > 1 && BulletMid == 0){
                Sound.PlayOneShot(Clip1);
                BulletMid = 1;
                GameObject BulletEntity = Instantiate(Bullet, TurretSpawnedMiddle.position, TurretSpawnedMiddle.rotation);
                BulletEntity.GetComponent<Rigidbody>().velocity = TurretSpawnedMiddle.forward * BulletSpeed;
            }

            if(BulletTimer < 30){BulletTimer += BulletTimerIncreaser;}



        }
        else{
            BulletTimer = 0;
            BulletUp = 0;
            BulletMid = 0;
            BulletDown = 0;
        }
        Invoke("StartShip",0.05f);
    }
}
