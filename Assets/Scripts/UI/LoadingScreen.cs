using UnityEngine;
using UnityEngine.UI;

namespace BotMania.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        private static LoadingScreen instance;

        [SerializeField] private Animator animator;
        [SerializeField] private Image image;

        public static bool screenEnabled
        {
            get => instance != null && instance.animator.GetBool("ScreenEnabled");
            set
            {
                if (instance != null) instance.animator.SetBool("ScreenEnabled", value);
            }
        }

        void Awake()
        {
            instance = this;
            screenEnabled = true;
            DontDestroyOnLoad(this);
        }

        public static void SetColor(Color color)
        {
            instance.image.color = color;
        }
    }
}
