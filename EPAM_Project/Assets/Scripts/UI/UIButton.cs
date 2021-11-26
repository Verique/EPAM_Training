using Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UIButton : UIElement<IUIManager>, IPointerEnterHandler 
    {
        private Button button;
        private SoundManager soundManager;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            soundManager.PlayOneShot("buttonMouseEnter");
        }

        public override void Init(IUIManager manager)
        {
            soundManager = ServiceLocator.Instance.Get<SoundManager>();
            button = GetComponent<Button>();
            button.onClick.AddListener(() => soundManager.PlayOneShot("buttonClick"));
        }

        public void AddListener(UnityAction action)
        {
            button.onClick.AddListener(action);
        }
    }
}