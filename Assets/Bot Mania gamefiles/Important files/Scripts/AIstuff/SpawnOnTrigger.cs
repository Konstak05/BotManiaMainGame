using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public float spawnInterval = 0.2f;
    public Transform[] spawnPositions;
    public bool hasSpawned,spawnindefinitely;
    public AudioSource Sound;
    public float[] Mag;
    public int[] EnemyType;
    public Transform prefab2;
    public GameObject ActiveEnemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            StartCoroutine(SpawnPrefabs());
            if(!spawnindefinitely){hasSpawned = true;}
        }
    }

    IEnumerator SpawnPrefabs()
    {
        int spawnIndex = 0;
        foreach (GameObject prefab in prefabsToSpawn)
        {
            if (spawnIndex >= Mag.Length){spawnIndex = 0;}
            if(Sound != null){
                float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
                float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
                Sound.volume = audioVolume * masterVolume;
                Sound.Play();
            }

            ActiveEnemy = Instantiate(prefab, spawnPositions[spawnIndex].position, gameObject.transform.rotation);
            if(EnemyType[spawnIndex] == 0){
                prefab2 = ActiveEnemy.transform.Find("bot");
                prefab2.GetComponent<EnemybotAI>().Mag = Mag[spawnIndex];
            }
            else if(EnemyType[spawnIndex] == 1){
                prefab2 = ActiveEnemy.transform.Find("Sentry");
                prefab2.GetComponent<EnemySentrybot>().Mag = Mag[spawnIndex];
            }
            else if(EnemyType[spawnIndex] == 2){
                prefab2 = ActiveEnemy.transform.Find("Sentry");
                prefab2.GetComponent<EnemySentryBoss>().Mag = Mag[spawnIndex];
            }
            yield return new WaitForSeconds(spawnInterval);
            spawnIndex++;
        }
    }
}