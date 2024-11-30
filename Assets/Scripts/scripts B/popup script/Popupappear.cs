using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popupappear : MonoBehaviour
{
    private PopupMain PopupMain1;
    public string TextNameReplacer;
    public string TextDescReplacer;
    public int isactive = 0;

    void Start()
    {
        PopupMain1 = GameObject.Find("Popup left top").GetComponent<PopupMain>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isactive == 0)
        {
            PopupMain1.TextName.text = TextNameReplacer;
            PopupMain1.TextDesc.text = TextDescReplacer;
            PopupMain1.ShowText();
            isactive = 1;
        }
    }

}
