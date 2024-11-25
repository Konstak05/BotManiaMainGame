using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceInvController : MonoBehaviour
{
    public float moveSpeed = 14f;
    public Transform playerTransform;
    public Collider triggerObject;
    public Animator SpaceshipAnimator;
    public float Spaceshiprotation;
    public int lifepoints,Iframes;
    public bool IsDestructable;
    //Barrier
    public Color BarrierColor;
    public MeshRenderer[] BarrierMesh;
    //lifeBar
    public Slider HPslider;
    public TextMeshProUGUI HPtext;
    //Trail
    public TrailRenderer trail;
    //Sounds
    public AudioClip HitSound,DeathSound;
    public AudioSource SoundSource;
    //DeathParticle
    public ParticleSystem DeathParticle,HurtParticle;

    void Start(){
        //HealthStuff
        lifepoints = 100;
        HPslider.value = lifepoints;
        BarrierColor.a = 1f;
        IsDestructable = false;
        HPslider.gameObject.SetActive(false);
        //Trail
        trail.enabled = false;
        Invoke("ShipController",2f);
        Invoke("AddDestructable",2.5f);
    }

    void ShipController()
    {
        if(HPslider.value != lifepoints){HPslider.value = lifepoints;}
        HPtext.text = lifepoints.ToString() + "/100";
        if(Iframes < 30){Iframes++;}

        BarrierIndicator();
        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal")*1.2f;
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        playerTransform.Translate(movement * moveSpeed * 0.03f);

        // Ensure the player stays within the boundaries
        ClampPosition();
        SpaceShipAnimFunction();

        Invoke("ShipController",0.02f);

        if(lifepoints <= 0){HasDied();}
    }
    private void BarrierIndicator()
    {
        //Barrier
        if(IsDestructable == false && BarrierColor.a < 1f){
                Color color1 = BarrierMesh[0].material.color;
                Color color2 = BarrierMesh[1].material.color;
                BarrierColor.a += 0.1f;
                color1.a = BarrierColor.a/1f;
                color2.a = BarrierColor.a/3f;
                BarrierMesh[0].material.color = color1;
                BarrierMesh[1].material.color = color2;
        }
        if(IsDestructable == true && BarrierColor.a > 0){
                Color color1 = BarrierMesh[0].material.color;
                Color color2 = BarrierMesh[1].material.color;
                BarrierColor.a -= 0.1f;
                color1.a = BarrierColor.a/1f;
                color2.a = BarrierColor.a/3f;
                BarrierMesh[0].material.color = color1;
                BarrierMesh[1].material.color = color2;
        }
        if(IsDestructable == true && BarrierColor.a <= 0){
            for (int i2 = 0; i2 < BarrierMesh.Length; i2++){BarrierMesh[i2].gameObject.SetActive(false); HPslider.gameObject.SetActive(true);}
        }
        else if(IsDestructable == false && BarrierColor.a >= 0){
            for (int i2 = 0; i2 < BarrierMesh.Length; i2++){BarrierMesh[i2].gameObject.SetActive(true); HPslider.gameObject.SetActive(false);}
        }
    }
    private void SpaceShipAnimFunction(){
        //SpaceAnimators
        if (Input.GetKey(KeyCode.W)){if(Spaceshiprotation > -1f){Spaceshiprotation -= 0.2f;}}
        if (Input.GetKey(KeyCode.S)){if(Spaceshiprotation < 1f){Spaceshiprotation += 0.2f;}}
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)){
            if(Spaceshiprotation > 0f){Spaceshiprotation -= 0.2f;}
            if(Spaceshiprotation < 0f){Spaceshiprotation += 0.2f;}
        }
        SpaceshipAnimator.SetFloat("Moving", Spaceshiprotation);
    }
    private void ClampPosition()
    {
        Vector3 clampedPosition = playerTransform.position;
        // Clamp position
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, triggerObject.bounds.min.x, triggerObject.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, triggerObject.bounds.min.y, triggerObject.bounds.max.y);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, triggerObject.bounds.min.z, triggerObject.bounds.max.z);
        // Apply the clamped position
        playerTransform.position = clampedPosition;
    }
    private void HasDied(){
        MeshRenderer SpaceshipMesh = gameObject.GetComponent<MeshRenderer>();
        Collider SpaceCollider = gameObject.GetComponent<BoxCollider>();
        SpaceshipMesh.enabled = false;
        SpaceCollider.enabled = false;
        CancelInvoke("ShipController");
        trail.enabled = false;
        HPslider.gameObject.SetActive(false);
        IsDestructable = true;
        DeathParticle.Play();
        SoundSource.PlayOneShot(DeathSound);
        Invoke("StartRespawn",2f);
    }
    private void StartRespawn(){
        MeshRenderer SpaceshipMesh = gameObject.GetComponent<MeshRenderer>();
        Collider SpaceCollider = gameObject.GetComponent<BoxCollider>();
        SpaceshipMesh.enabled = true;
        SpaceCollider.enabled = true;
        //FixBarrier
        Color color1 = BarrierMesh[0].material.color;
        Color color2 = BarrierMesh[1].material.color;
        BarrierColor.a = 1f;
        color1.a = BarrierColor.a/1f;
        color2.a = BarrierColor.a/3f;
        BarrierMesh[0].material.color = color1;
        BarrierMesh[1].material.color = color2;
        BarrierMesh[0].gameObject.SetActive(true);
        BarrierMesh[1].gameObject.SetActive(true);
        //EnableTrail
        trail.enabled = false;
        //ResetHP to 100
        lifepoints = 100;
        HPslider.value = lifepoints;
        IsDestructable = false;
        //ResetAnim
        Spaceshiprotation = 0;
        SpaceshipAnimator.SetFloat("Moving", Spaceshiprotation);

        SpaceshipAnimator.Play("Ship starter");
        Invoke("ShipController",2f);
        Invoke("AddDestructable",2.5f);
    }
    private void AddDestructable(){IsDestructable = true; trail.enabled = true;}


    //CollisionStuff
    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("EnemyBullet") && IsDestructable == true && Iframes >= 30)
        {
            Iframes = 0;
            lifepoints -= 20;
            SoundSource.PlayOneShot(HitSound);
            HurtParticle.Play();
        }
    }
}