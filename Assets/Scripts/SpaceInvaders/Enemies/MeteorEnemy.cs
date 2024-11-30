using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeteorEnemy : MonoBehaviour
{
    //lifeBar
    public Slider HPslider;
    public GameObject HPsliderObject;
    public int Iframes;
    public float lifepoints;
    public bool IsDestructable;
    //DeathParticle
    public ParticleSystem DeathParticle;
    //Scoreadder
    public int AddScoreValue;
    public EnemySpanwerBase Score;

    void Start()
    {
        Score = GameObject.Find("EnemySpawnerPosition").GetComponent<EnemySpanwerBase>();

        //HealthStuff
        lifepoints = lifepoints;
        HPslider.maxValue = lifepoints;
        HPslider.value = lifepoints;
        IsDestructable = true;
        Invoke("EnemyStarter",2f);
    }

    void EnemyStarter()
    {
        if(HPslider.value != lifepoints){HPslider.value = lifepoints;}
        if(Iframes < 5){Iframes++;}

        Invoke("EnemyStarter",0.02f);
        if(lifepoints <= 0){HasDied();}
    }
    private void HasDied(){
        CancelInvoke("EnemyStarter");
        MeshRenderer SpaceshipMesh = gameObject.GetComponent<MeshRenderer>();
        Collider SpaceCollider = gameObject.GetComponent<MeshCollider>();
        SpaceshipMesh.enabled = false;
        SpaceCollider.enabled = false;
        HPsliderObject.SetActive(false);
        IsDestructable = true;
        DeathParticle.Play();
        Score.AddScore(AddScoreValue);
    }

    //CollisionStuff
    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("PlayerBullet") && IsDestructable == true && Iframes >= 5)
        {
            Iframes = 0;
            lifepoints -= 20;
            GameObject PlayerBullet = other.gameObject;
            Destroy(PlayerBullet);
        }
    }
}
