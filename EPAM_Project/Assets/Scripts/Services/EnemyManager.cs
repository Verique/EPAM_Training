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
        private readonly Tuple<string, int>[] poolTagWeights = new[]
        {
            ("meleeEnemy", 5).ToTuple(),
            ("dashEnemy", 1).ToTuple()
        };

        private const float LevelSize = 500f;
        private const float SpawnHeight = 5f;
        private const float TimeToSpawn = 1f;
        private bool isSpawning;
        private ObjectPool pool;

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
                var spawnPos = new Vector3(
                    Random.Range(-LevelSize, LevelSize), 
                    SpawnHeight,
                    Random.Range(-LevelSize, LevelSize));

                var poolTagToSpawn = ChooseEnemy();
                pool.Spawn(poolTagToSpawn, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(TimeToSpawn);
            }
        }

        private string ChooseEnemy()
        {
            var overallWeight = poolTagWeights.Select(pair => pair.Item2).Sum();

            var roll = Random.Range(1, overallWeight + 1);

            var currentArea = 0;
            
            foreach (var (poolTag, weight) in poolTagWeights)
            {
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
                .Select(obj => obj.GetComponent<MeleeEnemy>())
                .Where((behaviour => behaviour.isActiveAndEnabled))
                .Select(behaviour => new EnemyData(
                    behaviour.transform.position.ToSerializable(), 
                    behaviour.GetComponent<EnemyStatLoader>().Stats,
                    behaviour.GetComponent<Poolable>().PoolTag))
                .ToList();
        }

        public void LoadData(IEnumerable<EnemyData> data)
        {
            foreach (var eData in data)
            {
                pool.Spawn<EnemyStatLoader>(eData.poolTag, eData.position, Quaternion.identity).LoadStats(eData.stats);
            }
        }

        public void OnGameStart()
        {
            StartSpawning();
        }

        public void SetTarget(ITarget target)
        {
            foreach (var enemy in Enemys)
            {
                enemy.GetComponent<MeleeEnemy>().Player = target;
            }
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