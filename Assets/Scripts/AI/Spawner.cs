using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static int MAX_MOB_COUNT = 50;

    #region Cache
    private int _mobCount = 0;
    #endregion

    private void Awake()
    {
        StartCoroutine(SpawnRoutine(AIManager.SpawnerNest.gameObject, AIManager.SpawnerNestSpawnConfig));
        StartCoroutine(SpawnRoutine(AIManager.MeleeMob.gameObject, AIManager.MeleeMobSpawnConfig));
        StartCoroutine(SpawnRoutine(AIManager.RangedMob.gameObject, AIManager.RangedMobSpawnConfig));
    }

    private IEnumerator SpawnRoutine(GameObject objectToSpawn, SpawnConfig spawnConfig)
    {
        CalculateMobCount();
        while (_mobCount > MAX_MOB_COUNT) { yield return new WaitForSeconds(2f); }

        Vector3 spawnPos = AIUtils.FindSuitableRandomPosition(transform.position, spawnConfig.SpawnRadius);
        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

        float timeToWait = Random.Range(spawnConfig.MinSpawnTime, spawnConfig.MaxSpawnTime);
        yield return new WaitForSeconds(timeToWait);

        StartCoroutine(SpawnRoutine(objectToSpawn, spawnConfig));
    }

    private void CalculateMobCount()
    {
        _mobCount = GameObject.FindObjectsOfType<Mob>().Length;
        Sacristan.Logger.Log(string.Format("Found {0} mobs", _mobCount));
    }

}

