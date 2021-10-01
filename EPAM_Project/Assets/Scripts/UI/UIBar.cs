using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class UIBar : MonoBehaviour
    {
        private Vector2 initialBarSize;
        private RectTransform barTransform;

        protected int MaxValue;
        protected Image Image;

        private void Start()
        {
            SetupBar();
        }

        protected void UpdateBar(int newValue)
        {
            var height = initialBarSize.y * newValue / MaxValue;
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