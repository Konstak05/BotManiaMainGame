using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItoggleBBmk2 : MonoBehaviour
{
    public Button toggleButton;

    public Animator MenuAnim;
    public float IsActivated;
    public bool IsActive;

    void Start()
    {
        IsActivated = 1;
        toggleButton.onClick.AddListener(TogglingTheMenu);
    }


    void TogglingTheMenu()
    {
        IsActive = !IsActive;
    }


    void Update()
    {
        MenuAnim.SetFloat("Rotate 0", IsActivated);

        if(!IsActive){
            if(IsActivated < 1f){
                IsActivated += 0.1f;
            }
        }
        if(IsActive){
            if(IsActivated > 0f){
                IsActivated -= 0.1f;
            }
        }
    }
}
