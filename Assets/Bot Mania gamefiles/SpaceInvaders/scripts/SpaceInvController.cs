using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceInvController : MonoBehaviour
{
    public float moveSpeed = 14f;
    public Transform playerTransform;
    public Collider triggerObject;
    public Animator SpaceshipAnimator;
    public float Spaceshiprotation;
    public int lifepoints;
    public bool IsDestructable;
    //Barrier
    public Color BarrierColor;
    public MeshRenderer[] BarrierMesh;
    //lifeBar
    public Slider HPslider;
    //Trail
    public TrailRenderer trail;

    void Start(){
        trail.enabled = false;
        lifepoints = 100;
        HPslider.value = lifepoints;
        BarrierColor.a = 1f;
        IsDestructable = false;
        HPslider.gameObject.SetActive(false);
        Invoke("ShipController",2f);
    }

    void ShipController()
    {
        if(HPslider.value != lifepoints){HPslider.value = lifepoints;}
        BarrierIndicator();
   
        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal")*1.2f;
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        playerTransform.Translate(movement * moveSpeed * 0.03f);

        // Ensure the player stays within the boundaries
        ClampPosition();

        SpaceShipAnimFunction();

        //Trailmanager
        if(trail.enabled == false && IsDestructable == false){trail.enabled = true;}
        else if(trail.enabled == true && IsDestructable == true){trail.enabled = false;}

        Invoke("ShipController",0.02f);
    }
    private void BarrierIndicator()
    {
        //Barrier
        if(IsDestructable == true && BarrierColor.a < 1f){
                Color color1 = BarrierMesh[0].material.color;
                Color color2 = BarrierMesh[1].material.color;
                BarrierColor.a += 0.1f;
                color1.a = BarrierColor.a/1f;
                color2.a = BarrierColor.a/3f;
                BarrierMesh[0].material.color = color1;
                BarrierMesh[1].material.color = color2;
        }
        if(IsDestructable == false && BarrierColor.a > 0){
                Color color1 = BarrierMesh[0].material.color;
                Color color2 = BarrierMesh[1].material.color;
                BarrierColor.a -= 0.1f;
                color1.a = BarrierColor.a/1f;
                color2.a = BarrierColor.a/3f;
                BarrierMesh[0].material.color = color1;
                BarrierMesh[1].material.color = color2;
        }
        if(IsDestructable == false && BarrierColor.a <= 0){
            for (int i2 = 0; i2 < BarrierMesh.Length; i2++){BarrierMesh[i2].gameObject.SetActive(false); HPslider.gameObject.SetActive(true);}
        }
        else if(IsDestructable == true && BarrierColor.a >= 0){
            for (int i2 = 0; i2 < BarrierMesh.Length; i2++){BarrierMesh[i2].gameObject.SetActive(true); HPslider.gameObject.SetActive(false);}
        }
    }
    private void SpaceShipAnimFunction(){
        //SpaceAnimators
        SpaceshipAnimator.SetFloat("Moving", Spaceshiprotation);
        if (Input.GetKey(KeyCode.W)){if(Spaceshiprotation > -1f){Spaceshiprotation -= 0.2f;}}
        if (Input.GetKey(KeyCode.S)){if(Spaceshiprotation < 1f){Spaceshiprotation += 0.2f;}}
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)){
            if(Spaceshiprotation > 0f){Spaceshiprotation -= 0.2f;}
            if(Spaceshiprotation < 0f){Spaceshiprotation += 0.2f;}
        }
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
}