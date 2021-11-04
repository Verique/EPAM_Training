using System.Collections.Generic;
using SaveData;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus
{
    public class SaveMenu : LoadSaveMenu
    {
        [SerializeField] private GameObject inputFieldPrefab;
        protected override void AddButtons(List<string> saveFileNames)
        {
            if (saveFileNames.Count <= MaxSaveCount)
            {
                AddElement();

                var inputField = Instantiate(inputFieldPrefab, scrollRect.content);
                inputField.GetComponent<InputField>().onEndEdit.AddListener(OnClickButton);
            
            }
        
            base.AddButtons(saveFileNames);
        }

        protected override void OnClickButton(string saveName)
        {
            ServiceLocator.Instance.Get<SaveManager>().Save(saveName);
            CloseMenu();
        }
    }
}
