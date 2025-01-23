using UnityEngine;

public class SetRandomColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        float hue = Random.Range(0f, 1f);
        renderer.material.color = Color.HSVToRGB(hue, 1f, 1f);
    }
}