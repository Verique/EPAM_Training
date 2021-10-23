using System;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

namespace Stats
{
    public class EnemyDataLoader : MonoBehaviour
    {
        [SerializeField] private int baseMaxHealth;
        [SerializeField] public float baseSpeed;
        [SerializeField] private List<string> baseDamageSourceTags;
        [SerializeField] private float baseInvTime;

        public EnemyData Data;

        private void Awake()
        {
            Data = new EnemyData(new HealthData(baseMaxHealth, baseInvTime, baseDamageSourceTags), baseSpeed);
        }

        private void Start()
        {
            GetComponent<Health>().Init(Data.HealthData);
        }
    }
}