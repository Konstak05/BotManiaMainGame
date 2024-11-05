using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ToggleUI : MonoBehaviour
{
  public PlayerInput PlayerInputStarter;
  public InputAction EscAction;
  public bool EscKey;
  public AudioClip mySoundClip;
  public WeaponScript WeaponScript;
  public AudioSource Sound;
  public float volume;
  public GameObject uiPanel;
  public GameObject uiPanel2;
  public Light Light1;
  public Light Light2;
  public float Soundreset = 1f;
  public WeaponScript IsHealing;
  public GameObject Player;
  public int PauseMenu;

  //Escforspawnlist stuff
  public GameObject EscSign;
  public TextMeshProUGUI EscText;

  void Start(){
    EscAction = PlayerInputStarter.actions["OpenCreateMenu"];
    EscText.text = EscAction.GetBindingDisplayString() + " for Spawnlist";
    uiPanel.SetActive(false);
    uiPanel2.SetActive(false);
    PauseMenu = 0;
    Player = GameObject.Find("Player");     
    if(PlayerPrefs.GetInt("MenuUnlocked") == 0){
      EscSign.SetActive(false);
    }
  }

  void Update()
  {
    EscKey = EscAction.WasPressedThisFrame();
    if (EscKey && Player.GetComponent<KeyboardControlMk2>().HP > 0 && WeaponScript.GuardingBot == 0 && IsHealing.IsHealingPlayer == 0 && PlayerPrefs.GetInt("MenuUnlocked") == 1){
    if(Soundreset >= 1f){
      float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
      float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
      Sound.volume = audioVolume * masterVolume * 0.5f;
      Sound.PlayOneShot(mySoundClip);   
      uiPanel.SetActive(!uiPanel.activeSelf);
      Soundreset = 0f;
    }     
    if (!uiPanel.activeSelf)
    {
      PauseMenu = 0;
      EscSign.SetActive(true);
    }
    else
    {
      PauseMenu = 1;
      uiPanel2.SetActive(false);
      EscSign.SetActive(false);
    }
    }     
    if (EscKey && Player.GetComponent<KeyboardControlMk2>().HP > 0 && WeaponScript.GuardingBot == 0 && IsHealing.IsHealingPlayer == 0 && PlayerPrefs.GetInt("MenuUnlocked") == 0){
    if(Soundreset >= 1f){
      float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
      float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
      Sound.volume = audioVolume * masterVolume * 0.5f;
      Sound.PlayOneShot(mySoundClip);       
      uiPanel2.SetActive(!uiPanel2.activeSelf);
      Soundreset = 0f;
    }     
    if (!uiPanel2.activeSelf){
      PauseMenu = 0;
      EscSign.SetActive(false);
    }
    else{
      PauseMenu = 1;
      uiPanel.SetActive(false);
      EscSign.SetActive(false);
    }
    }     
    if(Player.GetComponent<KeyboardControlMk2>().HP <= 0){
      uiPanel.SetActive(false);
      PauseMenu = 0;
    }     
    if(Soundreset < 1f && Player.GetComponent<KeyboardControlMk2>().HP > 0){
       Soundreset += 0.1f;
    }
  }
}
