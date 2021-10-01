using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const float LevelSize = 500f;
    private const float SpawnHeight = -5f;

    [SerializeField] private float timeToSpawn = 1f;

    private void Start()
    {
        StartCoroutine(nameof(SpawnEnemy));
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            var spawnPos = new Vector3(Random.Range(-LevelSize, LevelSize), Random.Range(-LevelSize, LevelSize),
                SpawnHeight);
            ObjectPooler.Instance.Spawn("enemy", spawnPos, Quaternion.identity);
            yield return new WaitForSecondsRealtime(timeToSpawn);
        }
    }
}