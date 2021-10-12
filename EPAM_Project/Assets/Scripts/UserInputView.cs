using System;
using Services;
using UnityEngine;

public class UserInputView : MonoBehaviour
{
    private UserInputController controller;
    private void Start()
    {
        controller = ServiceLocator.Instance.Get<UserInputController>();
    }
        
    private void Update()
    {
        controller.ProcessInput();
    }
}