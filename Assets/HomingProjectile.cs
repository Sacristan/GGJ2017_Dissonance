using UnityEngine;

public class HomingProjectile : Projectile
{
    #region Fields

    [Header("Homing Fields:")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float followTime = 3f;

    private float spawnTime;
    private bool directionCalculated = false;
    #endregion

    #region MonoBehaviour
    public override void Awake()
    {
        base.Awake();
        spawnTime = Time.realtimeSinceStartup;
        Destroy(gameObject,followTime * 3f);
    }

    public override void Update()
    {
        if (Time.time - spawnTime < followTime) FollowTarget();
        else base.Update();
    }
    #endregion

    #region Private methods
    private void FollowTarget()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        pos.y = transform.position.y;
        transform.position = pos;
        CalculateDirection();
    }

    private void CalculateDirection()
    {
        direction = target.position - transform.position;
        direction.y = 0;
        direction.Normalize();
    }
    #endregion
}
