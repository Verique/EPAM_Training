using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour, IPointerEnterHandler
    {
        private Button button;
        private SoundManager soundManager;
        
        public void Awake()
        {
            soundManager = ServiceLocator.Instance.Get<SoundManager>();
            button = GetComponent<Button>();
            button.onClick.AddListener(() => soundManager.PlayOneShot("buttonClick"));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            soundManager.PlayOneShot("buttonMouseEnter");
        }
    }
}