using UnityEngine;
using UnityEngine.UI;

public class Freemodespawner : MonoBehaviour
{
    private AudioSource Audio;
    public Transform spawnPosition;
    public GameObject[] prefabsToSpawn;
    public Button[] spawnButtons;
    public bool IsBot;
    public float[] customRotations;
    public int InAir = 4;
    public float[] CustomInAir;
    public LayerMask layerMask;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            int index = i;
            spawnButtons[i].onClick.AddListener(() => SpawnPrefab(index));
        }
    }

    void SpawnPrefab(int index)
    {

        RaycastHit hit;
        Camera camera = Camera.main;
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {

        float audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        Audio.volume = audioVolume * masterVolume;
        Audio.Play();
        
            float inAir = InAir;
            if (index < CustomInAir.Length)
            {
                inAir = CustomInAir[index];
            }
            Vector3 spawnPosition = hit.point + hit.normal * inAir;
            Quaternion spawnRotation;

            if (index < customRotations.Length && customRotations[index] != 0f)
            {
                spawnRotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + customRotations[index], 0f);
            }
            else
            {
                spawnRotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + customRotations[index], 0f);
            }

            Instantiate(prefabsToSpawn[index], spawnPosition, spawnRotation);
        }

        if (IsBot == true)
        {
            PlayerPrefs.SetInt("BotBouncerBots", PlayerPrefs.GetInt("BotBouncerBots", 0) + 1);
        }
    }
}