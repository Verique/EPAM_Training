using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class EnemySpawner : MonoBehaviour, IService
    {
        private const string EnemyPoolTag = "enemy";
        private const float LevelSize = 500f;
        private const float SpawnHeight = -5f;
        private ObjectPool pool;

        public IEnumerable<GameObject> Enemys => pool.GetPooledObjects(EnemyPoolTag);
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
    }
}