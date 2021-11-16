using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour, IService
    {
        private const string AudioSourcePoolTag = "audio";
        [SerializeField] private List<SoundInfo> sounds;
        private Dictionary<string, SoundInfo> soundDict;
        private AudioSource audioSource;
        private ObjectPool pool;

        private void Awake()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
            soundDict = sounds.ToDictionary(info => info.clipName);
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayMusic(string soundTag)
        {
            audioSource.clip = GetClip(soundTag);
            audioSource.Play();
        }

        public void PlayOneShot(string soundTag)
        {
            var clip = GetClip(soundTag);
            audioSource.PlayOneShot(clip);
        }

        public void PlayAt(string soundTag, Vector3 position)
        {
            var audioSourceAtPosition = pool.Spawn<AudioSource>(AudioSourcePoolTag, position, Quaternion.identity);
            audioSourceAtPosition.clip = GetClip(soundTag);
            audioSourceAtPosition.Play();
        }
        
        private AudioClip GetClip(string soundTag)
        {
            if (!soundDict.ContainsKey(soundTag)) throw new InvalidOperationException($"No sound {soundTag} in SoundManager");
            return soundDict[soundTag].clip;
        }
    }
}