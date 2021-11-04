using System.Collections.Generic;
using SaveData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Menus
{
    public abstract class LoadSaveMenu : MonoBehaviour
    {
        protected const int MaxSaveCount = 10;
        private const int ButtonOffset = 30;
    
        [SerializeField] protected GameObject buttonPrefab;
        [SerializeField] protected ScrollRect scrollRect;

        private int elementCount;
    
        public void OpenMenu()
        {
            GetSaveFileList();

            gameObject.SetActive(true);
        }

        public void AddElement()
        {
            elementCount++;
        }
    
        public void CloseMenu() => gameObject.SetActive(false);

        private void GetSaveFileList()
        {
            var saveFileNames = SaveManager.GetSaveFiles();
            var parentRectTransform = (RectTransform)scrollRect.content.transform;
        
            ClearContent();
            AddButtons(saveFileNames);
            SetContentHeight(parentRectTransform, elementCount * ButtonOffset);
        }

        protected void AddButton(string text, UnityAction action)
        {
            //needs object pooling
            var newGO = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, scrollRect.content);
            var pos = new Vector3(0, - elementCount * ButtonOffset, 0);
            newGO.transform.localPosition = pos;
        
            var button = newGO.GetComponent<Button>();
            var buttonText = newGO.GetComponentInChildren<Text>();

            button.onClick.AddListener(action);
            buttonText.text = text;
            elementCount++;
        }

        protected virtual void AddButtons(List<string> saveFileNames)
        {
            foreach (var saveFileName in saveFileNames)
            {
                AddButton(saveFileName, () => OnClickButton(saveFileName));
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