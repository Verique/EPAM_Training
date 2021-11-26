using System;

namespace UI.Menus.SaveLoad
{
    public interface IHasLoadMenu : IUIManager
    {
        public event Action LoadOpened;
    }
}