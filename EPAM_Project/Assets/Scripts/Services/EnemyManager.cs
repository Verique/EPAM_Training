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
            ("bossTurret", 0).ToTuple()
        };

        private const float LevelSize = 500f;
        private const float SpawnHeight = 5f;
        private const float TimeToSpawn = 1f;
        private ITarget player;
        private bool isSpawning;
        private ObjectPool pool;

        public event Action<Vector2> BossDirection;
        public event Action<string> BossSpawned;
        public event Action BossKilled;
        public event Action<Stat<int>> BossHealthChanged;

        private IEnumerable<GameObject> Enemys
        {
            get
            {
                var result = new List<GameObject>();
                foreach (var (poolTag, _) in poolTagWeights)
                {
                    var enemys = pool.GetPooledObjects(poolTag);

                    result.AddRange(enemys);
                }
                
                result.AddRange(pool.GetPooledObjects("boss"));

                return result;
            }
        }

        private void Awake()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
            isSpawning = true;
        }

        private IEnumerator SpawnEnemy()
        {
            while (isSpawning)
            {
                var poolTagToSpawn = ChooseEnemy();
                Spawn<MeleeEnemy>(poolTagToSpawn, RandomSpawnLocationWithinSquare(LevelSize));
                yield return new WaitForSeconds(TimeToSpawn);
            }
        }

        public T Spawn<T>(string poolTag, Vector3 location) where T : BaseEnemy
        {
            var enemy = pool.Spawn<T>(poolTag, location, Quaternion.identity);
            enemy.Player = player;
            return enemy;
        }

        public void SpawnBoss() => SpawnBossAt(RandomSpawnLocationWithinSquare(LevelSize));

        private void SpawnBossAt(Vector3 location)
        {
            var boss = Spawn<Boss>("boss", location);
            BossEvents(boss);
        }

        private void BossEvents(Boss boss)
        {
            BossSpawned?.Invoke(boss.PoolTag);
            boss.BossKilled += () => BossKilled?.Invoke();
            boss.BossDamaged += stat => BossHealthChanged?.Invoke(stat);
            boss.BossMoved += vector3 => BossDirection?.Invoke((vector3 - player.Position).ToVector2());
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

        private void StartSpawning()
        {
            StartCoroutine(nameof(SpawnEnemy));
        }

        private void StopSpawning()
        {
            StopCoroutine(nameof(SpawnEnemy));
        }

        public List<EnemyData> GetSaveData()
        {
            return Enemys
                .Select(obj => obj.GetComponent<BaseEnemy>())
                .Where((behaviour => behaviour.isActiveAndEnabled))
                .Select(behaviour => new EnemyData(
                    behaviour.transform.position.ToSerializable(), 
                    behaviour.GetComponent<EnemyStatLoader>().Stats,
                    behaviour.PoolTag))
                .ToList();
        }

        public void LoadData(IEnumerable<EnemyData> data)
        {
            foreach (var eData in data)
            {
                var enemy = Spawn<BaseEnemy>(eData.poolTag, eData.position);
                if (enemy is Boss b) BossEvents(b);
                enemy.GetComponent<EnemyStatLoader>().LoadStats(eData.stats);
            }
        }

        public void OnGameStart()
        {
            StartSpawning();
        }

        public void SetTarget(ITarget target)
        {
            player = target;
        }

        public void OnGameEnd()
        {
            StopSpawning();
            
            foreach (var enemy in Enemys)
            {
                enemy.SetActive(false);
            }
        }
    }
}