using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

namespace Stats
{
    public class BulletDataLoader : MonoBehaviour
    {
        [SerializeField] private int baseMaxHealth;
        [SerializeField] private float baseSpeed;
        [SerializeField] private List<string> baseDamageSourceTags;
        [SerializeField] private float baseInvTime;
        
        public BulletData Data;

        private void Awake()
        {
            Data = new BulletData(new HealthData(baseMaxHealth, baseInvTime, baseDamageSourceTags), baseSpeed);
        }

        private void Start()
        {
            GetComponent<Health>().Init(Data.HealthData);
        }
    }
}