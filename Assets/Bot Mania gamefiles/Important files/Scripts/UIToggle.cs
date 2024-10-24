using UnityEngine;
using UnityEngine.UI;


public class UIToggle : MonoBehaviour
{
    public GameObject myUI;
    public Button toggleButton;
    public bool StartClosed = false;
    private bool isUIVisible = false;

    void Start()
    {
        if(StartClosed){
        myUI.SetActive(false);
        }
        else{
        isUIVisible = true;
        }
        toggleButton.onClick.AddListener(ToggleUI);
    }

    void ToggleUI()
    {
        isUIVisible = !isUIVisible;
        myUI.SetActive(isUIVisible);
    }
}