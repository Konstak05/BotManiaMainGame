using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeteorEnemy : MonoBehaviour
{
    //lifeBar
    public Slider HPslider;
    public int lifepoints,Iframes,MAG = 1;
    public bool IsDestructable;

    // Start is called before the first frame update
    void Start()
    {
        //HealthStuff
        lifepoints = 100*MAG;
        HPslider.maxValue = 100*MAG;
        HPslider.value = lifepoints;
        IsDestructable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(HPslider.value != lifepoints){HPslider.value = lifepoints;}
        if(Iframes < 30){Iframes++;}
    }

    //CollisionStuff
    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("PlayerBullet") && IsDestructable == true && Iframes >= 30)
        {
            Iframes = 0;
            lifepoints -= 20;
            GameObject PlayerBullet = other.gameObject;
            Destroy(PlayerBullet);
        }
    }
}
