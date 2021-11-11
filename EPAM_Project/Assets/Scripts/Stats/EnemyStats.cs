using System;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseEnemyStats", menuName = "Scriptable/Stats/EnemyStats")]
    [Serializable]
    public class EnemyStats : ScriptableObject, IHasHealthStat
    {
        [field: SerializeField] public Stat<float> Speed { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<float> AttackTime { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<float> AttackDistance { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<float> SkillDistance { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<float> SkillCooldown { get; private set; } = new Stat<float>();
        [field: SerializeField] public Stat<int> Health { get; private set; } = new Stat<int>();
        [field: SerializeField] public Stat<int> Damage { get; private set; } = new Stat<int>();
    }
}