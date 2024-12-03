using BotMania.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BotMania
{
    public class tutorialFinishTeleporter : MonoBehaviour
    {
        private bool HasTeleported = false;
        public string SceneName;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !HasTeleported)
            {

                HasTeleported = true;
                Invoke("StartGame", 0f);
            }
        }

        void StartGame()
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            Invoke("StartGame2", 4);
        }

        void StartGame2()
        {
            Cursor.lockState = CursorLockMode.None;
            SceneLoader.LoadMenu(SceneLoader.Menus.PressStart, 1);
        }
    }
}
