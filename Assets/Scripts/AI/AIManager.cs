using UnityEngine;

public class AIManager : MonoBehaviour
{
    private static AIManager singletone;

    #region Fields

    [SerializeField]
    private SpawnerNestSpawner spawnerNestSpawner;

    [SerializeField]
    private SpawnerNest spawnerNest;
    #endregion

    #region Properties
    public static SpawnerNest SpawnerNest { get { return singletone.spawnerNest; } }
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
