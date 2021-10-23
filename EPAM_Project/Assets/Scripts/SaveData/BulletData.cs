using System;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class BulletData
    {
        [SerializeField] private float speed;
        [SerializeField] private HealthData healthData;
        
        public BulletData(HealthData healthData, float speed)
        {
            this.healthData = healthData;
            this.speed = speed;
        }

        public HealthData HealthData => healthData;
        public float Speed => speed;
    }
}