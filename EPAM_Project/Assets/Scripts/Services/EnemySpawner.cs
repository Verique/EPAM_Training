using System.Collections;
using Services;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour, IService
    {
        private const float LevelSize = 500f;
        private const float SpawnHeight = -5f;
        private ObjectPool pool;

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
                pool.Spawn("enemy", spawnPos, Quaternion.identity);
                yield return new WaitForSecondsRealtime(timeToSpawn);
            }
        }

        public void StartSpawning()
        {
            StartCoroutine(nameof(SpawnEnemy));
        }
    }
}