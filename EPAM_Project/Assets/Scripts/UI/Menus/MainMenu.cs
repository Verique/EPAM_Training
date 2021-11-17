using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif 
        }

        private void Awake()
        {
            ServiceLocator.Instance.Get<SoundManager>().PlayMusic("mainMenuMusic");
        }

        public void StartNewGame()
        {
            PlayerPrefs.SetString("saveName", "");
            SceneManager.LoadScene("InitialScene");
        }
    }
}
