using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Extensions;
using SaveData;
using Stats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services
{
    public class EnemyManager : MonoBehaviour, IService
    {
        private readonly Tuple<string, int>[] poolTagWeights = 
        {
            ("meleeEnemy", 5).ToTuple(),
            ("dashEnemy", 1).ToTuple(),
            ("bossTurret", 0).ToTuple(),
            ("boss", 0).ToTuple()
        };

        private const float LevelSize = 500f;
        private const float SpawnHeight = 5f;
        private const float TimeToSpawn = 1f;
        
        private bool isSpawning;
        private ObjectPool pool;

        public event Action<int> EnemyKilledExp;
        public event Action<Vector2> BossDirection;
        public event Action<string> BossSpawned;
        public event Action BossKilled;
        public event Action<Stat<int>> BossHealthChanged;

        public ITarget Target { get; set; }

        private IEnumerable<GameObject> Enemys
        {
            get
            {
                var result = new List<GameObject>();
                foreach (var (poolTag, _) in poolTagWeights) result.AddRange(pool.GetPooledObjects(poolTag));
                return result;
            }
        }

        private void OnEnemyKilled(int exp) => EnemyKilledExp?.Invoke(exp);

        private IEnumerator SpawnEnemy()
        {
            while (isSpawning)
            {
                var poolTagToSpawn = ChooseEnemy();
                Spawn(poolTagToSpawn, RandomSpawnLocationWithinSquare(LevelSize));
                yield return new WaitForSeconds(TimeToSpawn);
            }
        }

        public BaseEnemy Spawn(string poolTag, Vector3 location)
        {
            var enemy = pool.Spawn<BaseEnemy>(poolTag, location, Quaternion.identity);
            if (enemy is Boss b) BossEvents(b);
            enemy.Player = Target;
            return enemy;
        }

        private void BossEvents(Boss boss)
        {
            BossSpawned?.Invoke(boss.PoolTag);
            boss.EnemyKilledExp += exp => BossKilled?.Invoke();
            boss.BossDamaged += stat => BossHealthChanged?.Invoke(stat);
            boss.BossMoved += vector3 => BossDirection?.Invoke((vector3 - Target.Position).ToVector2());
        }

        public Vector3 RandomSpawnLocationWithinSquare(float size)
        {
            Vector3 spawnLocation;

            do
            {
                spawnLocation = new Vector3(
                    Random.Range(-size, size),
                    SpawnHeight,
                    Random.Range(-size, size));
            } 
            while (Physics.CheckSphere(spawnLocation, 2, ~LayerMask.NameToLayer("Terrain")));

            return spawnLocation;
        }

        private string ChooseEnemy()
        {
            var overallWeight = poolTagWeights.Select(pair => pair.Item2).Sum();
            var roll = Random.Range(1, overallWeight + 1);
            var currentArea = 0;
            
            foreach (var (poolTag, weight) in poolTagWeights)
            {
                if (weight == 0) continue;
                currentArea += weight;
                if (roll <= currentArea) return poolTag;
            }

            return "";
        }

        public IEnumerable<EnemyData> GetSaveData()
        {
            return Enemys
                .Select(obj => obj.GetComponent<BaseEnemy>())
                .Where(behaviour => behaviour.isActiveAndEnabled)
                .Select(behaviour => new EnemyData(
                    behaviour.transform.position.ToSerializable(),
                    behaviour.GetComponent<EnemyStatLoader>().Stats,
                    behaviour.PoolTag));
        }

        public void LoadData(IEnumerable<EnemyData> data)
        {
            foreach (var eData in data)
            {
                var enemy = Spawn(eData.poolTag, eData.position);
                enemy.GetComponent<EnemyStatLoader>().LoadStats(eData.stats);
            }
        }

        private void LinkEnemyDeath()
        {
            foreach (var enemy in Enemys.Select(o => o.GetComponent<MeleeEnemy>()))
            {
                enemy.EnemyKilledExp += OnEnemyKilled;
            }
        }
        
        public void OnGameStart()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
            ServiceLocator.Instance.Get<GameManager>().GoalReached += i => Spawn("boss", RandomSpawnLocationWithinSquare(LevelSize));
            LinkEnemyDeath();
            StartSpawning();
        }

        public void OnGameEnd()
        {
            StopSpawning();
            
            foreach (var enemy in Enemys)
            {
                enemy.SetActive(false);
            }
        }
        
        private void StartSpawning()
        {
            isSpawning = true;
            StartCoroutine(nameof(SpawnEnemy));
        }

        private void StopSpawning()
        {
            isSpawning = false;
        }
    }
}