using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrierScript : MonoBehaviour
{
    
    public GameObject Wall;
    public bool IsNegative;

    void Update()
    {
    if(!IsNegative){
    if(GlobalData.GetEnemyCount() > 0){Wall.SetActive(true);}
    else if(GlobalData.GetEnemyCount() < 1){Wall.SetActive(false);}
    }
    if(IsNegative){
    if(GlobalData.GetEnemyCount() < 1){Wall.SetActive(true);}
    else if(GlobalData.GetEnemyCount() > 0){Wall.SetActive(false);}
    }
    }
}
