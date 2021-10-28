using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseBulletStats", menuName = "Scriptable/BulletStats")]
    public class BulletStats : ScriptableObject, IStats<BulletStats>, IHasHealthStat
    {
        public Stat<float> Speed => speed;
        [SerializeField] private Stat<float> speed;
        
        public Stat<int> Health => health;
        [SerializeField] private Stat<int> health;

        public BulletStats Copy()
        {
            var newStats = CreateInstance<BulletStats>();
            newStats.health = health.Copy();
            newStats.speed = speed.Copy();

            return newStats;
        }

    }
}