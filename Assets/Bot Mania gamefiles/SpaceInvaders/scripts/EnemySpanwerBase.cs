using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwerBase : MonoBehaviour
{
    public GameObject[] Prefabtest;
    public Transform EnemySpawnerPosition;
    public Transform ParentPosition;

    void Start()
    {
        //int EnemyRandomizer = UnityEngine.Random.Range(1, 3);
        Invoke("EnemySpawnStarter", 5f);
    }

    void EnemySpawnStarter()
    {
        var meteor = Instantiate(Prefabtest[0], EnemySpawnerPosition.position, EnemySpawnerPosition.rotation);
        meteor.transform.SetParent(ParentPosition);
        Invoke("EnemySpawnStarter", 0.9123f);
    }
}
