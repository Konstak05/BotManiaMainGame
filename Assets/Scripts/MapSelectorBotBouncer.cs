using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapSelectorBotBouncer : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;
    public int Index;
    public GameObject[] maps;
    public TextMeshPro MapText;
    public string[] MapNames;

    void Start()
    {
        for (int j = 0; j < maps.Length; j++)
        {
            if (j == Index)
            {
                maps[j].SetActive(true);
                MapText.text = MapNames[j];
            }
            else
            {
                maps[j].SetActive(false);
            }
        }   
    }

    public void LeftButtonFunction()
    {
        if(Index > 0){
        Index = Index - 1;
        }
        else{
        Index = maps.Length - 1;
        }

        for (int j = 0; j < maps.Length; j++)
        {
            if (j == Index)
            {
                maps[j].SetActive(true);
                MapText.text = MapNames[j];
            }
            else
            {
                maps[j].SetActive(false);
            }
        }
    }

    public void RightButtonFunction()
    {
        if(Index == maps.Length - 1){
        Index = 0;
        }
        else{
        Index += 1;
        }


        for (int j = 0; j < maps.Length; j++)
        {
            if (j == Index)
            {
                maps[j].SetActive(true);
                MapText.text = MapNames[j];
            }
            else
            {
                maps[j].SetActive(false);
            }
        }
    }
}
