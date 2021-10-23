using System;
using System.Collections;
using Extensions;
using Player;
using SaveData;
using Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Services
{
    public class PlayerManager : MonoBehaviour, IService, ISaveable<PlayerData>
    {
        [SerializeField] private Transform player;
        public Transform Transform => player;

        public PlayerWeapon Weapon {get; private set; }
        public PlayerExperience Experience { get; private set; }
        
        private void Awake()
        {
            Weapon = player.GetComponent<PlayerWeapon>();
            Experience = player.GetComponent<PlayerExperience>();
            InitHealth();
        }

        #region Health
        
        private Renderer playerRenderer;
        public Health Health { get; private set; }
        private const int FramesToLerp = 80;
        private const float ColorLerpSpeed = 1 / (float) FramesToLerp;

        private void InitHealth()
        {
            Health = player.GetComponent<Health>();
            Health.HealthChanged += currentHealth => StartCoroutine(nameof(PlayerDamageTakenIndication));
            Health.IsDead += () => player.gameObject.SetActive(false);
            playerRenderer = player.GetComponentInChildren<Renderer>();
        }
        
        private IEnumerator PlayerDamageTakenIndication()
        {
            var frameCount = 0;

            while (frameCount < FramesToLerp)
            {
                playerRenderer.material.color = Color.Lerp(Color.red, Color.white, frameCount * ColorLerpSpeed);
                frameCount++;
                yield return new WaitForEndOfFrame();
            } 
        }
        
        #endregion


        public PlayerData GetSaveData()
        {
            var (currentHealth, maxHealth) = Health.GetSaveData();

            return new PlayerData
            {
                position = Transform.position.ToSerializable(),
                rotation = Transform.rotation.eulerAngles.ToSerializable(),
                currentHealth = currentHealth,
                currentClip = Weapon.GetSaveData(),
                maxHealth = maxHealth,
            };
        }

        public void LoadData(PlayerData data)
        {
            Transform.position = data.position;
            Transform.rotation = Quaternion.Euler(data.rotation);
            Health.LoadData((data.currentHealth, data.maxHealth).ToTuple());
            Weapon.LoadData(data.currentClip);
        }
    }
}