using UnityEngine;

public class LimitResolution : MonoBehaviour
{
    private void Awake()
    {
        bool windowed = PlayerPrefs.GetInt("Windowed") == 0;

        if (PlayerPrefs.GetFloat("Window") == 2)
        {
            Screen.SetResolution(960, 540, windowed);
        }
        else if (PlayerPrefs.GetFloat("Window") == 1)
        {
            Screen.SetResolution(1280, 720, windowed);
        }
        else if (PlayerPrefs.GetFloat("Window") == 0)
        {
            Screen.SetResolution(1920, 1080, windowed);
        }
        else if (PlayerPrefs.GetFloat("Window") == 3)
        {
            Screen.SetResolution(400, 225, windowed);
        }
        else if (PlayerPrefs.GetFloat("Window") == 4)
        {
            Screen.SetResolution(640, 360, windowed);
        }
        else if (PlayerPrefs.GetFloat("Window") == 5)
        {
            Screen.SetResolution(800, 450, windowed);
        }
        else if (PlayerPrefs.GetFloat("Window") == 6)
        {
            Screen.SetResolution(1024, 576, windowed);
        }
    }
}