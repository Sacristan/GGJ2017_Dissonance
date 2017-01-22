using UnityEngine;
using Sacristan.Messaging;
using System;

public class Mob : Pawn, IDamageable
{
    public class KeyNotAssignedException : Exception
    {
        public override string Message { get { return "Message Key not assigned my Mob child"; } }
    }

    public const string UNASSIGNED_KEY = null;

    protected string receivedDamageKey = UNASSIGNED_KEY;
    protected string diedKey = UNASSIGNED_KEY;

    #region MonoBehaviour
    public override void Awake()
    {
    }

    public void OnEnable()
    {
        Messenger.AddListener(Messages.SunInitialised, HandleSunKickedIn);
    }

    public void OnDisable()
    {
        Messenger.RemoveListener(Messages.SunInitialised, HandleSunKickedIn);
    }

    public virtual void Start()
    {
        if (receivedDamageKey == UNASSIGNED_KEY) throw new KeyNotAssignedException();
        if (diedKey == UNASSIGNED_KEY) throw new KeyNotAssignedException();
    }

    public override void Update()
    {

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Projectile projectile = hit.gameObject.GetComponent<Projectile>();

        if(projectile != null)
        {
            switch (projectile.PossibleTargets)
            {
                case Projectile.Targets.Mob:
                case Projectile.Targets.Both:
                    ApplyDamage(projectile.Damage);
                    break;
            }
        }
    }
    #endregion

    public virtual void ApplyDamage(float damage)
    {
        Sacristan.Logger.Log(string.Format("{0} received {1} damage", gameObject.name, damage));
        Messenger<float>.Broadcast(receivedDamageKey, damage);

        health -= damage;
        if (health <= 0) Die();
    }

    public virtual void Die()
    {
        Sacristan.Logger.Log(string.Format("{0} Died", gameObject.name));
        Messenger.Broadcast(diedKey);
        ExecuteDeath();
    }

    private void HandleSunKickedIn()
    {
        ExecuteDeath();
    }

    private void ExecuteDeath()
    {
        //Big explosion
        //Sound
        throw new NotImplementedException();
    }
}