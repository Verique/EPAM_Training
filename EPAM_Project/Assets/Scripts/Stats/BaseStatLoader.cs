using System;
using UnityEngine;

namespace Stats
{
    public class BaseStatLoader<T> : MonoBehaviour where T: ScriptableObject, IStats<T>
    {
        [SerializeField] private T baseStats;

        public T Stats { get; private set; }

        protected void Awake()
        {
            Stats = ScriptableObject.CreateInstance<T>();
            Stats.Copy(baseStats);
            
            if (Stats is IHasHealthStat hasHealthStat)
            {
                GetComponent<Health>().Init(hasHealthStat.Health);
            }
        }

        public void LoadStats(T stats)
        {
            Stats.Copy(stats);
        }
    }
}