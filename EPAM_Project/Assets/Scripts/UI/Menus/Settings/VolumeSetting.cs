using SaveData;
using UnityEngine;

namespace UI.Menus.Settings
{
    public abstract class VolumeSetting : SliderSetting
    {
        [SerializeField] protected MuteButton muteButton;

        protected GameAudioSettings AudioSettings;
        public virtual void Init(GameAudioSettings settings)
        {
            slider.maxValue = 1;
            slider.minValue = 0;
            slider.wholeNumbers = false;
            muteButton.Init();
            AudioSettings = settings;
        }

        protected override void Init()
        { }

        public abstract override void Apply();

        protected override void ChangeSettingText(float value)
        {
            text.text = ((int)(value * 100)).ToString();
        }
    }
}