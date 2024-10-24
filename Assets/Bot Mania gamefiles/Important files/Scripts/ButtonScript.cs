using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public float timer = 1f;
    public Animator transition;
    public void ReloadGame(){
        transition.SetTrigger("Start");
        Invoke("ReloadScene", timer);
        }
    public void LoadMainMenu(){
        transition.SetTrigger("Start"); 
        Invoke("LoadMainMenuTimer", timer);
        }

    private void ReloadScene(){SceneManager.LoadScene(SceneManager.GetActiveScene().name);}
    private void LoadMainMenuTimer(){Cursor.lockState = CursorLockMode.None; SceneManager.LoadScene("MainMenu");}
}