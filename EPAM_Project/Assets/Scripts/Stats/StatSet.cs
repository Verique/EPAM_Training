using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stats
{
    public class StatSet<T> : ISerializationCallbackReceiver
    {
        [SerializeField] private List<Stat<T>> statsListForSerialization;
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
            
            statsDict[statName] = new Stat<T>(statName, value);
        }

        public void OnBeforeSerialize()
        {
            statsListForSerialization = statsDict.Values.ToList();
        }

        public void OnAfterDeserialize()
        {
            statsDict = statsListForSerialization.ToDictionary(stat => stat.name);
        }

        public StatSet(IEnumerable<Stat<T>> initialList)
        {
            statsDict = initialList.ToDictionary(stat => stat.name);
        }
    }
}