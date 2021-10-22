using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class StatSet<T> : ISerializationCallbackReceiver
    {
        public List<Stat<T>> statsList;
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
            statsList = statsDict?.Values.ToList();
        }

        public void OnAfterDeserialize()
        {
            statsDict = statsList.ToDictionary(stat => stat.name);
        }

        public StatSet(IEnumerable<Stat<T>> initialList)
        {
            statsDict = initialList.ToDictionary(stat => stat.name);
        }
    }
}