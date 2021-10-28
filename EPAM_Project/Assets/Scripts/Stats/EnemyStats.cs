using System;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseEnemyStats", menuName = "Scriptable/EnemyStats")]
    public class EnemyStats : ScriptableObject, IStats<EnemyStats>, IHasHealthStat
    {
        public Stat<float> Speed => speed;
        [SerializeField] private Stat<float> speed;
        
        public Stat<int> Health => health;
        [SerializeField] private Stat<int> health;


        public EnemyStats Copy()
        {
            var newStats = CreateInstance<EnemyStats>();
            newStats.health = health.Copy();
            newStats.speed = speed.Copy();

            return newStats;
        }
    }
}