using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewStatSet", menuName = "Scriptable/StatSet", order = 0)]
    public class StatsScriptable : ScriptableObject
    {
        public StatSet<int> intStats;
        public StatSet<float> floatStats;
        
        public float GetFloat(string statName) => floatStats.Get(statName);
        public int GetInt(string statName) => intStats.Get(statName);
        
        public void SetFloat(string statName, float value) => floatStats.Set(statName, value);
        public void SetInt(string statName, int value) => intStats.Set(statName, value);
    }
}
