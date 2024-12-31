using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public Transform spawnPosition;
    public Button[] buttons;
    public AudioClip Spawnclip;
    public AudioSource SpawnSource;

    private int botCount = 0;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => SpawnBot(index));
        }
    }

    void SpawnBot(int index)
    {
        if (index >= 0 && index < prefabsToSpawn.Length)
        {
            float audioVolume = PlayerPrefs.GetFloat("AudioVolume", 1.0f);
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
            SpawnSource.volume = audioVolume * masterVolume;
            SpawnSource.PlayOneShot(Spawnclip);
            Instantiate(prefabsToSpawn[index], spawnPosition.position, spawnPosition.rotation);
            botCount++;
        }
    }
}