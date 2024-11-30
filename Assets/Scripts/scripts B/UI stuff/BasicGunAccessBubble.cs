using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGunAccessBubble : MonoBehaviour
{
    public string Playerpref;
    public Gunscript UnlockObject;
    public Botslock UnlockObject2;
    public GameObject Prize;
    public GameObject OwnedPrize;
    public ParticleSystem Gotten;
    public AudioSource BubblePopped;
    public AudioClip BubbleSound;
    public GameObject Barrier;
    public int GunId;

    private PopupMain PopupMain1;
    public string TextNameReplacer;
    public string TextDescReplacer;

    void Start(){
        UnlockObject = GameObject.Find("CameraArea223").GetComponent<Gunscript>();
        UnlockObject2 = GameObject.Find("botlock103").GetComponent<Botslock>();
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

            Gotten.Play();
            gameObject.SetActive(false);
            if(Barrier != null){
            Barrier.SetActive(false);
            }
            PlayerPrefs.SetInt(Playerpref,1);
            UnlockObject.RefreshLockers();

            switch (GunId)
            {
                case 0:
                UnlockObject.BasicPhysgun();
                break;

                case 1:
                UnlockObject.BasicGun();
                break;

                case 2:
                UnlockObject.CreativeTool1();
                break;

                case 3:
                UnlockObject.CreativeTool2();
                break;

                case 4:
                UnlockObject.CreativeTool3();
                break;

                case 5:
                UnlockObject.CreativeTool4();
                break;

                case 6:
                UnlockObject.CreativeTool5();
                break;

                case 7:
                UnlockObject.CreativeTool7();
                break;

                case 8:
                UnlockObject.CreativeTool8();
                break;

                case 9:
                UnlockObject.CreativeTool9();
                break;

                case 10:
                UnlockObject.CreativeTool6();
                break;

                case 11:
                UnlockObject.CreativeTool10();
                break;

                default:
                Debug.Log("Error");
                break;
            }



            UnlockObject2.UpdateButtonStates();
        }
    }




}
