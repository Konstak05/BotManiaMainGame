using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject[] Rooms;
    private bool isActive = false;
    public int[] RoomActivationID;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
        
  
        for (int i = 0; i < Rooms.Length; i++){
            if (RoomActivationID[i] == 1){Rooms[i].SetActive(true);}
            else{Rooms[i].SetActive(false);}
        }
        isActive = true;
        Invoke("Refresh",2.5f);
        }

    }

    void Refresh()
    {
      isActive = false;
    }

}