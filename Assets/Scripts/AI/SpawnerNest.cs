using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNest : Mob
{
    [SerializeField]
    private GameObject prefabToSpawn;

    [SerializeField]
    private float spawnTime = 5f;

    [SerializeField]
    private float spawnTimeDecreaseRate=0.2f;

    [SerializeField]
    private float minSpawnTime=0.1f;

    private float currentSpawnTime;

    public override void Awake()
    {
        base.Awake();

        receivedDamageKey = Messages.ReceivedDamageNest;
        diedKey = Messages.DiedNest;

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
