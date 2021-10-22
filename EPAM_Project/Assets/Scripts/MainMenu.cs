using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEditor.SearchService;
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
        SceneManager.LoadScene("InitialScene");
    }
}
