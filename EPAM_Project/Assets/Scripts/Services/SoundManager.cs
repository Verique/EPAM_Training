using System;
using System.Collections.Generic;
using System.Linq;
using SaveData;
using UnityEngine;

namespace Services
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour, IService
    {
        private const string MusicPref = "musicVolume";
        private const string SfxPref = "effectsVolume";
        private const string MutedSuffix = "IsMuted";
        private const string AudioSourcePoolTag = "audio";
        [SerializeField] private bool has3DEffects;
        [SerializeField] private List<SoundInfo> sounds;
        private Dictionary<string, SoundInfo> soundDict;
        private AudioSource audioSource;
        private ObjectPool pool;

        private GameAudioSettings settings;
        
        private void Awake()
        {
            if (has3DEffects) pool = ServiceLocator.Instance.Get<ObjectPool>();
            soundDict = sounds.ToDictionary(info => info.clipName);
            audioSource = GetComponent<AudioSource>();
            ApplySettings();
        }

        public void PlayMusic(string soundTag)
        {
            audioSource.clip = GetClip(soundTag);
            audioSource.Play();
        }

        public void PlayOneShot(string soundTag)
        {
            if (settings.sfxMuted) return;
            var clip = GetClip(soundTag);
            audioSource.PlayOneShot(clip, settings.sfxVolume);
        }

        public void PlayAt(string soundTag, Vector3 position)
        {
            if (settings.sfxMuted) return;
            var audioSourceAtPosition = pool.Spawn<AudioSource>(AudioSourcePoolTag, position, Quaternion.identity);
            audioSourceAtPosition.clip = GetClip(soundTag);
            audioSourceAtPosition.volume = settings.sfxVolume;
            audioSourceAtPosition.Play();
        }
        
        private AudioClip GetClip(string soundTag)
        {
            if (!soundDict.ContainsKey(soundTag)) throw new InvalidOperationException($"No sound {soundTag} in SoundManager");
            return soundDict[soundTag].clip;
        }

        private void ApplySettings() => ApplySettings(ServiceLocator.Instance.Get<SaveManager>().LoadAudioSettings());

        public void ApplySettings(GameAudioSettings newSettings)
        {
            settings = newSettings;
            audioSource.volume = settings.musicVolume;
            audioSource.mute = settings.musicMuted;
        }
    }
}