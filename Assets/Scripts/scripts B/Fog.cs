using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fog : MonoBehaviour
{

    public Color ColorIn;
    public Color ColorOut;
    public Renderer ButtonRenderer;
    public Renderer ButtonRenderer2;

    public void Start()
    {
      if(PlayerPrefs.GetFloat("Fog") == 0f)
      {
          RenderSettings.fog = true;
      }
      if(PlayerPrefs.GetFloat("Fog") == 1f)
      {
          RenderSettings.fog = false;
      }
    }

    public void ShowFog()
    {
       PlayerPrefs.SetFloat("Fog",0f);
          RenderSettings.fog = true;
    }

    public void hideFog()
    {
       PlayerPrefs.SetFloat("Fog",1f);
          RenderSettings.fog = false;
    }


    public void ColorClick()
    {
    
        ButtonRenderer.material.color = ColorIn;
        Invoke("ColorClick2",0.2f);
    }

    public void ColorClick2()
    {
    
        ButtonRenderer.material.color = ColorOut;
    }

    public void ColorClick3()
    {
    
        ButtonRenderer2.material.color = ColorIn;
        Invoke("ColorClick4",0.2f);
    }

    public void ColorClick4()
    {
    
        ButtonRenderer2.material.color = ColorOut;
    }

}
