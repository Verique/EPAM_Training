using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus.SaveLoad
{
    [RequireComponent(typeof(Button))]
    public class LoadSaveButton : MonoBehaviour
    {
        private Button button;
        private Text text;

        public event Action ButtonClicked;

        public void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => ButtonClicked?.Invoke());
            text = GetComponentInChildren<Text>();
        }

        public void SetText(string newText)
        {
            text.text = newText;
        }
    }
}