using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIBar : UIElement
    {
        private Vector2 initialBarSize;
        private RectTransform barTransform;
        protected int MaxValue { get; set; }

        protected virtual void Awake()
        {
            barTransform = GetComponent<RectTransform>();
            initialBarSize = barTransform.sizeDelta;
        }

        protected void UpdateBarHeight(int newValue)
        {
            var height = initialBarSize.y * newValue / MaxValue;
            barTransform.sizeDelta = new Vector2(initialBarSize.x, height);
        }
        
        protected void UpdateBarWidth(int newValue)
        {
            var width = initialBarSize.x * newValue / MaxValue;
            barTransform.sizeDelta = new Vector2(width, initialBarSize.y);
        }
    }
}