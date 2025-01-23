using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public int IsTaken;
    public PlayerSpawner PlayerspawnerScene;
    private GameObject Player;
    public GameObject Checkpointdecoration;
    public GameObject DEATHBARRIERLMAO;
    public ParticleSystem CheckpointSave;
    public AudioClip CheckpointSound;
    public AudioSource Sound;

    public GameObject Electric1;
    public GameObject Electric2;
    public GameObject Electric3;
    public GameObject Electric4;

    public GameObject Glass1;
    public GameObject Glass2;

    public Transform Telepos;

    public string Name;


    void Start()
    {


        Checkpointdecoration.SetActive(true);
        IsTaken = 0;
        PlayerspawnerScene = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        Player = GameObject.Find(Name);
        Glass1.SetActive(true);
        Glass2.SetActive(true);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & IsTaken == 0)
        {
            Glass1.SetActive(false);
            Glass2.SetActive(false);

            float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            Sound.volume = audioVolume * masterVolume;
            Sound.PlayOneShot(CheckpointSound);
            CheckpointSave.Play();
            Checkpointdecoration.SetActive(false);
            PlayerspawnerScene.SpawningPos = Telepos.position;
            PlayerspawnerScene.Rotation = Telepos.rotation;
            IsTaken = 1;
            DEATHBARRIERLMAO.SetActive(true);
            Invoke("Electric",0.1f);
        }
    }


    public void Electric(){
    
    int Lightning = UnityEngine.Random.Range(1, 5);
    
    if(Lightning == 1){
    Electric1.SetActive(true);
    Electric2.SetActive(false);
    Electric3.SetActive(false);
    Electric4.SetActive(false);
    }
    if(Lightning == 2){
    Electric1.SetActive(false);
    Electric2.SetActive(true);
    Electric3.SetActive(false);
    Electric4.SetActive(false);
    }
    if(Lightning == 3){
    Electric1.SetActive(false);
    Electric2.SetActive(false);
    Electric3.SetActive(true);
    Electric4.SetActive(false);
    }
    if(Lightning == 4){
    Electric1.SetActive(false);
    Electric2.SetActive(false);
    Electric3.SetActive(false);
    Electric4.SetActive(true);
    }


     Invoke("Electric",0.1f);
    }



}
