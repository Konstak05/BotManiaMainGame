using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class MouseLook : MonoBehaviour

{
    public ToggleUI ToggleUI;
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    public float xRotation = 0f;

    public float MouseX;
    public float MouseY;
    public GameObject MainCameraMain;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(90, 0f, 0f);
        //Cursor.lockState = CursorLockMode.Locked;
        Invoke("Mouselook",1f);
    }


    public void RespawnOnMap2()
    {
        MouseX = 0f;
        MouseY = 0f;
        xRotation = 0f;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        MainCameraMain.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void FalldamageApplier(){xRotation += 20;}


    void Mouselook()
    {
        if(ToggleUI.PauseMenu == 0){
            float MouseX = Input.GetAxis("Mouse X") * PlayerPrefs.GetFloat("Sensitivity") * 0.01f;
            float MouseY = Input.GetAxis("Mouse Y")/2 * PlayerPrefs.GetFloat("Sensitivity") * 0.01f;

            xRotation -= MouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 66f);

            MainCameraMain.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * MouseX);
        }
        Invoke("Mouselook",0.01f);
    }
}