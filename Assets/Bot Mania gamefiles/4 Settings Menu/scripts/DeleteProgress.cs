using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteProgress : MonoBehaviour
{

    public Animator transition;
    public string SceneName;
    public Renderer ButtonRenderer;
    public Color ColorIn;
    public Color ColorOut;

    public void ColorEnter()
    {
    
        ButtonRenderer.material.color = ColorIn;
    }

    public void ColorExit()
    {
        ButtonRenderer.material.color = ColorOut;
    }

    public void DeleteButton()
    {
    transition.SetTrigger("Start"); 
    Invoke("DeleteSaveData", 1f);
    }


    public void DeleteSaveData(){        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
        }
}
