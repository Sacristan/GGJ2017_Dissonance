using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNestBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn;

    [SerializeField]
    private float spawnTime = 5f;

    [SerializeField]
    private float spawnTimeDecreaseRate=0.2f;

    [SerializeField]
    private float minSpawnTime=0.1f;

    //[SerializeField]
    //private float spawnRadius = 10f;
    private float currentSpawnTime;

    private void Awake()
    {
        currentSpawnTime = spawnTime;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        SpawnPrefab();
        yield return new WaitForSeconds(spawnTime);

        currentSpawnTime = Mathf.Clamp(currentSpawnTime-spawnTimeDecreaseRate, minSpawnTime, currentSpawnTime);
        StartCoroutine(SpawnRoutine());
    }

    private void SpawnPrefab()
    {
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}
