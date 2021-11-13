using System;

namespace Enemy
{
    public class Boss : BaseEnemy
    {
        public override string PoolTag => "boss";
        
        public event Action BossKilled;

        protected override void OnKill(string dmgTag)
        {
            base.OnKill(dmgTag);
            BossKilled?.Invoke();
        }
    }
}