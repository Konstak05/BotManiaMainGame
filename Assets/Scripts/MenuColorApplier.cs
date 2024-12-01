using UnityEngine;
using UnityEngine.UI;

public class MenuColorApplier : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Image[] images;

    void Awake()
    {
        MenuColorChanger.onColorChange.AddListener(OnColorChange);
    }

    void OnDestroy()
    {
        MenuColorChanger.onColorChange.RemoveListener(OnColorChange);
    }
    void OnColorChange(Color color)
    {
        foreach (Renderer r in renderers) r.material.color = color;
        foreach (Image img in images) img.color = color;
    }
}
