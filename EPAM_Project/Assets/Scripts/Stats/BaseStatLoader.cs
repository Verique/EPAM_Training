using Newtonsoft.Json;
using UnityEngine;

namespace Stats
{
    public class BaseStatLoader<T> : MonoBehaviour where T: ScriptableObject
    {
        [SerializeField] private T baseStats;
        private T stats;

        public T Stats
        {
            get
            {
                if (stats == null)
                {
                    stats = ScriptableObject.CreateInstance<T>();
                    LoadStats(baseStats);
                }
                return stats;
            }
        }

        protected void Awake()
        {
            if (Stats is IHasHealthStat hasHealthStat)
            {
                GetComponent<Health>().Init(hasHealthStat.Health);
            }
        }

        public void LoadStats(T newStats)
        {
            var jsonString = JsonConvert.SerializeObject(newStats);
            
            JsonConvert.PopulateObject(jsonString, Stats);
        }
    }
}