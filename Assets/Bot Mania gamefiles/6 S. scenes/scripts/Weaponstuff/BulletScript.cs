using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public WeaponScript WeaponScript;
    public float life = 3;
    public float Damage = 10;
    public float DeleteWhenCollision;
    public bool isEnemyBullet;
    public GameObject[] BulletHole;
    public GameObject playerBulletPrefab;
    public GameObject bulletModel;
    public ParticleSystem Parry;
    public ParticleSystem ExplosionParticle;
    public TrailRenderer BulletTrail;
    public LayerMask layerMask;
    public int IsParried;
    public bool isParriable;
    public bool NormalBullet;
    public bool HasExploded;
    public AudioSource AudioSource;

    public float destroyDistance = 0.5f;

    private Vector3 initialPosition;
    private RaycastHit hit;

    void Start()
    {
        WeaponScript = GameObject.Find("CameraArea223").GetComponent<WeaponScript>();
        Destroy(gameObject, life);

        if(IsParried == 1){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        AudioSource.volume = audioVolume * masterVolume;
        AudioSource.Play();
        Parry.Play();
        }
        else if(IsParried == 2){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        AudioSource.volume = audioVolume * masterVolume;
        AudioSource.Play();
        }

        initialPosition = transform.position;
        if (Physics.Raycast(initialPosition, transform.forward, out hit , Mathf.Infinity, layerMask)){Debug.DrawLine(initialPosition, hit.point, Color.red, 5f);}
    }

    void Update()
    {
        if(NormalBullet){
            float distanceTraveled = Vector3.Distance(initialPosition, transform.position);
            if (hit.collider != null && distanceTraveled >= hit.distance - destroyDistance){
                int RandomBulletholeSprite = UnityEngine.Random.Range(0, BulletHole.Length);

                for (int i2 = 0; i2 < BulletHole.Length; i2++){
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                if(i2 == RandomBulletholeSprite){Instantiate(BulletHole[i2], hit.point + hit.normal * 0.05f, rotation);}
                }
                Destroy(gameObject);
            }
        }
    }

    void Delete(){
        if(!isEnemyBullet){Damage = 10;}
        else{Destroy(gameObject);}
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("MeleeProjectileBot") && isEnemyBullet && isParriable){
            GameObject playerBullet = Instantiate(playerBulletPrefab, transform.position, transform.rotation);
            playerBullet.GetComponent<Rigidbody>().useGravity = false;
            playerBullet.GetComponent<BulletScript>().Damage = Damage*50;
            playerBullet.GetComponent<BulletScript>().IsParried = 1;
            playerBullet.GetComponent<Rigidbody>().AddForce(-transform.forward * 200, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("PlayerMissileBot") && isEnemyBullet && isParriable){
            GameObject playerBullet = Instantiate(playerBulletPrefab, transform.position, transform.rotation);
            playerBullet.GetComponent<Rigidbody>().useGravity = false;
            playerBullet.GetComponent<BulletScript>().Damage = Damage*5;
            playerBullet.GetComponent<BulletScript>().IsParried = 1;
            playerBullet.GetComponent<Rigidbody>().AddForce(-transform.forward * 150, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && isEnemyBullet && WeaponScript.GuardingBot == 1 && isParriable){
            GameObject playerBullet = Instantiate(playerBulletPrefab, transform.position, Quaternion.LookRotation(Camera.main.transform.forward));
            playerBullet.GetComponent<Rigidbody>().useGravity = false;
            playerBullet.GetComponent<BulletScript>().Damage = Damage*15;
            playerBullet.GetComponent<BulletScript>().IsParried = 2;
            playerBullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 300, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else{
            if(NormalBullet){
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                Invoke("Delete",DeleteWhenCollision);
            }
            else{
                if(!HasExploded){
                    float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                    float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                    AudioSource.volume = audioVolume * masterVolume;
                    AudioSource.Play();
                    ExplosionParticle.Play();
                    bulletModel.SetActive(false);
                    BulletTrail.enabled = false;
                    HasExploded = true;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    Invoke("Delete",0.6f);
                }
            }
        }
    }
}