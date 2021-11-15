using Extensions;
using Player.Weapons;
using Services;
using UnityEngine;

namespace Enemy
{
    public class BossTurret : BaseEnemy
    {
        [SerializeField] private float spreadShotAngle;
        public override string PoolTag => "bossTurret";
        private string EnemyBulletTag => "enemyBullet";
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