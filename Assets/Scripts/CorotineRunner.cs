using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotMania
{
    public class CorotineRunner : MonoBehaviour
    {
        private static CorotineRunner _instance;
        public static CorotineRunner instance 
            => _instance != null ? _instance : new GameObject("Corotine Runner").AddComponent<CorotineRunner>();

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void Run(IEnumerator enumerator)
        {
            instance.StartCoroutine(enumerator);
        }    
    }
}
