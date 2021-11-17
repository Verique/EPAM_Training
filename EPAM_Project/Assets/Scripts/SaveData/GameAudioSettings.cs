using System;

namespace SaveData
{
    [Serializable]
    public class GameAudioSettings
    {
        public float musicVolume = 1;
        public float sfxVolume = 1;
        public bool musicMuted = false;
        public bool sfxMuted = false;
    }
}