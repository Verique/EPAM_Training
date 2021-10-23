using System;
using System.Collections.Generic;
using Player;
using SaveData;
using UnityEngine;

namespace Stats
{
    [RequireComponent(typeof(Health))]
    public class PlayerDataLoader : MonoBehaviour
    {
        [SerializeField] private float baseSpeed;
        [SerializeField] private int baseMaxHealth;
        [SerializeField] private List<string> baseDamageSourceTags;
        [SerializeField] private float baseInvTime;

        public PlayerData playerData;

        public HealthData GetHealthData => playerData.HealthData; 
        
        private void Awake()
        {
            playerData = new PlayerData(new HealthData(baseMaxHealth, baseInvTime, baseDamageSourceTags), baseSpeed);
        }

        private void Start()
        {
            GetComponent<Health>().Init(playerData.HealthData);
        }
    }
}