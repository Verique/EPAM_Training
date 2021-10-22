using System;
using UnityEngine;

namespace Stats
{
    public class StatLoader : MonoBehaviour
    {
        [SerializeField] private BaseStats baseStats;

        public int GetInt(string statName) => baseStats.intStats.Get(statName);
        public float GetFloat(string statName) => baseStats.floatStats.Get(statName);
    }
}