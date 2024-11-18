using UnityEngine;
using UnityEngine.UI;

public class ChangeMaterialColor : MonoBehaviour
{
    public Renderer[] BotColor;
    public Slider slider1;

    public Renderer[] Renderers;
    public Slider slider2;

    public Color[] customColors;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("CustomNoteBookColor1") == 0f || PlayerPrefs.GetFloat("CustomNoteBookColor2") == 0f)
        {
            PlayerPrefs.SetFloat("CustomNoteBookColor1", 1f);
            PlayerPrefs.SetFloat("CustomNoteBookColor2", 0.05f);

            foreach (Renderer r2 in BotColor)
            {
                r2.material.color = Color.black;
            }
            foreach (Renderer r in Renderers)
            {
                r.material.color = Color.blue;
            }

            slider1.value = 1f;
            slider2.value = 0.05f;
        }
        else
        {
            float value1 = PlayerPrefs.GetFloat("CustomNoteBookColor1");
            float value2 = PlayerPrefs.GetFloat("CustomNoteBookColor2");

            slider1.value = PlayerPrefs.GetFloat("CustomNoteBookColor1");
            slider2.value = PlayerPrefs.GetFloat("CustomNoteBookColor2");


            Color newColor1 = GetCustomColor(value1);
            foreach (Renderer r2 in BotColor)
            {
                r2.material.color = new Color(newColor1.r, newColor1.g, newColor1.b, 1f);
            }

            Color newColor2 = GetCustomColor(value2);
            foreach (Renderer r in Renderers)
            {
                r.material.color = new Color(newColor2.r, newColor2.g, newColor2.b, 1f);
            }
        }
    }

    public void OnSlidersValueChanged()
    {
        float value1 = slider1.value;
        Color newColor = GetCustomColor(value1);
        PlayerPrefs.SetFloat("CustomNoteBookColor1", value1);
        foreach (Renderer r2 in BotColor)
        {
            r2.material.color = new Color(newColor.r, newColor.g, newColor.b, 1f);;
        }
    }

    public void OnSlidersValueChanged2()
    {
        float value2 = slider2.value;
        Color newColor = GetCustomColor(value2);
        PlayerPrefs.SetFloat("CustomNoteBookColor2", value2);
        foreach (Renderer r in Renderers)
        {
            r.material.color = new Color(newColor.r, newColor.g, newColor.b, 1f);;
        }
    }

    private Color GetCustomColor(float value)
    {
        if (customColors != null && customColors.Length > 0)
        {
            int index = Mathf.FloorToInt(value * (customColors.Length - 1));
            float subValue = (value * (customColors.Length - 1)) - index;
            Color color1 = customColors[index];
            Color color2 = customColors[Mathf.Min(index + 1, customColors.Length - 1)];
            return Color.Lerp(color1, color2, subValue);
        }
        else
        {
            return Color.HSVToRGB(value, 1, 1);
        }
    }
}