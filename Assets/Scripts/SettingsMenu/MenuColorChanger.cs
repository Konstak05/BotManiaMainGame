using UnityEngine;
using UnityEngine.UI;

public class MenuColorChanger : MonoBehaviour
{
    public Renderer[] ColorRenderers;
    public Image[] ColorImage;
    public Slider slider1;
    public string PlayerPref;

    public Color[] customColors;

    private void Start()
    {
        if (PlayerPrefs.GetFloat(PlayerPref) == 0f)
        {
            PlayerPrefs.SetFloat(PlayerPref, 0.06f);

            foreach (Renderer r2 in ColorRenderers)
            {
                r2.material.color = Color.blue;
            }
            foreach (Image i in ColorImage)
            {
                i.color = new Color32(0, 0, 255, 255);
            }


            if(slider1 != null){
            slider1.value = 1f;
            }
        }
        else
        {
            float value1 = PlayerPrefs.GetFloat(PlayerPref);

            if(slider1 != null){
            slider1.value = PlayerPrefs.GetFloat(PlayerPref);
            }


            Color newColor1 = GetCustomColor(value1);
            foreach (Renderer r2 in ColorRenderers)
            {
                r2.material.color = new Color(newColor1.r, newColor1.g, newColor1.b, r2.material.color.a);
            }

            foreach (Image i in ColorImage)
            {
                i.color = new Color32((byte)(newColor1.r * 255),(byte)(newColor1.g * 255),(byte)(newColor1.b * 255),255);
            }
        }
    }

    public void OnSlidersValueChanged()
    {
        float value1 = slider1.value;
        Color newColor = GetCustomColor(value1);
        PlayerPrefs.SetFloat(PlayerPref, value1);
        foreach (Renderer r2 in ColorRenderers)
        {
            //r2.material.color = newColor;
            r2.material.color = new Color(newColor.r, newColor.g, newColor.b, r2.material.color.a);
        }

        foreach (Image i in ColorImage)
        {
            i.color = new Color(newColor.r, newColor.g, newColor.b, i.color.a);
            //i.color = new Color32((byte)(newColor.r * 255),(byte)(newColor.g * 255),(byte)(newColor.b * 255),255);
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