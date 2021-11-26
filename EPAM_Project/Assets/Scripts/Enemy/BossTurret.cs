using Extensions;
using Player.Weapons;
using Services;

namespace Enemy
{
    public class BossTurret : BaseEnemy
    {
        private const string EnemyBulletTag = "enemyBullet";
        public override string PoolTag => "bossTurret";
        
        private ObjectPool pool;

        protected override void Init()
        {
            base.Init();
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        protected override void StartAttack()
        {
            base.StartAttack();

            var position = transform.position;
            var diff = Player.Position - position;
            var rotation = diff.ToRotation();
            
            pool.Spawn<EnemyBullet>(EnemyBulletTag, position + diff.normalized * 10, rotation);
            StartMove();
        }

    }
}