using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNestSpawner : MonoBehaviour
{
    [SerializeField]
    private float minSpawnTime = 5f;

    [SerializeField]
    private float maxSpawnTime = 15f;

    [SerializeField]
    private float spawnRadius = 100f;

    private void Awake()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        Vector3 spawnPos = AIUtils.FindSuitableRandomPosition(transform.position, spawnRadius);
        Instantiate(AIManager.SpawnerNest, spawnPos, Quaternion.identity);

        float timeToWait = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(timeToWait);

        StartCoroutine(SpawnRoutine());
    }


}

