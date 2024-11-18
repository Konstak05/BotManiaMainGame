using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMeleetargetting : MonoBehaviour
{

    public float speed;
    public ParticleSystem KaboomParticle;
    public ParticleSystem Parry;
    public GameObject MissileObject;
    public GameObject HitboxMissile;
    public GameObject PlayerMissilePrefab;
    public AudioSource ExplosionSound;
    public int HasExploded;
    public int IsEnemy;
    public Enemydamagegiver MissileDamage;
    public float ExplodesAfter = 10f;
    public LayerMask Ground;
    public bool isParriable;
    public int IsParried;
    public AudioSource AudioSource;

    void Start(){
        Invoke("DeleteMissile", ExplodesAfter);

        if(IsParried == 1){
        Parry.Play();
        }
    }

    void Update()
    {
    if (HasExploded == 0)
    {
                GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
    }

    void OnCollisionEnter(Collision other)
    {
    if (Ground == (Ground | (1 << other.gameObject.layer)))
    {
     if(HasExploded == 0){
     HasExploded = 1;
     CancelInvoke("DeleteMissile");
     DeleteMissile();
     }
    }
    else if(other.gameObject.CompareTag("MeleeProjectileBot") && IsEnemy == 1 && isParriable){
        if(HasExploded == 0){
        GameObject PlayerMissile = Instantiate(PlayerMissilePrefab, transform.position, Quaternion.LookRotation(Camera.main.transform.forward));
        PlayerMissile.GetComponent<Rigidbody>().useGravity = false;
        PlayerMissile.GetComponent<GuardMeleetargetting>().IsParried = 1;
        PlayerMissile.GetComponent<Enemydamagegiver>().Damage = MissileDamage.Damage*30;
        PlayerMissile.GetComponent<Rigidbody>().AddForce(-transform.forward * 350, ForceMode.Impulse);
        HasExploded = 1;

        Destroy(gameObject);

        }
    }
    }

    void DeleteMissile()
    {
     GameObject Missilehitbox = Instantiate(HitboxMissile, gameObject.transform.position, gameObject.transform.rotation);
     gameObject.GetComponent<Collider>().enabled = false;
     Missilehitbox.GetComponent<Enemydamagegiver>().Damage = MissileDamage.Damage;

     KaboomParticle.Play();
     float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
     float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
     ExplosionSound.volume = audioVolume * masterVolume;
     ExplosionSound.Play();
     MissileObject.SetActive(false);
     Invoke("DeleteMissile2", 5f);
    }

    void DeleteMissile2()
    {

     Destroy(gameObject);
    }
}
