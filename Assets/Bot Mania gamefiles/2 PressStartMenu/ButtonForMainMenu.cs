using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonForMainMenu : MonoBehaviour
{
    public Animator transition;
    public string SceneName;
    public int HasStarted;
    public bool CanPress;

    void Start(){
    Application.targetFrameRate = 60;
    Invoke("CanPress2", 1);
    }


    void CanPress2()
    {
    CanPress = true;
    }


    void FixedUpdate()
    {
    if (Input.GetKeyDown(KeyCode.Return) && HasStarted == 0 && CanPress && PlayerPrefs.GetInt("FirstTime") == 1)
    {
    transition.SetTrigger("Start"); 
    Invoke("StartGame2", 1);
    HasStarted = 1;
    }       
    }

    void StartGame2()
    {
    Cursor.lockState = CursorLockMode.None;
    SceneManager.LoadScene(SceneName);

    }

    public void OpenDiscordLink()
    {
    Application.OpenURL("https://discord.gg/2c82552hZU");
    }

    public void OpenYoutubeLink()
    {
    Application.OpenURL("https://www.youtube.com/channel/UC5oykP-e-wX6H-MMIQbHvyA");
    }

    public void OpenTwitterLink()
    {
    Application.OpenURL("https://twitter.com/konstak05");
    }
}
