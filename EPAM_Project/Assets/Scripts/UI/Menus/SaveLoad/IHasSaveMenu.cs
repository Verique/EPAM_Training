using System;

namespace UI.Menus.SaveLoad
{
    public interface IHasSaveMenu : IUIManager
    {
        public event Action SaveOpened;
    }
}