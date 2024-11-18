using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timespawner : MonoBehaviour
{
    
    public GameObject Turret;
    public float MAG = 1f;
    public float SpawnTimer = 1f;
    public Transform ActiveTurret;
    public int Hasexploded;
    public GameObject GrenadahModel;
    public ParticleSystem GrenadahExplodah;
    public LayerMask Ground;

    void SpawnTurret()
    {
        var TurretEnemy = Instantiate(Turret, gameObject.transform.position, gameObject.transform.rotation);
        ActiveTurret = TurretEnemy.transform.Find("Sentry");
        ActiveTurret.GetComponent<EnemySentrybot>().Mag = MAG;
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")){Invoke("SpawnTurret",SpawnTimer);}

        if (other.gameObject.CompareTag("PlayerBulletBot") | other.gameObject.CompareTag("MeleeProjectileBot") | other.gameObject.CompareTag("PlayerMissileBot"))
        {
            if(Hasexploded == 0){
            gameObject.GetComponent<Collider>().enabled = false;
            Hasexploded = 1;
            GrenadahExplodah.Play();
            CancelInvoke("SpawnTurret");
            GrenadahModel.SetActive(false);
            Invoke("Delete",0.5f);
            }        
        }
    }
    void Delete(){Destroy(gameObject);}
}
