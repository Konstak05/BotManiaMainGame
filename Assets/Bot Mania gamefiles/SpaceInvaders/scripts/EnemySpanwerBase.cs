using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpanwerBase : MonoBehaviour
{
    public TextMeshPro WaveText,Score,HighScore;
    public int WaveCounting,WaveValue,ScoreValue,HighScoreValue;
    //Enemies and Spawn Points
    public GameObject[] Wave1,Wave2,Wave3,Wave4,Wave5,Wave6,Wave7,Wave8,Wave9,Wave10,Wave11;
    public GameObject[] EnemyChosenList;
    public Transform EnemySpawnerPosition;
    public Transform EnemyPosition;
    public Transform ParentPosition;

    void Start()
    {
        WaveCounting = 10;
        WaveText.text = "Wave starting soon!";
        Score.text = "Score: " + ScoreValue;
        HighScoreValue = PlayerPrefs.GetInt("SpaceInvHighScore");
        HighScore.text = "HighScore: " + HighScoreValue;
        Invoke("WaveStarter", 3f);
    }
    void WaveStarter()
    {
        if(WaveCounting > -1){
            WaveText.text = "Next Wave starts in: " + WaveCounting;
            WaveCounting -= 1;
            Invoke("WaveStarter", 1f);
        }
        else{
            WaveValue += 1;
            WaveCounting = 30;
            Invoke("WaveTimer", 0.1f);
            Invoke("EnemySpawnWave", 0.1f);
        }
    }
    void WaveTimer()
    {
        if(WaveCounting > -1){
            WaveText.text = "Wave " + WaveValue + " Lasts until " + WaveCounting;
            WaveCounting -= 1;
            Invoke("WaveTimer", 1f);
        }
        else{
            WaveCounting = 10;
            CancelInvoke("EnemySpawnWave");
            Invoke("WaveStarter", 1f);
        }
    }
    void EnemySpawnWave()
    {
        switch (WaveValue){
            case 1: EnemyChosenList = Wave1; break;
            case 2: EnemyChosenList = Wave2; break;
            case 3: EnemyChosenList = Wave3; break;
            case 4: EnemyChosenList = Wave4; break;
            case 5: EnemyChosenList = Wave5; break;
            case 6: EnemyChosenList = Wave6; break;
            case 7: EnemyChosenList = Wave7; break;
            case 8: EnemyChosenList = Wave8; break;
            case 9: EnemyChosenList = Wave9; break;
            case 10: EnemyChosenList = Wave10; break;
            default: EnemyChosenList = Wave11; break; // For WaveValue >= 11 or any invalid value
        }

        int EnemyRandomizervalue = UnityEngine.Random.Range(0, EnemyChosenList.Length);
        var Enemy = Instantiate(EnemyChosenList[EnemyRandomizervalue], EnemySpawnerPosition.position, EnemySpawnerPosition.rotation);
    
        Enemy.transform.SetParent(ParentPosition);

        float randomWaveValue = Random.Range(0.4f, 1.5f);
        Invoke("EnemySpawnWave", randomWaveValue);
    }

    //WaveReseter
    public void WaveReset(){
        WaveCounting = 10;
        WaveValue = 0;
        WaveText.text = "Resetting Waves!";
        CancelInvoke("WaveStarter");
        CancelInvoke("WaveTimer");
        CancelInvoke("EnemySpawnWave");
        Invoke("WaveStarter", 3f);
    }
    //ScoreFunctions
    public void AddScore(int AddValue){
        ScoreValue += AddValue;
        Score.text = "Score: " + ScoreValue;
    }
    public void UpdateScore(){
        if(ScoreValue > HighScoreValue){
            HighScoreValue = ScoreValue;
            HighScore.text = "HighScore: " + HighScoreValue;
        }
        ScoreValue = 0;
        Score.text = "Score: " + ScoreValue;
    }

    public void OnDisable(){PlayerPrefs.SetInt("SpaceInvHighScore", HighScoreValue);}
    public void OnDestroy(){PlayerPrefs.SetInt("SpaceInvHighScore", HighScoreValue);}
}
