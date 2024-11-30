using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public float timer = 1f;
    public Animator transition;
    public VolumeSettings SaveVolume1;
    public VolumeSettings SaveVolume2;
    public void ReloadGame(){
        transition.SetTrigger("Start");
        Invoke("ReloadScene", timer);
        }
    public void LoadMainMenu(){
        transition.SetTrigger("Start"); 
        Invoke("LoadMainMenuTimer", timer);
        }

    private void ReloadScene(){
        if (SaveVolume1 != null){
            SaveVolume1.ToMainMenu(); 
            SaveVolume2.ToMainMenu(); 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    private void LoadMainMenuTimer(){
        if (SaveVolume1 != null){
            SaveVolume1.ToMainMenu(); 
            SaveVolume2.ToMainMenu(); 
        }
        Cursor.lockState = CursorLockMode.None; SceneManager.LoadScene("MainMenu");
    }
}