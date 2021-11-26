using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus.SaveLoad
{
    [RequireComponent(typeof(InputField))]
    public class NewSaveField : MonoBehaviour
    {
        private InputField inputField;
        private Button submitButton;

        public event Action<string> NameSubmitted;

        private void Awake()
        {
            inputField = GetComponent<InputField>();
            submitButton = GetComponentInChildren<Button>();
            submitButton.onClick.AddListener(() => NameSubmitted?.Invoke(inputField.text));
        }
    }
}