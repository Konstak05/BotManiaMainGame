using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform playerTransform;
    public Collider triggerObject;
    public Animator SpaceshipAnimator;
    public float Spaceshiprotation;

    void Start(){
        Invoke("ShipController",2f);
    }

    void ShipController()
    {
        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal")*1.2f;
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);

        // Move the player
        playerTransform.Translate(movement * moveSpeed * 0.03f);

        // Ensure the player stays within the trigger object
        ClampPosition();

        SpaceshipAnimator.SetFloat("Moving", Spaceshiprotation);

        if (Input.GetKey(KeyCode.W)){if(Spaceshiprotation > -1f){Spaceshiprotation -= 0.2f;}}
        if (Input.GetKey(KeyCode.S)){if(Spaceshiprotation < 1f){Spaceshiprotation += 0.2f;}}

        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)){
            if(Spaceshiprotation > 0f){Spaceshiprotation -= 0.2f;}
            if(Spaceshiprotation < 0f){Spaceshiprotation += 0.2f;}
        }
        Invoke("ShipController",0.02f);
    }

    void ClampPosition()
    {
        Vector3 clampedPosition = playerTransform.position;

        // Clamp X position
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, triggerObject.bounds.min.x, triggerObject.bounds.max.x);

        // Clamp Z position
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, triggerObject.bounds.min.y, triggerObject.bounds.max.y);

        // Clamp Z position
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, triggerObject.bounds.min.z, triggerObject.bounds.max.z);

        // Apply the clamped position
        playerTransform.position = clampedPosition;
    }
}