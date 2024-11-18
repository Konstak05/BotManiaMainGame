using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingToggle : MonoBehaviour
{
    public Toggle buttonToggle;
    public string Playerprefstring = "Toggle";
    private void Start()
    {

      buttonToggle.isOn = PlayerPrefs.GetInt(Playerprefstring, 0) == 1;
    }

    private void Update()
    {
        if (buttonToggle.isOn)
        {
         PlayerPrefs.SetInt(Playerprefstring, 1);
        }
        else
        {
         PlayerPrefs.SetInt(Playerprefstring, 0);
        }       
    }

    private void OnDestroy()
    {
      PlayerPrefs.GetInt(Playerprefstring);    
    }
}
