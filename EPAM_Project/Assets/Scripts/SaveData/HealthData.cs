using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class HealthData
    {
        [SerializeField] private int currentHealth;
        public int maxHealth;
        public float invTime;
        public List<string> damageSourceTags;

        public HealthData(int maxHealth, float invTime, List<string> damageSourceTags)
        {
            currentHealth = maxHealth;
            this.maxHealth = maxHealth;
            this.invTime = invTime;
            this.damageSourceTags = damageSourceTags;
        }

        public event Action<int> HealthChanged; 
        public event Action IsDead; 

        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                HealthChanged?.Invoke(value);
                
                if (value <= 0)
                    IsDead?.Invoke();
            }
        }
    }
}