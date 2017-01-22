using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Transform target;

    [SerializeField]
    private GameObject spawn;
    
    [SerializeField]
    private float shotAngle = 45f;
    #endregion

    private float lastShotTime; //Remove this as soona as functionality added

    #region MonoBehaviour
    void Update()
    {
        if (Time.realtimeSinceStartup-lastShotTime > 3f)
        {
            lastShotTime = Time.realtimeSinceStartup;

            GameObject gameObject = Instantiate(spawn, transform.position, Quaternion.identity);
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.velocity = BallisticVel(target, shotAngle);
            Destroy(gameObject, 5);
        }
    }
    #endregion

    #region Private Methods
    private Vector3 BallisticVel(Transform target, float angle)
    {
        Vector3 dir = target.position - transform.position;
        float h = dir.y;
        dir.y = 0; 
        float dist = dir.magnitude;
        float a = angle * Mathf.Deg2Rad; 
        dir.y = dist * Mathf.Tan(a);
        dist += h / Mathf.Tan(a); 
                                   
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }

    #endregion

}
