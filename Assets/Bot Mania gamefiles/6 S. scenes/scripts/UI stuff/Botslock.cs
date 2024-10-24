using UnityEngine;
using UnityEngine.UI;

public class Botslock : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] locks;
    public string[] playerPrefsKeys;

    private void Awake()
    {
        UpdateButtonStates();
    }

    public void UpdateButtonStates()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int playerPrefValue = PlayerPrefs.GetInt(playerPrefsKeys[i]);

            if (playerPrefValue == 0)
            {
                buttons[i].gameObject.SetActive(false);
                locks[i].gameObject.SetActive(true);
            }
            else
            {
                buttons[i].gameObject.SetActive(true);
                locks[i].gameObject.SetActive(false);
            }
        }
    }
}