using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI HourText;

    void Start(){Invoke("Updatetime", 0f);}

    void Updatetime()
    {
        System.DateTime currentTime = System.DateTime.Now;
        HourText.text = (currentTime.Hour.ToString() + ":" + currentTime.Minute.ToString() + ":" + currentTime.Second.ToString());
        Invoke("Updatetime", 0.5f);
    }
}
