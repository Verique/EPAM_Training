using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const float levelSize = 500f;
    private const float spawnHeight = -5f;

    [SerializeField]
    private float timeToSpawn = 1f;

    private void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-levelSize, levelSize), Random.Range(-levelSize, levelSize), spawnHeight);
            ObjectPooler.Instance?.Spawn("enemy", spawnPos, Quaternion.identity);
            yield return new WaitForSecondsRealtime(timeToSpawn);
        }
    }
}
