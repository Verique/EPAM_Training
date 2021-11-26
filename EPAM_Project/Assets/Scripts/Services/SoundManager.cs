using System;
using System.Collections.Generic;
using System.Linq;
using SaveData;
using UnityEngine;

namespace Services
{
    public class SoundManager : MonoBehaviour, IService
    {
        private const string AudioSourcePoolTag = "audio";
        
        [SerializeField] private List<SoundInfo> sounds;
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource sfxAudioSource;
        
        private Dictionary<string, SoundInfo> soundDict;
        private ObjectPool pool;

        private void Awake()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
            soundDict = sounds.ToDictionary(info => info.clipName);
            ApplySettings();
        }

        public void PlayMusic(string soundTag)
        {
            musicAudioSource.clip = GetClip(soundTag);
            if (musicAudioSource.mute) return;
            musicAudioSource.Play();
        }

        public void PlayOneShot(string soundTag)
        {
            if (sfxAudioSource.mute) return;
            var clip = GetClip(soundTag);
            sfxAudioSource.PlayOneShot(clip, sfxAudioSource.volume);
        }

        public void PlayAt(string soundTag, Vector3 position)
        {
            if (sfxAudioSource.mute) return;
            var audioSourceAtPosition = pool.Spawn<AudioSource>(AudioSourcePoolTag, position, Quaternion.identity);
            audioSourceAtPosition.clip = GetClip(soundTag);
            audioSourceAtPosition.volume = sfxAudioSource.volume;
            audioSourceAtPosition.Play();
        }
        
        private AudioClip GetClip(string soundTag)
        {
            if (!soundDict.ContainsKey(soundTag)) throw new InvalidOperationException($"No sound {soundTag} in SoundManager");
            return soundDict[soundTag].clip;
        }

        private void ApplySettings() => ApplySettings(ServiceLocator.Instance.Get<SaveManager>().LoadAudioSettings());

        public void ApplySettings(GameAudioSettings settings)
        {
            musicAudioSource.volume = settings.musicVolume;
            musicAudioSource.mute = settings.musicMuted;
            if (settings.musicMuted) musicAudioSource.Stop();
            else musicAudioSource.Play();
            sfxAudioSource.mute = settings.sfxMuted;
            sfxAudioSource.volume = settings.sfxVolume;
        }
    }
}