using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    #region Cache

    #endregion

    private void Awake()
    {
        StartCoroutine(SpawnRoutine(AIManager.SpawnerNest.gameObject, AIManager.SpawnerNestSpawnConfig));
        StartCoroutine(SpawnRoutine(AIManager.MeleeMob.gameObject, AIManager.MeleeMobSpawnConfig));
        StartCoroutine(SpawnRoutine(AIManager.RangedMob.gameObject, AIManager.RangedMobSpawnConfig));
    }

    private IEnumerator SpawnRoutine(GameObject objectToSpawn, SpawnConfig spawnConfig)
    {
        AIManager.RecalculateMobCount();
        while (!AIManager.CanSpawnMobs) { yield return new WaitForSeconds(2f); }

        Vector3 spawnPos = AIUtils.FindSuitableRandomPosition(transform.position, spawnConfig.SpawnRadius);
        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

        float timeToWait = Random.Range(spawnConfig.MinSpawnTime, spawnConfig.MaxSpawnTime);
        yield return new WaitForSeconds(timeToWait);

        StartCoroutine(SpawnRoutine(objectToSpawn, spawnConfig));
    }



}

