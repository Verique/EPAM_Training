using SaveData;

namespace UI.Menus.Settings
{
    public class MusicVolumeSetting : VolumeSetting
    {
        public override void Apply()
        {
            AudioSettings.musicMuted = muteButton.IsMuted;
            AudioSettings.musicVolume = slider.value;
        }

        public override void Init(GameAudioSettings settings)
        {
            base.Init(settings);
            slider.value = settings.musicVolume;
            muteButton.Mute(settings.musicMuted);
        }

    }
}