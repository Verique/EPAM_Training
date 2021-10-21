using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewStatSet", menuName = "Scriptable/StatSet", order = 0)]
    public class StatsScriptable : ScriptableObject
    {
        [SerializeField] private StatSet<int> intStats;
        [SerializeField] private StatSet<float> floatStats;

        public float GetFloat(string statName) => floatStats.Get(statName);
        public float GetInt(string statName) => intStats.Get(statName);
    }
}
