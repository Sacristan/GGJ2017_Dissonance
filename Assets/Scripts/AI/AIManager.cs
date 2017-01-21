using UnityEngine;

public class AIManager : MonoBehaviour
{
    private static AIManager singletone;

    #region Fields

    [SerializeField]
    private SpawnerNestSpawner spawnerNestSpawner;

    [SerializeField]
    private SpawnerNestBehaviour spawnerNestSpawn;
    #endregion

    #region Properties
    public static SpawnerNestBehaviour SpawnerNestSpawn { get { return singletone.spawnerNestSpawn; } }
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        if (singletone == null) singletone = this;
        else Destroy(singletone);

        SpawnerNestSpawner instance = Instantiate<SpawnerNestSpawner>(spawnerNestSpawner);
        instance.transform.position = Vector3.zero;
    }
    #endregion

}
