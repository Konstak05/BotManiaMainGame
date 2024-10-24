using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLocker : MonoBehaviour
{
    private int Cost = 5;
    private int Cost2 = 10;
    private int Cost3 = 15;
    private int Cost4 = 20;
    private int Cost5 = 30;
    private int Cost6 = 40;
    public GameObject MapShower;
    public GameObject MapShower2;
    public GameObject MapShower3;
    public GameObject MapShower4;
    public GameObject MapShower5;
    public GameObject MapShower6;
    public Animator transition;


    public void Start()
    {
        int BotBouncerCurrency = PlayerPrefs.GetInt("BotBouncerPoints");        
    }

    public void Update()
    {
        int BotBouncerCurrency = PlayerPrefs.GetInt("BotBouncerPoints");     

        if(PlayerPrefs.GetInt("Map2") == 1){MapShower.SetActive(false);;}
        else{MapShower.SetActive(true);;}
        if(PlayerPrefs.GetInt("Map3") == 1){MapShower2.SetActive(false);;}
        else{MapShower2.SetActive(true);;}
        if(PlayerPrefs.GetInt("Map4") == 1){MapShower3.SetActive(false);;}
        else{MapShower3.SetActive(true);;}
        if(PlayerPrefs.GetInt("Map5") == 1){MapShower4.SetActive(false);;}
        else{MapShower4.SetActive(true);;}
        if(PlayerPrefs.GetInt("Map6") == 1){MapShower5.SetActive(false);;}
        else{MapShower5.SetActive(true);;}
        if(PlayerPrefs.GetInt("Map7") == 1){MapShower6.SetActive(false);;}
        else{MapShower6.SetActive(true);;}
    }

    public void GoToScene2(){
        int BotBouncerCurrency = PlayerPrefs.GetInt("BotBouncerPoints");  
        if (BotBouncerCurrency >= Cost && PlayerPrefs.GetInt("Map2") == 0){        
        BotBouncerCurrency -= Cost;
        PlayerPrefs.SetInt("BotBouncerPoints", BotBouncerCurrency);    
        PlayerPrefs.SetInt("Map2", 1);
        }
    else if(!PlayerPrefs.HasKey("Map2")){
        PlayerPrefs.SetInt("Map2", 0);
        }
    else if (PlayerPrefs.GetInt("Map2") == 1){        
        transition.SetTrigger("Start"); 
        Invoke("LoadBotBouncerMap1timer", 1);
        }}




    public void GoToScene3(){
        int BotBouncerCurrency = PlayerPrefs.GetInt("BotBouncerPoints");  
        if (BotBouncerCurrency >= Cost2 && PlayerPrefs.GetInt("Map3") == 0){        
        BotBouncerCurrency -= Cost2;
        PlayerPrefs.SetInt("BotBouncerPoints", BotBouncerCurrency);    
        PlayerPrefs.SetInt("Map3", 1);
        }
    else if(!PlayerPrefs.HasKey("Map3")){
        PlayerPrefs.SetInt("Map3", 0);
        }
    else if (PlayerPrefs.GetInt("Map3") == 1){        
        transition.SetTrigger("Start"); 
        Invoke("LoadBotBouncerMap2timer", 1);
        }}




    public void GoToScene4(){
        int BotBouncerCurrency = PlayerPrefs.GetInt("BotBouncerPoints");  
        if (BotBouncerCurrency >= Cost3 && PlayerPrefs.GetInt("Map4") == 0){        
        BotBouncerCurrency -= Cost3;
        PlayerPrefs.SetInt("BotBouncerPoints", BotBouncerCurrency);    
        PlayerPrefs.SetInt("Map4", 1);
        }
    else if(!PlayerPrefs.HasKey("Map4")){
        PlayerPrefs.SetInt("Map4", 0);
        }
    else if (PlayerPrefs.GetInt("Map4") == 1){        
        transition.SetTrigger("Start"); 
        Invoke("LoadBotBouncerMap3timer", 1);
        }}



    public void GoToScene5(){
        int BotBouncerCurrency = PlayerPrefs.GetInt("BotBouncerPoints");  
        if (BotBouncerCurrency >= Cost5 && PlayerPrefs.GetInt("Map6") == 0){        
        BotBouncerCurrency -= Cost5;
        PlayerPrefs.SetInt("BotBouncerPoints", BotBouncerCurrency);    
        PlayerPrefs.SetInt("Map6", 1);
        }
    else if(!PlayerPrefs.HasKey("Map6")){
        PlayerPrefs.SetInt("Map6", 0);
        }
    else if (PlayerPrefs.GetInt("Map6") == 1){        
        transition.SetTrigger("Start"); 
        Invoke("LoadBotBouncerMap5timer", 1);
        }}





    public void GoToScene6(){
        int BotBouncerCurrency = PlayerPrefs.GetInt("BotBouncerPoints");  
        if (BotBouncerCurrency >= Cost4 && PlayerPrefs.GetInt("Map5") == 0){        
        BotBouncerCurrency -= Cost4;
        PlayerPrefs.SetInt("BotBouncerPoints", BotBouncerCurrency);    
        PlayerPrefs.SetInt("Map5", 1);
        }
    else if(!PlayerPrefs.HasKey("Map5")){
        PlayerPrefs.SetInt("Map5", 0);
        }
    else if (PlayerPrefs.GetInt("Map5") == 1){        
        transition.SetTrigger("Start"); 
        Invoke("LoadBotBouncerMap4timer", 1);
        }}






    public void GoToScene7(){
        int BotBouncerCurrency = PlayerPrefs.GetInt("BotBouncerPoints");  
        if (BotBouncerCurrency >= Cost6 && PlayerPrefs.GetInt("Map7") == 0){        
        BotBouncerCurrency -= Cost6;
        PlayerPrefs.SetInt("BotBouncerPoints", BotBouncerCurrency);    
        PlayerPrefs.SetInt("Map7", 1);
        }
    else if(!PlayerPrefs.HasKey("Map7")){
        PlayerPrefs.SetInt("Map7", 0);
        }
    else if (PlayerPrefs.GetInt("Map7") == 1){        
        transition.SetTrigger("Start"); 
        Invoke("LoadBotBouncerMap6timer", 1);
        }}


    private void LoadBotBouncerMap1timer(){SceneManager.LoadScene("Bot bouncer Asylum map");}
    private void LoadBotBouncerMap2timer(){SceneManager.LoadScene("Bot bouncer Sand map");}
    private void LoadBotBouncerMap3timer(){SceneManager.LoadScene("Bot bouncer SnowMap");}
    private void LoadBotBouncerMap4timer(){SceneManager.LoadScene("Bot bouncer River Map");}
    private void LoadBotBouncerMap5timer(){SceneManager.LoadScene("Bot bouncer Flatgrass map");}
    private void LoadBotBouncerMap6timer(){SceneManager.LoadScene("Bot bouncer IsolationMap");}
}
