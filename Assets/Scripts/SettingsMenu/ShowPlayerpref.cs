using UnityEngine;
using TMPro;


public class ShowPlayerpref : MonoBehaviour
{
    public string playerPrefName;
    public TextMeshPro PlayerprefText;
    public TextMeshProUGUI PlayerprefTextUI;
    private int Textvalue;

    void Start()
    {
        Textvalue = PlayerPrefs.GetInt(playerPrefName, 0);
        Refresh();
    }

    void Refresh()
    {
        if (PlayerprefText != null) PlayerprefText.text = Textvalue.ToString();
        if (PlayerprefTextUI != null) PlayerprefTextUI.text = Textvalue.ToString();
        Invoke("Refresh",10f);
    }
}
