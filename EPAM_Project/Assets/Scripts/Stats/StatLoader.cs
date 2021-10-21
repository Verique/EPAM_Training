using System;
using UnityEngine;

namespace Stats
{
    public class StatLoader : MonoBehaviour
    {
        [SerializeField] private StatsScriptable initialStats;
        private StatSet<int> intStats; 
        private StatSet<float> floatStats;

        public void OnEnable()
        {
            intStats = new StatSet<int>(initialStats.intStats);
            floatStats = new StatSet<float>(initialStats.floatStats);
        }

        public int GetInt(string statName) => intStats.Get(statName);
        public float GetFloat(string statName) => floatStats.Get(statName);

        public void SetInt(string statName, int value) => intStats.Set(statName, value);
        public void SetFloat(string statName, float value) => floatStats.Set(statName, value);
    }
}