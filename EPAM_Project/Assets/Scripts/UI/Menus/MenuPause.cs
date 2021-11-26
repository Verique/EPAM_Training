using System;
using Services;
using UI.Menus.SaveLoad;
using UnityEngine;

namespace UI.Menus
{
    public class MenuPause : UIMenu<UIManager>, IHasLoadMenu, IHasSaveMenu
    {
        [SerializeField] private UIButton resumeButton;
        [SerializeField] private UIButton mainMenuButton;
        [SerializeField] private UIButton saveButton;
        [SerializeField] private UIButton loadButton;
        
        [SerializeField] private MenuSave saveMenu;
        [SerializeField] private MenuLoad loadMenu;
        
        public event Action LoadOpened;
        public event Action SaveOpened;
        
        protected override void Show()
        {
            base.Show();
            Time.timeScale = 0;
        }

        protected override void Hide()
        {
            base.Hide();
            Time.timeScale = 1;
        }

        private void OnDisable()
        {
            Hide();
        }

        public override void Init(UIManager manager)
        {
            manager.GamePaused += Show;
            manager.GameResumed += Hide;

            var gameManager = ServiceLocator.Instance.Get<GameManager>();
            
            saveMenu.Init(this);
            loadMenu.Init(this);
            resumeButton.Init(this);
            mainMenuButton.Init(this);
            saveButton.Init(this);
            loadButton.Init(this);
            
            resumeButton.AddListener(gameManager.PauseResumeToggle);
            mainMenuButton.AddListener(gameManager.ToMainMenu);
            saveButton.AddListener(() => SaveOpened?.Invoke());
            loadButton.AddListener(() => LoadOpened?.Invoke());
            
        }
    }
}
