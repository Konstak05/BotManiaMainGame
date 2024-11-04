using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class frameratecounter : MonoBehaviour
{
    public TextMeshPro Test;
    // Update is called once per frame
    void Update()
    {
        Test.text = Time.deltaTime.ToString();
    }
}
