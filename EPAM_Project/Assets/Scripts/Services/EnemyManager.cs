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
        private const string EnemyPoolTag = "enemy";
        private const float LevelSize = 500f;
        private const float SpawnHeight = 5f;
        private const float TimeToSpawn = 1f;
        private ObjectPool pool;

        private IEnumerable<MeleeEnemy> Enemys => 
            pool.GetPooledObjects(EnemyPoolTag).Select(enemyGO => enemyGO.GetComponent<MeleeEnemy>());

        private void Awake()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                var spawnPos = new Vector3(
                    Random.Range(-LevelSize, LevelSize), 
                    SpawnHeight,
                    Random.Range(-LevelSize, LevelSize));
                
                pool.Spawn(EnemyPoolTag, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(TimeToSpawn);
            }
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
                .Where((behaviour => behaviour.isActiveAndEnabled))
                .Select(behaviour => new EnemyData(behaviour.transform.position.ToSerializable(), behaviour.GetComponent<EnemyStatLoader>().Stats))
                .ToList();
        }

        public void LoadData(List<EnemyData> data)
        {
            foreach (var enemy in Enemys)
            {
                enemy.gameObject.SetActive(false);
            }
            
            foreach (var eData in data)
            {
                pool.Spawn<EnemyStatLoader>(EnemyPoolTag, eData.position, Quaternion.identity).LoadStats(eData.stats);
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
                enemy.gameObject.SetActive(false);
            }
        }
    }
}