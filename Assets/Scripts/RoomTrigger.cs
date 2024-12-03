using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
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
#if UNITY_EDITOR
    BoxCollider[] col;
    private void OnDrawGizmos()
    {
        if (col == null || col.Length == 0) { col = GetComponents<BoxCollider>(); return; }

        Gizmos.color = Color.blue;
        for (int i = 0; i < col.Length; i++)
        {
            Gizmos.DrawWireCube(transform.position + col[i].center, col[i].size);
        }
    }
#endif
}