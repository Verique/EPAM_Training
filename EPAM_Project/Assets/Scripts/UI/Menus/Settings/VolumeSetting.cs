using UnityEngine;

namespace UI.Menus.Settings
{
    public abstract class VolumeSetting : SliderSetting
    {
        [SerializeField] private MuteButton muteButton;

        public abstract string PrefName { get; }

        protected override void Init()
        {
            slider.maxValue = 1;
            slider.minValue = 0;
            slider.wholeNumbers = false;
            slider.value = PlayerPrefs.GetFloat(PrefName);
            
            var isMuted = bool.Parse(PlayerPrefs.GetString(PrefName + "IsMuted"));
            muteButton.Mute(isMuted);
        }

        public override void Apply()
        {
            var value = slider.value;
            var isMuted = muteButton.IsMuted.ToString();
            
            PlayerPrefs.SetFloat(PrefName, value);
            PlayerPrefs.SetString(PrefName + "IsMuted", isMuted);
        }

        protected override void ChangeSettingText(float value)
        {
            text.text = ((int)(value * 100)).ToString();
        }
    }
}