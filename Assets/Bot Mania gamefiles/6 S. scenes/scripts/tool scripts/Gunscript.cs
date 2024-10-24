using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gunscript : MonoBehaviour
{
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
    public float scrollValue;
    public float scrollValue2;
    public bool IsTutorialMode2;
    public WeaponScript WeaponScript;
    public objPickup2 Playerpickupscript;
    public int GunEquipped;

    public int selectedIndex = 0;

    void Start()
    {

      if(!IsTutorialMode2){
      selectedIndex = 0;
      GunEquipped = values[selectedIndex];
      objectsToShow[selectedIndex].SetActive(true);
      ShowSelectedObject();
      }

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => {
                selectedIndex = index;
                if(!IsTutorialMode2){
                GunEquipped = values[selectedIndex];
                }
                ShowSelectedObject();
            });
        }

        for (int j = 1; j < objectsToShow.Length; j++)
        {
            objectsToShow[j].SetActive(false);
        }

      RefreshLockers();
      if(IsTutorialMode2){
      objectsToShow[0].SetActive(false);
      }
    }

    void Update()
    {
        scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue > 0f && Playerpickupscript.Grabbing == 0 && WeaponScript.GuardingBot == 0 && IsHealing.IsHealingPlayer == 0 && ToggleUI.PauseMenu == 0 && PlayerPrefs.GetInt("HasUnlockedWeapons") == 1)
        {
            if(PlayerPrefs.GetInt("C08") == 1){
            if(selectedIndex > values.Length){
            selectedIndex = 0;
            }
            else{
            selectedIndex = (selectedIndex + 1) % values.Length;
            }
            scrollValue2 = 1f;
            ShowSelectedObject();
            }
        }
        else if (scrollValue < 0f && Playerpickupscript.Grabbing == 0 && WeaponScript.GuardingBot == 0 && IsHealing.IsHealingPlayer == 0 && ToggleUI.PauseMenu == 0 && PlayerPrefs.GetInt("HasUnlockedWeapons") == 1)
        {
            if(PlayerPrefs.GetInt("C08") == 1){
            if(selectedIndex > 0){
            selectedIndex = selectedIndex - 1;
            }
            else{
            selectedIndex = values.Length - 1;
            }
             scrollValue2 = -1f;
            ShowSelectedObject();
            }
        }
    }

    void ShowSelectedObject()
    {
        if(IsUnlocked[selectedIndex] == 1){
            GunEquipped = values[selectedIndex];
        for (int j = 0; j < objectsToShow.Length; j++)
        {
            if (j == selectedIndex)
            {
                objectsToShow[j].SetActive(true);
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume * 0.5f;
                Sound.PlayOneShot(mySoundClip);
                scrollValue = 0f;
            }
            else
            {
                objectsToShow[j].SetActive(false);
                if(WeaponScript.GuardingBot == 1){
                WeaponScript.GuardingBot = 0;
                }
            }
        }
        }

        if(IsUnlocked[selectedIndex] == 0 && scrollValue2 == 1f){
            selectedIndex = (selectedIndex + 1) % values.Length;
            ShowSelectedObject();
        }
        if(IsUnlocked[selectedIndex] == 0 && scrollValue2 == -1f){
            if(selectedIndex > 0){
            selectedIndex = selectedIndex - 1;
            }
            else{
            selectedIndex = values.Length - 1;
            }
            ShowSelectedObject();
        }
    }


    public void RefreshLockers()
    {for (int w = 0; w < WeaponIDs.Length; w++){IsUnlocked[w] = PlayerPrefs.GetInt(WeaponIDs[w]);}}

    public void BasicGun()
    {
     GunEquipped = 1;
     selectedIndex = 1;
     RefreshHoldingWeapon();
    }

    public void BasicPhysgun()
    {
     GunEquipped = 0;
     selectedIndex = 0;
     RefreshHoldingWeapon();
    }

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

    public void RefreshHoldingWeapon()
    {    
        for (int j = 0; j < objectsToShow.Length; j++){
            if (j == selectedIndex){
                objectsToShow[j].SetActive(true);
                scrollValue = 0f;}
            else{
                objectsToShow[j].SetActive(false);
                if(WeaponScript.GuardingBot == 1){WeaponScript.GuardingBot = 0;}
                }
        }
    }
}