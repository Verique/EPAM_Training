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

        public T Get(string statName) => statsDict[statName].value;
    
        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            statsDict = stats.ToDictionary(stat => stat.name);
        }
    }
}