using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus.Settings
{
    public abstract class SliderSetting : MonoBehaviour
    {
        [SerializeField] protected Slider slider;
        [SerializeField] protected Text text;

        public abstract void Apply();

        protected virtual void Start()
        {
            Init();
            ChangeSettingText(slider.value);
            slider.onValueChanged.AddListener(ChangeSettingText);
        }

        protected abstract void Init();
        
        protected virtual void ChangeSettingText(float value)
        {
            text.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}