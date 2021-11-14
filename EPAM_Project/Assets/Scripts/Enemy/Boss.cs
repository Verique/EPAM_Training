using System;
using Stats;

namespace Enemy
{
    public class Boss : BaseEnemy
    {
        public override string PoolTag => "boss";
        public Stat<int> Health => Stats.Health;
        
        public event Action BossKilled;
        public event Action<Boss> BossSpawned;

        protected override void Init()
        {
            base.Init();
            BossSpawned?.Invoke(this);
        }

        protected override void OnKill(string dmgTag)
        {
            base.OnKill(dmgTag);
            BossKilled?.Invoke();
        }
    }
}