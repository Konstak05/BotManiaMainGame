using UnityEngine;
using UnityEngine.UI;

public class SettingsSelectNew : MonoBehaviour
{
    public GameObject[] uiElements;
    public GameObject[] uiElementsoutline;
    private bool startClosed = true;

    void Start()
    {
        if (startClosed)
        {
            for (int i = 0; i < uiElements.Length; i++)
            {
                uiElements[i].SetActive(i == 0);
            }
            for (int i = 0; i < uiElementsoutline.Length; i++)
            {
                uiElementsoutline[i].SetActive(i == 0);
            }
        }
    }

    //Simple settings UI changer. Should be removed later
    public void ToggleUI1(){
        int index = 0; 
        for (int i = 0; i < uiElements.Length; i++){uiElements[i].SetActive(i == index);}
        for (int i = 0; i < uiElementsoutline.Length; i++){uiElementsoutline[i].SetActive(i == index);}
    }
    public void ToggleUI2(){
        int index = 1; 
        for (int i = 0; i < uiElements.Length; i++){uiElements[i].SetActive(i == index);}
        for (int i = 0; i < uiElementsoutline.Length; i++){uiElementsoutline[i].SetActive(i == index);}
    }
    public void ToggleUI3(){
        int index = 2; 
        for (int i = 0; i < uiElements.Length; i++){uiElements[i].SetActive(i == index);}
        for (int i = 0; i < uiElementsoutline.Length; i++){uiElementsoutline[i].SetActive(i == index);}
    }
    public void ToggleUI4(){
        int index = 3; 
        for (int i = 0; i < uiElements.Length; i++){uiElements[i].SetActive(i == index);}
        for (int i = 0; i < uiElementsoutline.Length; i++){uiElementsoutline[i].SetActive(i == index);}
    }
    public void ToggleUI5(){
        int index = 4; 
        for (int i = 0; i < uiElements.Length; i++){uiElements[i].SetActive(i == index);}
        for (int i = 0; i < uiElementsoutline.Length; i++){uiElementsoutline[i].SetActive(i == index);}
    }
}