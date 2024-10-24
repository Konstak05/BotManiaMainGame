using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrierScript : MonoBehaviour
{
    private GameObject Player;

    private int isactivated;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Player == null){
            Player = other.gameObject;
            }
            Player.GetComponent<KeyboardControlMk2>().HP = 0;
        }
    }
}
