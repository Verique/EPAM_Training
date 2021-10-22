using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseStats", menuName = "Scriptable/StatSet", order = 0)]
    public class BaseStats : ScriptableObject
    {
        public StatSet<int> intStats;
        public StatSet<float> floatStats;
    }
}
