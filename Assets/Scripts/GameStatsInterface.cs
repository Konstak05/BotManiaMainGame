using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatsInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    void Start()
    {
        InvokeRepeating("Refresh", 0, 10);
    }

    void Refresh()
    {
        string t = "Bots spawned:{0}\nTime spend (M):{1}";
        text.text = string.Format(t, PlayerPrefs.GetInt("BotBouncerBots"), PlayerPrefs.GetInt("Playtime"));
    }
}
