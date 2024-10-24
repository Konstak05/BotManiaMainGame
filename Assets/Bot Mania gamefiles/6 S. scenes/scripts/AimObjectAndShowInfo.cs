using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AimObjectAndShowInfo : MonoBehaviour
{

    public string names;
    public string description;
    public TextMeshProUGUI nameTextMeshPro;
    public TextMeshProUGUI descriptionTextMeshPro;

    public void Enter()
    {
      nameTextMeshPro.text = names;
      descriptionTextMeshPro.text = description;
    }
    public void Exit()
    {
     nameTextMeshPro.text = " ";
     descriptionTextMeshPro.text = " ";
    }
}