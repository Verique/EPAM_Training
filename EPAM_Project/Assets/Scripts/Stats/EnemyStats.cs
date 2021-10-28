using System;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseEnemyStats", menuName = "Scriptable/EnemyStats")]
    [Serializable]
    public class EnemyStats : ScriptableObject, IStats<EnemyStats>, IHasHealthStat
    {
        public Stat<float> Speed => speed;
        [SerializeField] private Stat<float> speed = new Stat<float>();
        
        public Stat<int> Health => health;
        [SerializeField] private Stat<int> health = new Stat<int>();


        public void Copy(EnemyStats from)
        {
            health.Copy(from.health);
            speed.Copy(from.speed);
        }
    }
}