using System;
using Services;
using UI.Menus.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus.MainMenu
{
    public class MenuMain : MonoBehaviour, IHasLoadMenu, IHasSettingsMenu
    {
        [SerializeField] private UIButton playButton;
        [SerializeField] private UIButton loadButton;
        [SerializeField] private UIButton settingsButton;
        [SerializeField] private UIButton exitButton;

        [SerializeField] private UIMenu<IHasLoadMenu> loadMenu;
        [SerializeField] private UIMenu<IHasSettingsMenu> settingsMenu;
        
        public event Action SettingsOpened;
        public event Action LoadOpened;

        private static void ExitGame()
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
            
            loadMenu.Init(this);
            settingsMenu.Init(this);
            loadButton.Init(this);
            settingsButton.Init(this);
            playButton.Init(this);
            exitButton.Init(this);
            
            
            settingsButton.AddListener(() => SettingsOpened?.Invoke());
            loadButton.AddListener(() => LoadOpened?.Invoke());
            playButton.AddListener(StartNewGame);
            exitButton.AddListener(ExitGame);
        }

        private static void StartNewGame()
        {
            PlayerPrefs.SetString("saveName", "");
            SceneManager.LoadScene("InitialScene");
        }
    }
}
