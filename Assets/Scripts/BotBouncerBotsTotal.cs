using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotBouncerBotsTotal : MonoBehaviour
{
    [SerializeField] private string playerPrefName = "BotBouncerBots";
    public Text BBBText;
    private int BBBValue;

    void Update()
    {
        BBBValue = PlayerPrefs.GetInt(playerPrefName, 0);
        UpdateTimerText2();
    }

    private void UpdateTimerText2()
    {
        BBBText.text = BBBValue.ToString();
    }
}