using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletBrightness : MonoBehaviour
{
    public string PlayerPref;
    public Light Light;
    public int If;
    public int Turn;
    public Slider BounceMul;

    void Start()
    {
        if(PlayerPrefs.GetFloat(PlayerPref) == 0){
        BounceMul.value = 1;
        }
        else{
        BounceMul.value = PlayerPrefs.GetFloat(PlayerPref);
        }
    }


    public void OnValueChanged()
    {
      if(PlayerPrefs.GetFloat(PlayerPref) == If)
      {
        PlayerPrefs.SetFloat(PlayerPref,Turn);  

      }
      else{

      Light.intensity = BounceMul.value;
      PlayerPrefs.SetFloat(PlayerPref,BounceMul.value);  
      }
    }


}
