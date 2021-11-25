using System;
using Services;
using Stats;
using UnityEngine;

namespace Enemy
{
    public class Boss : BaseEnemy
    {
        private const float SpawnSize = 40f;
        private const string TurretTag = "bossTurret";
        public override string PoolTag => "boss";

        public event Action<Stat<int>> BossDamaged; 
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
            eManager.Spawn(TurretTag, spawnLocation);
            StartMove();
        }

        protected override void Move()
        {
            base.Move();
            BossMoved?.Invoke(transform.position);
        }
    }
}