using System;
using Stats;

namespace Enemy
{
    public class Boss : BaseEnemy
    {
        public override string PoolTag => "boss";

        public event Action<Stat<int>> BossDamaged; 
        public event Action BossKilled;

        protected override void Init()
        {
            base.Init();
            Stats.Health.ValueChanged += h => BossDamaged?.Invoke(Stats.Health);
            BossDamaged?.Invoke(Stats.Health);
        }

        protected override void OnKill(string dmgTag)
        {
            base.OnKill(dmgTag);
            BossKilled?.Invoke();
        }
    }
}