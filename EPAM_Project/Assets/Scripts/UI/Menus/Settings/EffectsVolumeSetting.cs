using SaveData;

namespace UI.Menus.Settings
{
    public class EffectsVolumeSetting : VolumeSetting
    {
        public override void Apply()
        {
            AudioSettings.sfxMuted = muteButton.IsMuted;
            AudioSettings.sfxVolume = slider.value;
        }

        public override void Init(GameAudioSettings settings)
        {
            base.Init(settings);
            slider.value = settings.sfxVolume;
            muteButton.Mute(settings.sfxMuted);
        }
    }
}