using System;
using System.Collections;
using Enemy;
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
        public PlayerWeapon Weapon {get; private set; }
        public PlayerExperience Experience { get; private set; }
        public Vector3 Position => player.position;
        public ITarget PlayerTarget { get; private set; }
        
        private void Awake()
        {
            PlayerTarget = player.GetComponent<ITarget>();
            Weapon = player.GetComponent<PlayerWeapon>();
            Experience = player.GetComponent<PlayerExperience>();
            InitHealth();
        }

        #region Health
        
        private Renderer playerRenderer;
        public Health Health { get; private set; }

        private void InitHealth()
        {
            Health = player.GetComponent<Health>();
            Health.HealthChanged += currentHealth => StartCoroutine(nameof(PlayerDamageTakenIndication));
            Health.IsDead += () => player.gameObject.SetActive(false);
            playerRenderer = player.GetComponentInChildren<Renderer>();
        }
        
        private IEnumerator PlayerDamageTakenIndication()
        {
            var startTime = Time.time;
            var currentTimeDiff = 0f;
            
            while (currentTimeDiff < Health.InvTime)
            {
                playerRenderer.material.color = Color.Lerp(Color.red, Color.white, currentTimeDiff / Health.InvTime);
                yield return new WaitForEndOfFrame();
                currentTimeDiff = Time.time - startTime;
            } 
        }
        
        #endregion


        public PlayerData GetSaveData()
        {
            var (currentHealth, maxHealth) = Health.GetSaveData();

            return new PlayerData
            {
                position = player.position.ToSerializable(),
                rotation = player.rotation.eulerAngles.ToSerializable(),
                currentHealth = currentHealth,
                currentClip = Weapon.GetSaveData(),
                maxHealth = maxHealth,
            };
        }

        public void LoadData(PlayerData data)
        {
            player.position = data.position;
            player.rotation = Quaternion.Euler(data.rotation);
            Health.LoadData((data.currentHealth, data.maxHealth).ToTuple());
            Weapon.LoadData(data.currentClip);
        }

        public void SubscribeToPlayerDeath(Action act)
        {
            Health.IsDead += act;
        }
    }
}