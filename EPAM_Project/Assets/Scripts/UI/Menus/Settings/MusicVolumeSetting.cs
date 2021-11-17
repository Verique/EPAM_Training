using SaveData;
using UnityEngine;

namespace UI.Menus.Settings
{
    public class MusicVolumeSetting : VolumeSetting
    {
        public override void Apply()
        {
            audioSettings.musicMuted = muteButton.IsMuted;
            audioSettings.musicVolume = slider.value;
        }

        public override void Init(GameAudioSettings settings)
        {
            base.Init(settings);
            slider.value = settings.musicVolume;
            muteButton.Mute(settings.musicMuted);
        }

    }
}