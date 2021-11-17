using System;
using System.Collections.Generic;
using System.Linq;
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

        private float musicVolume;
        private bool isMusicMuted;
        private float sfxVolume;
        private bool isSfxMuted;

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
            if (isSfxMuted) return;
            var clip = GetClip(soundTag);
            audioSource.PlayOneShot(clip, sfxVolume);
        }

        public void PlayAt(string soundTag, Vector3 position)
        {
            if (isSfxMuted) return;
            var audioSourceAtPosition = pool.Spawn<AudioSource>(AudioSourcePoolTag, position, Quaternion.identity);
            audioSourceAtPosition.clip = GetClip(soundTag);
            audioSourceAtPosition.volume = sfxVolume;
            audioSourceAtPosition.Play();
        }
        
        private AudioClip GetClip(string soundTag)
        {
            if (!soundDict.ContainsKey(soundTag)) throw new InvalidOperationException($"No sound {soundTag} in SoundManager");
            return soundDict[soundTag].clip;
        }

        public void ApplySettings()
        {
            musicVolume = PlayerPrefs.GetFloat(MusicPref, 1);
            audioSource.volume = musicVolume;
            isMusicMuted = bool.Parse(PlayerPrefs.GetString(MusicPref + MutedSuffix, "False"));
            audioSource.mute = isMusicMuted;
            
            sfxVolume = PlayerPrefs.GetFloat(SfxPref, 1);
            isSfxMuted = bool.Parse(PlayerPrefs.GetString(SfxPref + MutedSuffix, "False"));
        }
    }
}