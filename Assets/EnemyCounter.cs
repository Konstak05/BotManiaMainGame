using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{

    public TextMeshPro Test;


    void Update()
    {
        Test.text = GlobalData.GetEnemyCount().ToString();
    }
}
