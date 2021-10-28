using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseEnemyStats", menuName = "Scriptable/EnemyStats")]
    [Serializable]
    public class EnemyStats : ScriptableObject, IHasHealthStat
    {
        [field: SerializeField] public Stat<float> Speed { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<int> Health { get; private set; } = new Stat<int>();
    }
}