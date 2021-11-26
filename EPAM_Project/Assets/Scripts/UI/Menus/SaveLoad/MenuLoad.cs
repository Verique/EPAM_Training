using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus.SaveLoad
{
    public class MenuLoad : LoadSaveMenu<IHasLoadMenu>
    {
        protected override void OnClickButton(string saveName)
        {
            PlayerPrefs.SetString("saveName", saveName);
            SceneManager.LoadScene("InitialScene");
        }

        public override void Init(IHasLoadMenu manager)
        {
            manager.LoadOpened += Show;
        }
    }
}
