using System;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class EnemyData
    {
        [SerializeField] private float speed;
        [SerializeField] private HealthData healthData;
        
        public EnemyData(HealthData healthData, float speed)
        {
            this.healthData = healthData;
            this.speed = speed;
        }

        public HealthData HealthData => healthData;
        public float Speed => speed;
    }
}