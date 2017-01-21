using UnityEngine;
using Sacristan.Messaging;
using System;

public class Mob : MonoBehaviour, IDamageable
{
    public class KeyNotAssignedException: Exception
    {
        public override string Message { get { return "Message Key not assigned my Mob child"; } }
    }

    public const string UNASSIGNED_KEY = null;

    protected string receivedDamageKey = UNASSIGNED_KEY;
    protected string diedKey = UNASSIGNED_KEY;

    #region MonoBehaviour
    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        if (receivedDamageKey == UNASSIGNED_KEY) throw new KeyNotAssignedException();
        if (diedKey == UNASSIGNED_KEY) throw new KeyNotAssignedException();
    }

    public virtual void Update()
    {

    }

    public virtual void LateUpdate()
    {

    }

    #endregion

    public virtual void ApplyDamage(float damage)
    {
        Sacristan.Logger.Log(string.Format("{0} received {1} damage", gameObject.name, damage));
        Messenger<float>.Broadcast(receivedDamageKey, damage);
    }

    public virtual void Die()
    {
        Sacristan.Logger.Log(string.Format("{0} Died", gameObject.name));
        Messenger.Broadcast(diedKey);
    }
}