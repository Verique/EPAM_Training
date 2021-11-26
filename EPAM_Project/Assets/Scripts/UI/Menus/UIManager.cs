using System;
using Services;
using UnityEngine;

namespace UI.Menus
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private UIMenu<UIManager> pauseMenu;
        public event Action GamePaused;
        public event Action GameResumed;

        [SerializeField] private UIMenu<UIManager> endMenu;
        public event Action<GameStats> GameEnded;

        private void Awake()
        {
            var gameManager = ServiceLocator.Instance.Get<GameManager>();
            
            pauseMenu.Init(this);
            endMenu.Init(this);
            
            gameManager.GamePaused += () => GamePaused?.Invoke();
            gameManager.GameResumed += () => GameResumed?.Invoke();
            gameManager.GameEnded += stats => GameEnded?.Invoke(stats);
        }
    }
}