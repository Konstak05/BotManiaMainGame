using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slidersaver : MonoBehaviour
{
    public string PlayerPref;
    public int If;
    public int Turn;
    public Slider BounceMul;
    void Start()
    {
        if(PlayerPrefs.GetFloat(PlayerPref) <= If){
        BounceMul.value = Turn;
        }
        else{
        BounceMul.value = PlayerPrefs.GetFloat(PlayerPref);
        }
    }


    public void OnValueChanged()
    {
      if(BounceMul.value == If | PlayerPrefs.GetFloat(PlayerPref) == If)
      {
        PlayerPrefs.SetFloat(PlayerPref,Turn);  
      }
      else{

      
      PlayerPrefs.SetFloat(PlayerPref,BounceMul.value);  
      }
    }


}
