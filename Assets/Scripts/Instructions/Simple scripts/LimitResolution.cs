using UnityEngine;

public class LimitResolution : MonoBehaviour
{
    private void Awake()
    {
        bool windowed = PlayerPrefs.GetInt("Windowed") == 0;

        float windowMode = PlayerPrefs.GetFloat("Window");

        switch (windowMode)
        {
            case 0: Screen.SetResolution(1920, 1080, windowed); break;
            case 1: Screen.SetResolution(1280, 720, windowed); break;
            case 2: Screen.SetResolution(960, 540, windowed); break;
            case 3: Screen.SetResolution(400, 225, windowed); break;
            case 4: Screen.SetResolution(640, 360, windowed); break;
            case 5: Screen.SetResolution(800, 450, windowed); break;
            case 6: Screen.SetResolution(640, 360, windowed); break;
            default: Screen.SetResolution(1920, 1080, windowed); break;
        }
    }
}