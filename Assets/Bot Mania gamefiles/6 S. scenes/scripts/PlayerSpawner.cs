using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{

    public string Location;
    public string Goal;
    public bool IsCreativeMode;
    public bool IsTutorialMode;
    public bool IsFromTeleporter;
    public bool IsIndoors;
    public bool IsStoryMode;

    public AudioClip TeleportSound1;
    public AudioClip TeleportSound2;
    public AudioClip TeleportSound3;
    public AudioSource Sound;
    public Vector3 SpawningPos;
    public Quaternion Rotation;
    public GameObject ParticleTeleporter;
    public GameObject Minimap;
    public Gunscript UnlockObject2;

    public AudioSource Ambient;
    public AudioSource Combat;
    private float IsAmbient,Isfighting;

    void Awake()
    {
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        IsAmbient = 0f;
        Isfighting = 0f;

        GlobalData.Initialize();

    }

    void Start(){
    
    Minimap = GameObject.Find("Minimap101");
    if(IsIndoors){
        Minimap.SetActive(false);
    }
    else{
        Minimap.SetActive(true);
    }
    
    if(IsFromTeleporter){
    ParticleTeleporter = GameObject.Find("Player boom particle");
    int RandomTeleport = UnityEngine.Random.Range(0, 3);

    if(RandomTeleport == 0){
    float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
    float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
    Sound.volume = audioVolume * masterVolume;
    Sound.PlayOneShot(TeleportSound1);
    }
    else if (RandomTeleport == 1){
    float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
    float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
    Sound.volume = audioVolume * masterVolume;
    Sound.PlayOneShot(TeleportSound2);

    }
    else if (RandomTeleport == 2){
    float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
    float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
    Sound.volume = audioVolume * masterVolume;
    Sound.PlayOneShot(TeleportSound3);

    }
    
    ParticleTeleporter.SetActive(true);

    }

    if(IsTutorialMode){
    UnlockObject2 = GameObject.Find("CameraArea223").GetComponent<Gunscript>();
    UnlockObject2.IsTutorialMode2 = true;
    }

    }

    void Update(){

       if(GlobalData.GetEnemyCount() < 1 && Ambient != null && Combat != null){

        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        Ambient.volume = MusicVolume * MasterVolume * IsAmbient;
        Combat.volume = MusicVolume * MasterVolume * Isfighting;

        if(IsAmbient < 1){
            IsAmbient = IsAmbient + 0.01f;
        }
        if(Isfighting > 0){
            Isfighting = Isfighting - 0.01f;
        }

       }
       else if (GlobalData.GetEnemyCount() > 0 && Ambient != null && Combat != null){

        float MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        Ambient.volume = MusicVolume * MasterVolume * IsAmbient;
        Combat.volume = MusicVolume * MasterVolume * Isfighting;

        if(IsAmbient > 0){
            IsAmbient = IsAmbient - 0.01f;
        }
        if(Isfighting < 1){
            Isfighting = Isfighting + 0.01f;
        }

       }

    }
}
