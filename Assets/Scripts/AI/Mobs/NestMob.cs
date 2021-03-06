﻿using UnityEngine;

public class NestMob : Mob
{
    [Header("Movement")]
    [SerializeField]
    private float damageToPlayer = 20f;
    [SerializeField]
    private float closeEnoughDistance = 5f;
    [SerializeField]
    private float speed = 5f;

    private Player _player;

    public override void Awake()
    {
        base.Awake();
        receivedDamageKey = Messages.ReceivedDamageNestMob;
        diedKey = Messages.DiedNestMob;
    }

    public override void Start()
    {
        base.Start();
        _player = The.player;
    }

    public override void Update()
    {
        Vector3 target = _player.transform.position;
        Vector3 currentPos = transform.position;

        Vector3 dir = target - currentPos;
        dir.Normalize();

        if (body.isGrounded)
        {
            Vector3 newVel = dir * speed * Time.deltaTime;
            velocity.x = newVel.x;
            velocity.z = newVel.z;
        }

        transform.rotation = Quaternion.LookRotation(dir);

        float distanceFromTarget = Vector3.Distance(target, currentPos);

        if (distanceFromTarget <= closeEnoughDistance)
        {
            _player.ApplyDamage(damageToPlayer);
            Die();
            Destroy(this);
        }

        base.Update();

        ////base.Update();

        //Vector3 target = _player.transform.position;
        //Vector3 currentPos = transform.position;

        //float distanceFromTarget = Vector3.Distance(target, currentPos);

        //if (distanceFromTarget <= closeEnoughDistance)
        //{
        //    _player.ApplyDamage(damageToPlayer);
        //    Die();
        //    Destroy(this);
        //}
        //else
        //{
        //    Vector3 dir = target - currentPos;
        //    dir.y = 0;
        //    dir.Normalize();

        //    //characterController.Move(dir * speed * Time.deltaTime);
        //}

    }

}
