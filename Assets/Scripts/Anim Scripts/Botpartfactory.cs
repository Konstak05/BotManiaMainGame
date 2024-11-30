using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botpartfactory : MonoBehaviour
{
    public GameObject[] Objects;
    public MeshRenderer[] ObjectMesh;
    public Color ColorObject;
    public int SpawnedObject;
    public float ColorObjectLogger;
    public Animator AnimationRefresher;
    public GameObject MainObject;

    void Start()
    {
        ColorObject.a = 0f;
        ColorObject.r = 1f;
        ColorObject.g = 1f;
        ColorObject.b = 1f;
        for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.color = ColorObject;}
        Invoke("UpdateFactory1",1.1f);
    }

    void UpdateFactory1()
    {
        if(SpawnedObject == 0){
            int randomIndex = UnityEngine.Random.Range(0, Objects.Length);
            for (int i2 = 0; i2 < Objects.Length; i2++){Objects[i2].SetActive(i2 == randomIndex);}
            SpawnedObject = 1;
        }
        Invoke("UpdateFactoryAlpha",0.05f);
        Invoke("UpdateFactory2",2f);
    }
    void UpdateFactoryAlpha()
    {
        ColorObject.a += 0.026f;
        ColorObjectLogger = ColorObject.a;
        for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.color = ColorObject;}
        Invoke("UpdateFactoryAlpha",0.05f);
    }
    void UpdateFactory2()
    {
        //Change Material to Opaque
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.SetFloat("_Mode", 0);};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.SetInt("_ZWrite", 1);};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.DisableKeyword("_ALPHATEST_ON");};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.DisableKeyword("_ALPHABLEND_ON");};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.DisableKeyword("_ALPHAPREMULTIPLY_ON");};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;};
        //CancelFadeTimer
        CancelInvoke("UpdateFactoryAlpha");
        Invoke("UpdateFactory3",0.9f);
    }
    void UpdateFactory3()
    {
        Invoke("UpdateFactoryColor",0.05f);
        Invoke("UpdateFactory4",3.6f);
    }
    void UpdateFactoryColor()
    {
        if(ColorObject.r > 0 && ColorObject.g > 0 && ColorObject.b > 0){
            ColorObject.r -= 0.025f;
            ColorObject.g -= 0.025f;
            ColorObject.b -= 0.025f;
            for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.color = ColorObject;}
        }
        Invoke("UpdateFactoryColor",0.05f);
    }
    void UpdateFactory4()
    {
        //Change Material to Fade
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.SetFloat("_Mode", 2);}; //Fade
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.SetInt("_ZWrite", 0);};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.DisableKeyword("_ALPHATEST_ON");};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.EnableKeyword("_ALPHABLEND_ON");};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.DisableKeyword("_ALPHAPREMULTIPLY_ON");};
        //for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;};

        CancelInvoke("UpdateFactoryColor");
        ColorObject.a = 0f;
        ColorObject.r = 1f;
        ColorObject.g = 1f;
        ColorObject.b = 1f;
        SpawnedObject = 0;
        for (int i = 0; i < ObjectMesh.Length; i++) {ObjectMesh[i].material.color = ColorObject;}
        for (int i = 0; i < Objects.Length; i++) {Objects[i].SetActive(false);}
        if(MainObject.activeInHierarchy){Invoke("Animationfunctionrefresher",1f);}
        Invoke("UpdateFactory1",2f);
    }
    void Animationfunctionrefresher(){
        if(MainObject.activeInHierarchy){AnimationRefresher.Play("Factory 1");}
    }
}
