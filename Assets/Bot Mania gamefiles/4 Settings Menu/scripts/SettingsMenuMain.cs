using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuMain : MonoBehaviour
{
    public float volume;
    public GameObject[] uiElements;
    public GameObject[] uiElementsButton;
    public Button[] toggleButtons;
    private bool startClosed = true;
    public float Soundreset = 2f;
    public AudioClip Clip;
    public AudioSource Sound;

    void Start()
    {
        if (startClosed)
        {
            for (int i = 0; i < uiElements.Length; i++)
            {
                uiElements[i].SetActive(i == 0);
            }
            for (int i = 0; i < uiElementsButton.Length; i++)
            {
                uiElementsButton[i].SetActive(i == 0);
            }
        }
        else
        {
        }

        for (int i = 0; i < toggleButtons.Length; i++)
        {
            int index = i; // capture the value of i in a local variable
            toggleButtons[i].onClick.AddListener(() =>
            {
                ToggleUI(index);
            });
        }
    }

    public void Update()
    {
      if(Soundreset < 2f){
         Soundreset += 0.1f;
      }
    }

    public void ToggleUI(int index)
    {
        if(Soundreset >= 2f){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
        Sound.PlayOneShot(Clip);

        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(i == index);
        }
        for (int i = 0; i < uiElementsButton.Length; i++)
        {
            uiElementsButton[i].SetActive(i == index);
        }
        Soundreset = 0f;
        }
    }
}