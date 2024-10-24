using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSelectorOUT : MonoBehaviour
{
    public GameObject[] maps;
    public string PlayerPrefID;
    public int PlayerprefIDindex;

    void Start()
    {
        PlayerprefIDindex = PlayerPrefs.GetInt(PlayerPrefID);

        for (int j = 0; j < maps.Length; j++)
        {
            if (j == PlayerprefIDindex)
            {
                maps[j].SetActive(true);
            }
            else
            {
                maps[j].SetActive(false);
            }
        }   
    }

}
