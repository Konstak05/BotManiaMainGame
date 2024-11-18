using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleScript : MonoBehaviour
{
    public string Playerpref;
    public Botslock UnlockObject;
    public StoryModeEnemylock UnlockObjectEnemy;
    public GameObject Prize;
    public GameObject OwnedPrize;
    public ParticleSystem Gotten;
    public AudioSource BubblePopped;
    public AudioClip BubbleSound;
    public int Bubbletype;
    public bool IsEnemyBubble;

    void Start(){

        switch (Bubbletype){
        case 0:
        UnlockObject = GameObject.Find("Botlock101").GetComponent<Botslock>();
        UnlockObjectEnemy = GameObject.Find("Botlock101").GetComponent<StoryModeEnemylock>();
        break;

        case 1:
        UnlockObject = GameObject.Find("Botlock102").GetComponent<Botslock>();
        break;

        case 2:
        UnlockObject = GameObject.Find("botlock104").GetComponent<Botslock>();
        break;

        default:
        Debug.Log("Invalid Bubbletype");
        break;
        }

      if(PlayerPrefs.GetInt(Playerpref) == 0){
       Prize.SetActive(true);
       OwnedPrize.SetActive(false);
      }
      else{
       Prize.SetActive(false);
       OwnedPrize.SetActive(true);
      }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            BubblePopped.volume = audioVolume * masterVolume;
            BubblePopped.PlayOneShot(BubbleSound);

            PlayerPrefs.SetInt(Playerpref,1);
            Gotten.Play();
            gameObject.SetActive(false);
            UnlockObject.UpdateButtonStates();
            if(Bubbletype == 0){
            UnlockObjectEnemy.RefreshEnemyLocks();
            }
        }
    }

    //void Delay()
    //{
     //UnlockObjectEnemy.RefreshEnemyLocks();
    //}


}
