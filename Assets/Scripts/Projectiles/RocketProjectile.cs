using UnityEngine;

public class RocketProjectile : Projectile
{
    private Rigidbody rigidBody;
    private Quaternion initialQuaternion;

    public override void Awake()
    {
        base.Awake();

        rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 10f);
    }

    public void Start()
    {
        rigidBody.velocity = direction * speed;
        initialQuaternion = rigidBody.rotation;
    }

    public override void Update()
    {
        //base.Update();
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.rotation = initialQuaternion;
    }
}
