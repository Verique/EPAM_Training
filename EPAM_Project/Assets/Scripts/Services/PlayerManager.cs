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
        private const float DamageTakenAnimTime = 1f;
        
        private Transform player;
        
        private Renderer playerRenderer;
        public PlayerWeapon Weapon {get; private set; }
        public PlayerExperience Experience { get; private set; }
        public ITarget PlayerTarget { get; private set; }
        public PlayerStatLoader StatLoader { get; private set; }
        
        
        private void Awake()
        {
            player = FindObjectOfType<PlayerMovement>().transform;
            PlayerTarget = player.GetComponent<ITarget>();
            Weapon = player.GetComponent<PlayerWeapon>();
            Experience = player.GetComponent<PlayerExperience>();
            playerRenderer = player.GetComponentInChildren<Renderer>();
            StatLoader = player.GetComponent<PlayerStatLoader>();
        }

        private void Start()
        {
            var stats = StatLoader.Stats;
            stats.Health.MinValueReached += () => player.gameObject.SetActive(false);
            stats.Health.ValueChanged += currentHealth => StartCoroutine(nameof(PlayerDamageTakenIndication));
        }

        private IEnumerator PlayerDamageTakenIndication()
        {
            var startTime = Time.time;
            var currentTimeDiff = 0f;
            
            while (currentTimeDiff < DamageTakenAnimTime)
            {
                playerRenderer.material.color = Color.Lerp(Color.red, Color.white, currentTimeDiff / DamageTakenAnimTime);
                yield return new WaitForEndOfFrame();
                currentTimeDiff = Time.time - startTime;
            } 
        }
        

        public PlayerData GetSaveData()
        {
            //var (currentHealth, maxHealth) = Health.GetSaveData();

            return new PlayerData
            {
                position = player.position.ToSerializable(),
                rotation = player.rotation.eulerAngles.ToSerializable(),
                //currentHealth = currentHealth,
                currentClip = Weapon.GetSaveData(),
                //maxHealth = maxHealth,
            };
        }

        public void LoadData(PlayerData data)
        {
            player.position = data.position;
            player.rotation = Quaternion.Euler(data.rotation);
           // Health.LoadData((data.currentHealth, data.maxHealth).ToTuple());
            Weapon.LoadData(data.currentClip);
        }
    }
}