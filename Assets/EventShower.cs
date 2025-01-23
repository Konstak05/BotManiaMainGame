using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventShower : MonoBehaviour
{
    public GameObject Map;
    public DayOfWeek[] DaysToShow;
    void Start()
    {
        DayOfWeek currentDay = System.DateTime.Now.DayOfWeek;
        for(int day = 0; day < DaysToShow.Length; day++)
            if(currentDay == DaysToShow[day]){
                Map.SetActive(true);
                day = DaysToShow.Length;
            }
            else
            {
                Map.SetActive(false);
            }
        }
    }


