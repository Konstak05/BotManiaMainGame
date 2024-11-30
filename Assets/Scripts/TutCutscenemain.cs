using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutCutscenemain : MonoBehaviour
{
    public AudioSource Audio;
    public AudioSource Audio1;
    public AudioSource Audio2;


    public GameObject Text0;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;
    public GameObject Text5;

    public string SceneName;

    void Start() {//Tutorialspin.SetActive(false); 
    Invoke("StartCutscene", 3f);}

    void StartCutscene(){
    Audio1.Play();
    Text0.SetActive(true);
    Invoke("StartCutscene2", 1f);
    }
    void StartCutscene2(){
    Invoke("StartCutscene3", 2f);
    }
    void StartCutscene3(){
    Audio1.Play();
    Text0.SetActive(false);
    Invoke("StartText", 2f);
    }


    void StartText(){
        Audio.Play();
        Text1.SetActive(true);
    Invoke("StartText2", 5f);
    }

    void StartText2(){
        Text1.SetActive(false);
    Invoke("StartText3", 2f);
    }

    void StartText3(){
        Audio.Play();
        Text2.SetActive(true);
    Invoke("StartText4", 5f);
    }

    void StartText4(){
        Text2.SetActive(false);
        Invoke("StartText5", 3f);
    }

    void StartText5(){
        Audio.Play();
        Text3.SetActive(true);
        Invoke("StartText6", 6f);
    }

    void StartText6(){
        Text3.SetActive(false);
        Invoke("StartText7", 3f);
    }

    void StartText7(){
        Audio.Play();
        Text4.SetActive(true);
        Invoke("StartText8", 6f);
    }

    void StartText8(){
        Text4.SetActive(false);
        Invoke("StartText9", 2f);
    }

    void StartText9(){
        Audio.Play();
        Text5.SetActive(true);
        Audio2.Stop();
        Invoke("StartText10", 7f);
    }

    void StartText10(){
        Audio.Play();
        Text5.SetActive(false);
        Invoke("ChangeScene", 2f);
    }

    void ChangeScene(){
    SceneManager.LoadScene(SceneName);
    }
}
