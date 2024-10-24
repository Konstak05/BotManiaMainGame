using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamemodeMenuSwitch : MonoBehaviour
{
    public float volume;
    public GameObject[] uiElements;
    public GameObject[] ActualButtons;
    public Button[] toggleButtons;
    private bool startClosed = true;
    public float Soundreset = 2f;
    public AudioClip Clip;
    public AudioSource Sound;
    public TextMeshPro TVscreen;
    public Material skyboxMaterial1;
    public Material skyboxMaterial2;

    void Start()
    {
        TVscreen.text = "Hello! Welcome to the terminal.Here you can choose what mode you want to play.";

        if (startClosed)
        {
            for (int i = 0; i < uiElements.Length; i++)
            {
                uiElements[i].SetActive(i == 0);
            }
        }
        else
        {
        }
    }

    public void Update()
    {
      if(Soundreset < 2f){
         Soundreset += 0.1f;
      }
    }

    public void ToggleUI()
    {
        if(Soundreset >= 2f){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip);

        uiElements[0].SetActive(true);
        uiElements[1].SetActive(false);
        uiElements[2].SetActive(false);
        uiElements[3].SetActive(false);
        uiElements[4].SetActive(false);
        Soundreset = 0f;
        TVscreen.text = "Still under construction. You cannot access it on this version.Please try again later!";
        RenderSettings.skybox = skyboxMaterial1;
        }
    }

    public void ToggleUI2()
    {
        if(Soundreset >= 2f){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip);

        uiElements[0].SetActive(false);
        uiElements[1].SetActive(true);
        uiElements[2].SetActive(false);
        uiElements[3].SetActive(false);
        uiElements[4].SetActive(false);
        Soundreset = 0f;

        TVscreen.text = "Sandbox gives you the ability to just mess around.It's the base game";
        RenderSettings.skybox = skyboxMaterial2;
        }
    }

    public void ToggleUI3()
    {
        if(Soundreset >= 2f){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip);

        uiElements[0].SetActive(false);
        uiElements[1].SetActive(false);
        uiElements[2].SetActive(true);
        uiElements[3].SetActive(false);
        uiElements[4].SetActive(false);

        Soundreset = 0f;

        TVscreen.text = "Extra content for you to play while you wait for actual game updates! It also includes the remastered version of Bot Bouncer.";
        }
    }

    public void ToggleUI4()
    {
        if(Soundreset >= 2f){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip);

        uiElements[0].SetActive(false);
        uiElements[1].SetActive(false);
        uiElements[2].SetActive(false);
        uiElements[3].SetActive(true);
        uiElements[4].SetActive(false);

        Soundreset = 0f;

        TVscreen.text = "Content that exist for limited time only! The game will have more content as time goes on";
        RenderSettings.skybox = skyboxMaterial2;
        }
    }

    public void ToggleUI5()
    {
        if(Soundreset >= 2f){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip);

        uiElements[0].SetActive(false);
        uiElements[1].SetActive(false);
        uiElements[2].SetActive(false);
        uiElements[3].SetActive(false);
        uiElements[4].SetActive(true);
        Soundreset = 0f;

        TVscreen.text = "Earn objects, weapons and other special items on the Bot Capsule! Here you can select which banner to go for. Remember that banners come in different days so remember to check it out once in a while!";
        RenderSettings.skybox = skyboxMaterial2;
        }
    }
}