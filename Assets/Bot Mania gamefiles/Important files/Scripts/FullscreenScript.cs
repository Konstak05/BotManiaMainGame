using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FullscreenScript : MonoBehaviour
{
    public PlayerInput PlayerInputStarter;
    public InputAction Fullscreenaction;
    public bool FullscreenKey;

    private static FullscreenScript instance;

    private void Start()
    {
        Fullscreenaction = PlayerInputStarter.actions["Fullscreen"];
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        FullscreenKey = Fullscreenaction.WasPressedThisFrame();
        if (FullscreenKey)
        {
            Screen.fullScreen = !Screen.fullScreen;
            PlayerPrefs.SetInt("Windowed", PlayerPrefs.GetInt("Windowed") == 0 ? 1 : 0);
        }
    }
}