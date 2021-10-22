using System.Collections;
using System.Collections.Generic;
using SaveData;
using UnityEngine;
using UnityEngine.UI;

public class TestSaveButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SaveManager.Save);
    }
}
