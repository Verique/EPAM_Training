using System;
using UnityEngine;

namespace Stats
{
    public class BaseStatLoader<T> : MonoBehaviour where T: IStats<T>
    {
        [SerializeField] private T baseStats;

        private T currentStats;

        public T Stats => currentStats;

        protected void Awake()
        {
            currentStats = baseStats.Copy();
            
            if (currentStats is IHasHealthStat hasHealthStat)
            {
                GetComponent<Health>().Init(hasHealthStat.Health);
            }
        }
    }
}