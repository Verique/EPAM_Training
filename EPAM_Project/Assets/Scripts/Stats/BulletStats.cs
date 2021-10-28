using System;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "NewBaseBulletStats", menuName = "Scriptable/BulletStats")]
    [Serializable]
    public class BulletStats : ScriptableObject, IStats<BulletStats>, IHasHealthStat
    {
        public Stat<float> Speed => speed;
        [SerializeField] private Stat<float> speed = new Stat<float>();
        
        public Stat<int> Health => health;
        [SerializeField] private Stat<int> health = new Stat<int>();

        public void Copy(BulletStats from)
        {
            health.Copy(from.health);
            speed.Copy(from.speed);
        }
    }
}