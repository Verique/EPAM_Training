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
    public class PlayerManager : MonoBehaviour, IService
    {
        private const float DamageTakenAnimTime = 1f;
        
        private Transform player;
        
        private Renderer playerRenderer;

        public PlayerExperience Experience { get; private set; }
        public ITarget PlayerTarget { get; private set; }
        public PlayerStatLoader StatLoader { get; private set; }
        
        
        private void Awake()
        {
            player = FindObjectOfType<PlayerMovement>().transform;
            PlayerTarget = player.GetComponent<ITarget>();
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
            return new PlayerData (player.position.ToSerializable(), player.rotation.eulerAngles.ToSerializable(), StatLoader.Stats);
        }

        public void LoadData(PlayerData data)
        {
            player.position = data.position;
            player.rotation = Quaternion.Euler(data.rotation);
            StatLoader.LoadStats(data.stats);
        }
    }
}