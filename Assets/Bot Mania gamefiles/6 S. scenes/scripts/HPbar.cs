using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HPbar : MonoBehaviour
{
    public GameObject Player;
    public KeyboardControlMk2 PlayerScript;
    public float beatDuration = 0.5f;
    public float maxScale = 1.2f;

    private Vector3 originalScale;
    public GameObject Heart;

    public Slider BounceMul;
    public TextMeshProUGUI HP2;
    void Start()
    {
        BounceMul.value = 100;
        originalScale = Heart.transform.localScale;
        StartCoroutine(Beat());
    }

    public void RespawnHPHeart()
    {
        BounceMul.value = 100;
        StartCoroutine(Beat());
    }

    void Update()
    {

        Player = GameObject.Find("Player");
        PlayerScript = Player.GetComponent<KeyboardControlMk2>();

        BounceMul.value = PlayerScript.HP;
        float HP = PlayerScript.HP;
        HP2.text = HP.ToString();

    }

    private IEnumerator Beat()
    {
        while (true)
        {
            yield return ScaleUp();
            yield return ScaleDown();
        }
    }



    private IEnumerator ScaleUp()
    {
        float elapsedTime = 0f;
        Vector3 targetScale = originalScale * maxScale;

        while (elapsedTime < beatDuration)
        {
            Heart.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / beatDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Heart.transform.localScale = targetScale;
    }

    private IEnumerator ScaleDown()
    {
        float elapsedTime = 0f;
        Vector3 targetScale = originalScale;

        while (elapsedTime < beatDuration)
        {
            Heart.transform.localScale = Vector3.Lerp(Heart.transform.localScale, targetScale, elapsedTime / beatDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Heart.transform.localScale = targetScale;
        if(PlayerScript.HP > 10){
        beatDuration = 0.01f * PlayerScript.HP;
        }
        else{
        beatDuration = 0.1f;
        }
    }


}
