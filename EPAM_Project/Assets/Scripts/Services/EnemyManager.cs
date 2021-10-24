using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Extensions;
using SaveData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Services
{
    public class EnemyManager : MonoBehaviour, IService, ISaveable<List<EnemyData>>
    {
        private const string EnemyPoolTag = "enemy";
        private const float LevelSize = 500f;
        private const float SpawnHeight = -5f;
        private ObjectPool pool;

        public IEnumerable<EnemyBehaviour> Enemys => 
            pool.GetPooledObjects(EnemyPoolTag).Select(enemyGO => enemyGO.GetComponent<EnemyBehaviour>());
        [SerializeField] private float timeToSpawn = 1f;

        private void Awake()
        {
            pool = ServiceLocator.Instance.Get<ObjectPool>();
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                var spawnPos = new Vector3(Random.Range(-LevelSize, LevelSize), Random.Range(-LevelSize, LevelSize),
                    SpawnHeight);
                pool.Spawn(EnemyPoolTag, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(timeToSpawn);
            }
        }

        public void StartSpawning()
        {
            StartCoroutine(nameof(SpawnEnemy));
        }

        public void StopSpawning()
        {
            StopCoroutine(nameof(SpawnEnemy));
        }

        public List<EnemyData> GetSaveData()
        {
            return Enemys.Where((behaviour => behaviour.isActiveAndEnabled)).Select(behaviour =>
                new EnemyData
                {
                    position = behaviour.transform.position.ToSerializable(),
                    currentHealth = behaviour.GetComponent<Health>().GetSaveData().Item1
                }).ToList();
        }

        public void LoadData(List<EnemyData> data)
        {
            foreach (var enemy in Enemys)
            {
                enemy.gameObject.SetActive(false);
            }
            
            foreach (var eData in data)
            {
                var enemyGO = pool.Spawn(EnemyPoolTag, eData.position, Quaternion.identity);
                var eHealth = enemyGO.GetComponent<Health>();
                eHealth.LoadData(new Tuple<int, int>(eData.currentHealth, eHealth.MaxHealth));
            }
        }
    }
}