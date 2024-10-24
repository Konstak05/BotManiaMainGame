using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryModeEnemylock : MonoBehaviour
{
    public GameObject[] StoryModeThings;
    public GameObject[] StoryModeThings2;
    public PlayerSpawner PlayerspawnerScene;


    void Start()
    {
    PlayerspawnerScene = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
    RefreshEnemyLocks();
    }


    public void RefreshEnemyLocks()
    {
    if(PlayerspawnerScene.IsStoryMode == true){
    for (int StoryModeThingsIndex = 0; StoryModeThingsIndex < StoryModeThings.Length; StoryModeThingsIndex++)
    {
       StoryModeThings[StoryModeThingsIndex].SetActive(true);
       StoryModeThings2[StoryModeThingsIndex].SetActive(false);
    }
    }
    }
}
