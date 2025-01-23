using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceMulsaver : MonoBehaviour
{
    public Slider BounceMul;
    void Start()
    {
        BounceMul.value = PlayerPrefs.GetFloat("BounceMul");
    }


    void Update()
    {
      if(BounceMul.value == 0f | PlayerPrefs.GetFloat("BounceMul") == 0f)
      {
        PlayerPrefs.SetFloat("BounceMul",1);  
      }
      else{

      
      PlayerPrefs.SetFloat("BounceMul",BounceMul.value);  
      }
    }


}
