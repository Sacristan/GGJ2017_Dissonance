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

    protected CharacterController characterController;

    #region MonoBehaviour
    public override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
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

    /*public virtual void OnControllerColliderHit(ControllerColliderHit hit)
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
    }*/
    #endregion

    /*public virtual void ApplyDamage(float damage)
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
    }*/

    public override void ApplyDamage(float damage)
    {
        //ScoreManager.singletone.AddScore(5);
        Sacristan.Logger.Log(string.Format("{0} received {1} damage at {2}", gameObject.name, damage, Time.realtimeSinceStartup));
        Messenger<float>.Broadcast(Messages.ReceivedDamageNestMob, damage);

        health = Mathf.Clamp(health - damage, 0, this.maxHealth); // clamp to ensure correct UI content
        if (health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        //ScoreManager.singletone.AddScore(20);
        Messenger.Broadcast(Messages.DiedNestMob);
        GameObject bloodSplatter = Instantiate(AIManager.BloodSplatter, transform.position, Quaternion.identity);
        GameObject audioEffect = new GameObject("DeathAudio");
        audioEffect.transform.position = transform.position;

        AudioSource audioSource = audioEffect.AddComponent<AudioSource>();

        audioSource.PlayOneShot(AIManager.DeathClip, 1f);

        Destroy(bloodSplatter, 2f);
        Destroy(gameObject);
    }

}