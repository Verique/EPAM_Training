using System.Collections.Generic;
using SaveData;
using Services;
using UnityEngine;

namespace UI.Menus.SaveLoad
{
    public class MenuSave : LoadSaveMenu<IHasSaveMenu>
    {
        private SaveManager saveManager;
        
        [SerializeField] private NewSaveField inputFieldPrefab;
        protected override void AddButtons(List<string> saveFileNames)
        {
            if (saveFileNames.Count <= MaxSaveCount)
            {
                AddElement();
            
                var inputField = Instantiate(inputFieldPrefab, scrollRect.content);
                inputField.NameSubmitted += OnClickButton;
            }
        
            base.AddButtons(saveFileNames);
        }

        protected override void OnClickButton(string saveName)
        {
            saveManager.Save(saveName);
            Hide();
        }

        public override void Init(IHasSaveMenu manager)
        {
            saveManager = ServiceLocator.Instance.Get<SaveManager>();
            manager.SaveOpened += Show;
        }
    }
}
