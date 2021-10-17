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

        private void Awake()
        {
            SetupBar();
        }

        public void UpdateBarHeight(int newValue)
        {
            var height = initialBarSize.y * newValue / MaxValue;
            barTransform.sizeDelta = new Vector2(initialBarSize.x, height);
        }

        protected virtual void SetupBar()
        {
            barTransform = GetComponent<RectTransform>();
            initialBarSize = barTransform.sizeDelta;
        }
    }
}