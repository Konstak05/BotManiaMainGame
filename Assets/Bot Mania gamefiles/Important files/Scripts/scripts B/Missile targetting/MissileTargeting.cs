using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTargeting : MonoBehaviour
{

    public float speed;
    public float Max;
    public float Min;
    public float Turn;
    public Transform target;
    public ParticleSystem KaboomParticle;
    public ParticleSystem SmokeParticle;
    public GameObject MissileObject;
    public GameObject HitboxMissile;
    public AudioSource ExplosionSound;
    public int HasExploded;
    public int IsEnemy;
    public GameObject[] targets;
    public Enemydamagegiver MissileDamage;
    public float ExplodesAfter = 10f;
    public LayerMask Ground;
    public LayerMask PlayerAttack;
    public int CanBeParried = 1;

    void Start(){
        Invoke("DeleteMissile", ExplodesAfter);
        Invoke("MissileInterval", 0.01f);
    }

    void Update(){
        if(IsEnemy == 0){targets = GameObject.FindGameObjectsWithTag("Enemy");}
        else{targets = GameObject.FindGameObjectsWithTag("Player");}
    }

    private void MissileInterval(){
        if (targets.Length > 0)
        {
            float closestDistance = Mathf.Infinity;
            foreach (GameObject t in targets)
            {
                if(t != null){
                    float distance = Vector3.Distance(transform.position, t.transform.position);
                    if (distance < closestDistance && distance <= Max && distance >= Min)
                    {
                        closestDistance = distance;
                        target = t.transform;
                    }
                }
            }

            if (target != null && HasExploded == 0)
            {
                Vector3 direction = (target.position + new Vector3(0, 0, 0) - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);
                float rotationSpeed = Turn;
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
                GetComponent<Rigidbody>().velocity = transform.forward * speed;
            }
        }
        Invoke("MissileInterval", 0.01f);
    }


    void OnCollisionEnter(Collision other)
    {
        if (Ground == (Ground | (1 << other.gameObject.layer)))
        {
            if(HasExploded == 0){
                HasExploded = 1;
                CancelInvoke("DeleteMissile");
                CancelInvoke("MissileInterval");
                DeleteMissile();
            }
        }
        if (PlayerAttack == (PlayerAttack | (1 << other.gameObject.layer)) && CanBeParried == 1)
        {
            if(HasExploded == 0){
                HasExploded = 1;
                CancelInvoke("DeleteMissile");
                CancelInvoke("MissileInterval");
                DeleteMissile();
            }
        }
    }

    void DeleteMissile()
    {
        GameObject Missilehitbox = Instantiate(HitboxMissile, gameObject.transform.position, gameObject.transform.rotation);
        gameObject.GetComponent<Collider>().enabled = false;
        if(IsEnemy == 1){Missilehitbox.GetComponent<Enemydamagegiver>().Damage = MissileDamage.Damage;}
        SmokeParticle.Stop();
        KaboomParticle.Play();
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        ExplosionSound.volume = audioVolume * masterVolume;
        ExplosionSound.Play();
        MissileObject.SetActive(false);
        Invoke("DeleteMissile2", 5f);
    }

    void DeleteMissile2(){Destroy(gameObject);}
}
