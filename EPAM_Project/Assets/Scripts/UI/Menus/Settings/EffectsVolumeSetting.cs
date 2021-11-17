using SaveData;

namespace UI.Menus.Settings
{
    public class EffectsVolumeSetting : VolumeSetting
    {
        public override void Apply()
        {
            audioSettings.sfxMuted = muteButton.IsMuted;
            audioSettings.sfxVolume = slider.value;
        }

        public override void Init(GameAudioSettings settings)
        {
            base.Init(settings);
            slider.value = settings.sfxVolume;
            muteButton.Mute(settings.sfxMuted);
        }
    }
}