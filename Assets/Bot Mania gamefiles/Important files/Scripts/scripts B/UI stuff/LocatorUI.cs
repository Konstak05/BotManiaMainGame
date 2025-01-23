using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocatorUI : MonoBehaviour
{
    public Transform Player;
    public TextMeshPro X;
    public TextMeshPro Y;
    public TextMeshPro Z;
    void Update()
    {
        int Xaxis2 = Mathf.RoundToInt(Player.position.x);
        X.text = Xaxis2.ToString();

        int Yaxis2 = Mathf.RoundToInt(Player.position.y);
        Y.text = Yaxis2.ToString();

        int Zaxis2 = Mathf.RoundToInt(Player.position.z);
        Z.text = Zaxis2.ToString();
    }
}
