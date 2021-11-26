using System;

namespace UI.Menus.MainMenu
{
    public interface IHasSettingsMenu : IUIManager
    {
        public event Action SettingsOpened;
    }
}