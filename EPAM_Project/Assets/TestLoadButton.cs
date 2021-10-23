using System;
using System.Collections;
using System.Collections.Generic;
using SaveData;
using UnityEngine;
using UnityEngine.UI;

public class TestLoadButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SaveManager.Load);
    }
}
