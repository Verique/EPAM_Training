using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class StatSet<T> : ISerializationCallbackReceiver
    {
        [SerializeField] private List <Stat<T>> stats;
        private Dictionary<string, Stat<T>> statsDict;

        public T Get(string statName)
        {
            if (!statsDict.ContainsKey(statName)) 
                throw new InvalidOperationException($"There is no {statName} in stats");
            
            return statsDict[statName].value;
        }
        
        public void Set(string statName, T value)
        {
            if (!statsDict.ContainsKey(statName)) 
                throw new InvalidOperationException($"There is no {statName} in stats");
            
            statsDict[statName].value = value;
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            statsDict = stats.ToDictionary(stat => stat.name);
        }
    }
}