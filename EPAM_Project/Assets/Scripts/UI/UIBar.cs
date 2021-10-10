using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class UIBar : MonoBehaviour
    {
        private Vector2 initialBarSize;
        private RectTransform barTransform;

        public int MaxValue { get; set; }
        protected Image Image;

        private void Awake()
        {
            SetupBar();
        }

        public void UpdateBarHeight(int newValue)
        {
            var height = initialBarSize.y * newValue / MaxValue;
            SetBarHeight(height);
        }
        
        public void UpdateBarWidth(int newValue)
        {
            var width = initialBarSize.x * newValue / MaxValue;
            SetBarWidth(width);
        }

        private void SetBarWidth(float width)
        {
            barTransform.sizeDelta = new Vector2(width, initialBarSize.y);
        }
        
        private void SetBarHeight(float height)
        {
            barTransform.sizeDelta = new Vector2(initialBarSize.x, height);
        }

        protected virtual void SetupBar()
        {
            Image = GetComponent<Image>();
            barTransform = GetComponent<RectTransform>();
            initialBarSize = barTransform.sizeDelta;
        }
    }
}