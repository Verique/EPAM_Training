using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewStatSet", menuName = "Scriptable/StatSet", order = 0)]
    public class StatsScriptable : ScriptableObject
    {
        public List <Stat<int>> intStats;
        public List <Stat<float>> floatStats;
    }
}
