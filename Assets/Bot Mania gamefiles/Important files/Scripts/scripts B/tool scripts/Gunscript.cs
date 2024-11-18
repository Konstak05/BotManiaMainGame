using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Gunscript : MonoBehaviour
{
    //InputControls
    public PlayerInput PlayerInputStarter;
    public InputAction ScrollUpAction;
    public InputAction ScrollDownAction;
    public bool scrollUpKey;
    public bool scrollDownKey;
    public int FixedDelay;

    public AudioSource Sound;
    public ToggleUI ToggleUI;
    public AudioClip mySoundClip;
    public float volume;
    public Button[] buttons;
    public int[] values;
    public GameObject[] objectsToShow;
    public int[] IsUnlocked;
    public string[] WeaponIDs;
    public WeaponScript IsHealing;
    public float scrollValueSuccessful;
    public bool IsTutorialMode2;
    public WeaponScript WeaponScript;
    public objPickup2 Playerpickupscript;
    public int GunEquipped;

    public int selectedIndex = 0;

    void Start(){
        ScrollUpAction = PlayerInputStarter.actions["ScrUp"];
        ScrollDownAction = PlayerInputStarter.actions["ScrDown"];

        if(!IsTutorialMode2){
            selectedIndex = 0;
            GunEquipped = values[selectedIndex];
            objectsToShow[selectedIndex].SetActive(true);
            ShowSelectedObject();
        }
        for (int i = 0; i < buttons.Length; i++){
            int index = i;
            buttons[i].onClick.AddListener(() => {
                selectedIndex = index;
                if(!IsTutorialMode2){GunEquipped = values[selectedIndex];}
                ShowSelectedObject();
            });
        }
        for (int j = 1; j < objectsToShow.Length; j++){objectsToShow[j].SetActive(false);}
        RefreshLockers();
        if(IsTutorialMode2){objectsToShow[0].SetActive(false); GunEquipped = 999;}
    }

    void Update(){
        scrollUpKey = ScrollUpAction.IsPressed();
        scrollDownKey = ScrollDownAction.IsPressed();
        if (scrollUpKey && FixedDelay == 0 && Playerpickupscript.Grabbing == 0 && WeaponScript.GuardingBot == 0 && IsHealing.IsHealingPlayer == 0 && ToggleUI.PauseMenu == 0 && PlayerPrefs.GetInt("HasUnlockedWeapons") == 1){
            if(PlayerPrefs.GetInt("C08") == 1){
                if(selectedIndex > values.Length){selectedIndex = 0;}
                else{selectedIndex = (selectedIndex + 1) % values.Length;}
                scrollValueSuccessful = 1f;
                ShowSelectedObject();
                FixedDelay = 1; 
                Invoke("FixedDelayforWeaponSwapping",0.2f);
            }
        }
        else if (scrollDownKey && FixedDelay == 0 && Playerpickupscript.Grabbing == 0 && WeaponScript.GuardingBot == 0 && IsHealing.IsHealingPlayer == 0 && ToggleUI.PauseMenu == 0 && PlayerPrefs.GetInt("HasUnlockedWeapons") == 1){
            if(PlayerPrefs.GetInt("C08") == 1){
                if(selectedIndex > 0){selectedIndex = selectedIndex - 1;}
                else{selectedIndex = values.Length - 1;}
                scrollValueSuccessful = -1f;
                ShowSelectedObject();
                FixedDelay = 1;
                Invoke("FixedDelayforWeaponSwapping",0.2f);
            }
        }
    }
    void ShowSelectedObject(){
        if(IsUnlocked[selectedIndex] == 1){
            GunEquipped = values[selectedIndex];
            for (int j = 0; j < objectsToShow.Length; j++){
                if (j == selectedIndex){
                    objectsToShow[j].SetActive(true);

                    //SoundEffect
                    SoundVolumeUpdater();
                    Sound.PlayOneShot(mySoundClip);
                }
                else{
                    objectsToShow[j].SetActive(false);
                    if(WeaponScript.GuardingBot == 1){WeaponScript.GuardingBot = 0;}
                }
            }
        }
        if(IsUnlocked[selectedIndex] == 0 && scrollValueSuccessful == 1f){
            selectedIndex = (selectedIndex + 1) % values.Length;
            ShowSelectedObject();
        }
        if(IsUnlocked[selectedIndex] == 0 && scrollValueSuccessful == -1f){
            if(selectedIndex > 0){selectedIndex = selectedIndex - 1;}
            else{selectedIndex = values.Length - 1;}
            ShowSelectedObject();
        }
    }

    void SoundVolumeUpdater(){
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Sound.volume = audioVolume * masterVolume;
    }

    void FixedDelayforWeaponSwapping(){FixedDelay = 0;}

    public void RefreshLockers(){for (int w = 0; w < WeaponIDs.Length; w++){IsUnlocked[w] = PlayerPrefs.GetInt(WeaponIDs[w]);}}

    public void BasicPhysgun(){GunEquipped = 0; selectedIndex = 0; RefreshHoldingWeapon();}
    public void BasicGun(){GunEquipped = 1; selectedIndex = 1; RefreshHoldingWeapon();}
    public void CreativeTool1(){GunEquipped = 2;  selectedIndex = 2; RefreshHoldingWeapon();}
    public void CreativeTool2(){GunEquipped = 3;  selectedIndex = 3; RefreshHoldingWeapon();}
    public void CreativeTool3(){GunEquipped = 4;  selectedIndex = 4; RefreshHoldingWeapon();}
    public void CreativeTool4(){GunEquipped = 5;  selectedIndex = 5; RefreshHoldingWeapon();}
    public void CreativeTool5(){GunEquipped = 6;  selectedIndex = 6; RefreshHoldingWeapon();}
    public void CreativeTool6(){GunEquipped = 10;  selectedIndex = 7; PlayerPrefs.SetInt("HasUnlockedWeapons",1); RefreshHoldingWeapon();}
    public void CreativeTool7(){GunEquipped = 9;  selectedIndex = 10; RefreshHoldingWeapon();}
    public void CreativeTool8(){GunEquipped = 7;  selectedIndex = 9; RefreshHoldingWeapon();}
    public void CreativeTool9(){GunEquipped = 8;  selectedIndex = 8; RefreshHoldingWeapon();}
    public void CreativeTool10(){GunEquipped = 11;  selectedIndex = 11; RefreshHoldingWeapon();}

    public void RefreshHoldingWeapon(){    
        for (int j = 0; j < objectsToShow.Length; j++){
            if (j == selectedIndex){
                objectsToShow[j].SetActive(true);
            }
            else{
                objectsToShow[j].SetActive(false);
                if(WeaponScript.GuardingBot == 1){WeaponScript.GuardingBot = 0;}
            }
        }
    }
}