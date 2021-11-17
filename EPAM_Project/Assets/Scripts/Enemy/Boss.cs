using System;
using Services;
using Stats;
using UnityEngine;

namespace Enemy
{
    public class Boss : BaseEnemy
    {
        private const float SpawnSize = 40f;
        public override string PoolTag => "boss";
        public const string TurretTag = "bossTurret";

        public event Action<Stat<int>> BossDamaged; 
        public event Action BossKilled;
        public event Action<Vector3> BossMoved;

        private EnemyManager eManager;

        protected override void Init()
        {
            base.Init();
            Stats.Health.ValueChanged += h => BossDamaged?.Invoke(Stats.Health);
            BossDamaged?.Invoke(Stats.Health);
            eManager = ServiceLocator.Instance.Get<EnemyManager>();
        }

        protected override void StartSkill()
        {
            base.StartSkill();
            var spawnLocation = eManager.RandomSpawnLocationWithinSquare(SpawnSize) + transform.position;
            eManager.Spawn<BossTurret>(TurretTag, spawnLocation);
            StartMove();
        }

        protected override void Move()
        {
            base.Move();
            BossMoved?.Invoke(transform.position);
        }

        protected override void OnKill(string dmgTag)
        {
            base.OnKill(dmgTag);
            BossKilled?.Invoke();
        }
    }
}