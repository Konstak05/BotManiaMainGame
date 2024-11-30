using UnityEngine;
using UnityEngine.SceneManagement;


public class BGMMainMenu : MonoBehaviour
{
    private static BGMMainMenu instance;
    public AudioSource Area;
    public AudioSource Area2;
    public AudioSource Area3;
    public AudioSource Area4;
    private float IsMenu,IsMenu2,IsMenu3,IsMenu4;
    private int Jiggle;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        IsMenu = 1;
        IsMenu2 = 0;
        IsMenu3 = 0;

    
        Area.volume = MusicVolume * MasterVolume * IsMenu;
        Area2.volume = MusicVolume * MasterVolume * IsMenu2;
        Area3.volume = MusicVolume * MasterVolume/3 * IsMenu3;
        Area.loop = true;
        Area2.Play();
        Area2.loop = true;
        Area3.Play();   
        Area3.loop = true; 
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "BotBouncerMenu" | SceneManager.GetActiveScene().name == "PressStartMenu" | SceneManager.GetActiveScene().name == "MainMenu" | SceneManager.GetActiveScene().name == "SettingsMenu")
        {

        if(SceneManager.GetActiveScene().name == "PressStartMenu"){
        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        Area.volume = MusicVolume * MasterVolume * IsMenu;
        Area2.volume = MusicVolume * MasterVolume * IsMenu2;
        Area3.volume = MusicVolume * MasterVolume/3 * IsMenu3;
        Area4.volume = MusicVolume * MasterVolume * IsMenu4;

        if(IsMenu < 1){
            IsMenu = IsMenu + 0.01f;
        }
        if(IsMenu2 > 0){
            IsMenu2 = IsMenu2 - 0.01f;
        }
        if(IsMenu3 > 0){
            IsMenu3 = IsMenu3 - 0.01f;
        }
        if(IsMenu4 > 0){
            IsMenu4 = IsMenu4 - 0.01f;
        }

        if(Jiggle == 1){
        Jiggle = 0;
        Area4.Stop();
        }
        }


        if(SceneManager.GetActiveScene().name == "BotBouncerMenu" | SceneManager.GetActiveScene().name == "MainMenu"){
        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        Area.volume = MusicVolume * MasterVolume * IsMenu;
        Area2.volume = MusicVolume * MasterVolume * IsMenu2;
        Area3.volume = MusicVolume * MasterVolume/3 * IsMenu3;
        Area4.volume = MusicVolume * MasterVolume * IsMenu4;

        if(Input.GetKey("p")){

        if(Jiggle == 0){
        Area4.Play();
        Jiggle = 1;
        }

        if(IsMenu > 0){
            IsMenu = IsMenu - 0.01f;
        }
        if(IsMenu2 > 0){
            IsMenu2 = IsMenu2 - 0.01f;
        }
        if(IsMenu3 > 0){
            IsMenu3 = IsMenu3 - 0.01f;
        }
        if(IsMenu4 < 1){
            IsMenu4 = IsMenu4 + 0.01f;
        }

        }
        else{
        if(Jiggle == 1){
        Jiggle = 0;
        Area4.Stop();
        }

        if(IsMenu > 0){
            IsMenu = IsMenu - 0.01f;
        }
        if(IsMenu2 < 1){
            IsMenu2 = IsMenu2 + 0.01f;
        }
        if(IsMenu3 > 0){
            IsMenu3 = IsMenu3 - 0.01f;
        }
        if(IsMenu4 > 0){
            IsMenu4 = IsMenu4 - 0.01f;
        }
        }

        }
        if(SceneManager.GetActiveScene().name == "SettingsMenu"){
        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        Area.volume = MusicVolume * MasterVolume * IsMenu;
        Area2.volume = MusicVolume * MasterVolume * IsMenu2;
        Area3.volume = MusicVolume * MasterVolume/3 * IsMenu3;

        if(IsMenu > 0){
            IsMenu = IsMenu - 0.01f;
        }
        if(IsMenu2 > 0){
            IsMenu2 = IsMenu2 - 0.01f;
        }
        if(IsMenu3 < 1){
            IsMenu3 = IsMenu3 + 0.01f;
        }
        if(IsMenu4 > 0){
            IsMenu4 = IsMenu4 - 0.01f;
        }
        }



            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }
}