using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIBar : MonoBehaviour
    {
        [SerializeField] protected GameObject barGO;
        private Vector2 initialBarSize;
        private RectTransform barTransform;
        protected int MaxValue { get; set; }

        private void Start()
        {
            SetupBar();
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

        protected virtual void SetupBar()
        {
            barTransform = barGO.GetComponent<RectTransform>();
            initialBarSize = barTransform.sizeDelta;
        }
    }
}