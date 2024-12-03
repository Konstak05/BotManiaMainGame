using BotMania.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace BotMania
{
    public static class SceneLoader
    {
        // Note: Preload is unaccesable due being loading scene, why would you want to load it...
        public enum Menus { Intro, PressStart, MainMenu, BotBouncerMenu, SettingsMenu }
        public static Dictionary<Menus, string> menuSceneNames;

        public static UnityEvent onSceneLoaded = new();

        public static void Init()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            menuSceneNames = new()
            {
                {Menus.Intro, "Intro"},
                {Menus.PressStart, "PressStartMenu"},
                {Menus.MainMenu, "MainMenu"},
                {Menus.BotBouncerMenu, "BotBouncerMenu"},
                {Menus.SettingsMenu, "SettingsMenu"},
            };
        }

        /// <summary>
        /// color: 0 = black (defalt) 1 = white
        /// </summary>
        public static void LoadMenu(Menus menu, int color = 0, bool showLoading = true)
        {
            CorotineRunner.Run(LoadSceneCorotine(menuSceneNames[menu], color, showLoading));
        }

        public static void LoadLevel(string ID, string num, int color = 0)
        {

        }

        static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            onSceneLoaded.Invoke();
        }

        static void SetColorFromInt(int color)
        {
            switch (color)
            {
                case 0: LoadingScreen.SetColor(Color.black); break;
                case 1: LoadingScreen.SetColor(Color.white); break;
                default: LoadingScreen.SetColor(Color.black); break;
            }
        }

        public static void LoadScene(string scene, int color = 0, bool showLoading = true)
        {
            CorotineRunner.Run(LoadSceneCorotine(scene, color, showLoading));
        }

        private static IEnumerator LoadSceneCorotine(string scene, int screenColor, bool showLoading = true)
        {
            SetColorFromInt(screenColor);
            if (showLoading)
            {
                LoadingScreen.screenEnabled = true;
                yield return new WaitForSecondsRealtime(0.5f);
            }
            yield return SceneManager.LoadSceneAsync(scene);
            if (showLoading) LoadingScreen.screenEnabled = false;
        }
    }
}
