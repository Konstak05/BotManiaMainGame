using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TextMeshPro EnemyCount;
    public Slider SliderMul;
    private float EnemyMeter;
    private int SafeStopper,SpottedStopper,Spotted2Stopper,Spotted3Stopper;
    public GameObject SafeSign,SpottedSign,Spotted2Sign,Spotted3Sign;

    void Start(){
        SafeSign.SetActive(true); SpottedSign.SetActive(false); Spotted2Sign.SetActive(false); Spotted3Sign.SetActive(false);
    }
    void Update()
    {
        EnemyCount.text = GlobalData.GetEnemyCount().ToString();
        SliderMul.value = GlobalData.GetEnemyCount() + 4;
        if(SliderMul.value < 5 && SafeStopper == 0){
            Debug.Log("Safe"); 
            SafeStopper = 1; SpottedStopper = 0; Spotted2Stopper = 0; Spotted3Stopper = 0;
            SafeSign.SetActive(true); SpottedSign.SetActive(false); Spotted2Sign.SetActive(false); Spotted3Sign.SetActive(false);
        }
        if(SliderMul.value >= 5 && SliderMul.value < 10 && SpottedStopper == 0){
            Debug.Log("Spotted"); 
            SafeStopper = 0; SpottedStopper = 1; Spotted2Stopper = 0; Spotted3Stopper = 0;
            SafeSign.SetActive(false); SpottedSign.SetActive(true); Spotted2Sign.SetActive(false); Spotted3Sign.SetActive(false);
        }
        if(SliderMul.value >= 10 && SliderMul.value < 26 && Spotted2Stopper == 0){
            Debug.Log("You're screwed"); 
            SafeStopper = 0; SpottedStopper = 0; Spotted2Stopper = 2; Spotted3Stopper = 0;
            SafeSign.SetActive(false); SpottedSign.SetActive(false); Spotted2Sign.SetActive(true); Spotted3Sign.SetActive(false);
        }
        if(SliderMul.value >= 26 && Spotted3Stopper == 0){
            Debug.Log("Skull"); 
            SafeStopper = 0; SpottedStopper = 0; Spotted2Stopper = 0; Spotted3Stopper = 1;
            SafeSign.SetActive(false); SpottedSign.SetActive(false); Spotted2Sign.SetActive(false); Spotted3Sign.SetActive(true);
        }
    }
}
