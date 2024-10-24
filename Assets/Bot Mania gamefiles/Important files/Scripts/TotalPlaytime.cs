using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TotalPlaytime : MonoBehaviour
{
     private static TotalPlaytime instance;
    private float interval = 60f;
    private int timerValue;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        timerValue = 0;
        DontDestroyOnLoad(gameObject);
        InvokeRepeating("AddToTimer", 0f, 1f);
    }

    private void AddToTimer()
    {
        timerValue++;
        if (timerValue >= interval)
        {
            PlayerPrefs.SetInt("Playtime", PlayerPrefs.GetInt("Playtime", 0) + 1);
            PlayerPrefs.Save();
            timerValue = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Screen.fullScreen = !Screen.fullScreen;
            PlayerPrefs.SetInt("Windowed", PlayerPrefs.GetInt("Windowed") == 0 ? 1 : 0);
        }
    }


    private void OnDestroy()
    {

    }
}