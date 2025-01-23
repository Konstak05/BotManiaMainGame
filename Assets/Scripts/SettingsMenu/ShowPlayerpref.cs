using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShowPlayerpref : MonoBehaviour
{
    public string playerPrefName;
    public TextMeshPro PlayerprefText;
    private int Textvalue;

    void Start()
    {
        Textvalue = PlayerPrefs.GetInt(playerPrefName, 0);
        Invoke("Refresh",0f);
        
    }

    void Refresh()
    {
    PlayerprefText.text = Textvalue.ToString();
    Invoke("Refresh",10f);
    }
}
