using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotMania
{
    public class Preload : MonoBehaviour
    {
        private void Awake()
        {
            SceneLoader.Init();
        }

        // Inint everything first dumbass
        private void Start()
        {
            SceneLoader.LoadMenu(SceneLoader.Menus.Intro);
        }
    }
}
