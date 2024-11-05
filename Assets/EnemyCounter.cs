using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TextMeshPro Test;
    public Slider BounceMul;

    void Update()
    {
        Test.text = GlobalData.GetEnemyCount().ToString();
        BounceMul.value = GlobalData.GetEnemyCount();
    }
}
