using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLocationLocker : MonoBehaviour
{
    public GameObject[] Location;
    public GameObject[] locks;
    public string[] playerPrefsKeys;

    private void Awake()
    {
        UpdateLocationStates();
    }

    public void UpdateLocationStates()
    {
        for (int i = 0; i < Location.Length; i++)
        {
            int playerPrefValue = PlayerPrefs.GetInt(playerPrefsKeys[i]);

            if (playerPrefValue == 0)
            {
                Location[i].SetActive(false);
                locks[i].SetActive(true);
            }
            else
            {
                Location[i].SetActive(true);
                locks[i].SetActive(false);
            }
        }
    }
}
