using System.Collections.Generic;
using SaveData;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus.SaveLoad
{
    public abstract class LoadSaveMenu<T> : UIMenu<T> where T : IUIManager 
    {
        protected const int MaxSaveCount = 10;
        private const int ButtonOffset = 30;
    
        [SerializeField] protected LoadSaveButton buttonPrefab;
        [SerializeField] protected ScrollRect scrollRect;
        [SerializeField] protected Button exitButton;

        private int elementCount;

        protected override void Show()
        {
            base.Show();
            GetSaveFileList();
        }

        private void Awake()
        {
            exitButton.onClick.AddListener(Hide);
        }

        protected void AddElement()
        {
            elementCount++;
        }
    
        private void GetSaveFileList()
        {
            var saveFileNames = SaveManager.GetSaveFiles();
            var parentRectTransform = (RectTransform)scrollRect.content.transform;
        
            ClearContent();
            AddButtons(saveFileNames);
            SetContentHeight(parentRectTransform, elementCount * ButtonOffset);
        }

        private LoadSaveButton AddButton()
        {
            //needs object pooling
            var newGO = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, scrollRect.content);
            var pos = new Vector3(0, - elementCount * ButtonOffset, 0);
            newGO.transform.localPosition = pos;
            elementCount++;
            
            return newGO;
        }

        protected virtual void AddButtons(List<string> saveFileNames)
        {
            foreach (var saveFileName in saveFileNames)
            {
                var button = AddButton();
                button.ButtonClicked += () => OnClickButton(saveFileName);
                button.SetText(saveFileName);
            }
        }
    
        protected abstract void OnClickButton(string saveName);

        private void ClearContent()
        {
            for (var i = 0; i < scrollRect.content.childCount; i++)
            {
                Destroy(scrollRect.content.GetChild(i).gameObject);
            }

            elementCount = 0;
        }

        private void SetContentHeight(RectTransform content, float size)
        {
            content.sizeDelta = new Vector2(content.sizeDelta.x, size);
        }
    }
}