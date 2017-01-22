using Sacristan.Messaging;
using UnityEngine;

[System.Serializable]
public class SpawnConfig
{
    #region Fields
    [SerializeField]
    private float minSpawnTime = 5f;

    [SerializeField]
    private float maxSpawnTime = 15f;

    [SerializeField]
    private float spawnRadius = 100f;
    #endregion

    #region Properties
    public float MinSpawnTime { get { return minSpawnTime; } }
    public float MaxSpawnTime { get { return maxSpawnTime; } }
    public float SpawnRadius { get { return spawnRadius; } }
    #endregion
}

public class AIManager : MonoBehaviour
{
    private static int MAX_MOB_COUNT = 50;

    private static AIManager singletone;

    #region Fields

    [Header("AI Actors")]
    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private MeleeMob meleeMob;

    [SerializeField]
    private RangedMob rangedMob;

    [SerializeField]
    private SpawnerNest spawnerNest;

    [Header("Config objects:")]
    [SerializeField]
    private SpawnConfig spawnerNestConfig;

    [SerializeField]
    private SpawnConfig meleeMobSpawnConfig;

    [SerializeField]
    private SpawnConfig rangedMobSpawnConfig;

    private int _mobCount = 0;

    private Spawner spawnerInstance;
    #endregion

    [Header("Effects")]
    [SerializeField]
    private GameObject bloodSplatter;

    [SerializeField]
    private AudioClip deathClip;

    #region Properties
    public static bool CanSpawnMobs
    {
        get
        {
            if (singletone == null) singletone = FindObjectOfType<AIManager>();
            return singletone._mobCount < MAX_MOB_COUNT;
        }
    }

    public static MeleeMob MeleeMob { get { return singletone.meleeMob; } }
    public static RangedMob RangedMob { get { return singletone.rangedMob; } }
    public static SpawnerNest SpawnerNest { get { return singletone.spawnerNest; } }

    public static SpawnConfig MeleeMobSpawnConfig { get { return singletone.meleeMobSpawnConfig; } }
    public static SpawnConfig RangedMobSpawnConfig { get { return singletone.rangedMobSpawnConfig; } }
    public static SpawnConfig SpawnerNestSpawnConfig { get { return singletone.spawnerNestConfig; } }

    public static GameObject BloodSplatter { get { return singletone.bloodSplatter; } }
    public static AudioClip DeathClip { get { return singletone.deathClip; } }
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        if (singletone == null) singletone = this;

        spawnerInstance = Instantiate<Spawner>(spawner);
        spawnerInstance.transform.position = Vector3.zero + Vector3.up * 2f;
    }

    #endregion

    public static void RecalculateMobCount()
    {
        singletone._mobCount = GameObject.FindObjectsOfType<Mob>().Length;
        Sacristan.Logger.Log(string.Format("Found {0} mobs", singletone._mobCount));
    }
}
