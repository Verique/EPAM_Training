using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus
{
    public class LoadMenu : LoadSaveMenu
    {
        protected override void OnClickButton(string saveName)
        {
            PlayerPrefs.SetString("saveName", saveName);
            SceneManager.LoadScene("InitialScene");
        }
    }
}
