using Newtonsoft.Json;
using UnityEngine;

namespace Stats
{
    public class BaseStatLoader<T> : MonoBehaviour where T: ScriptableObject
    {
        [SerializeField] private T baseStats;

        public T Stats { get; private set; }

        protected void Awake()
        {
            Stats = ScriptableObject.CreateInstance<T>();
            LoadStats(baseStats);
            
            if (Stats is IHasHealthStat hasHealthStat)
            {
                GetComponent<Health>().Init(hasHealthStat.Health);
            }
        }

        public void LoadStats(T stats)
        {
            var jsonString = JsonConvert.SerializeObject(stats);
            
            JsonConvert.PopulateObject(jsonString, Stats);
        }
    }
}