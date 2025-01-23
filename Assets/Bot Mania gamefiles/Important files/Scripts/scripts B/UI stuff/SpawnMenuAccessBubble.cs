using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenuAccessBubble : MonoBehaviour
{
    public string Playerpref;
    public ToggleUI UnlockObject;
    public GameObject Prize;
    public GameObject OwnedPrize;
    public ParticleSystem Gotten;
    public AudioSource BubblePopped;
    public AudioClip BubbleSound;
    public GameObject Barrier;

    private PopupMain PopupMain1;
    public string TextNameReplacer;
    public string TextDescReplacer;

    void Start(){
        UnlockObject = GameObject.Find("Player").GetComponent<ToggleUI>();
        PopupMain1 = GameObject.Find("Popup left top").GetComponent<PopupMain>();

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
            
            PopupMain1.TextName.text = TextNameReplacer;
            PopupMain1.TextDesc.text = TextDescReplacer;
            PopupMain1.ShowText();

            if(PlayerPrefs.GetInt("MenuUnlocked") == 0){
            UnlockObject.EscSign.SetActive(true);
            }
            Gotten.Play();
            gameObject.SetActive(false);
            Barrier.SetActive(false);
            PlayerPrefs.SetInt(Playerpref,1);
        }
    }




}
