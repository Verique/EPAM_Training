using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void StartNewGame()
    {
        PlayerPrefs.SetString("saveName", "");
        SceneManager.LoadScene("InitialScene");
    }
}
