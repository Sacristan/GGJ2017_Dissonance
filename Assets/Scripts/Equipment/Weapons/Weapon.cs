﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public enum AttackType
	{
		Melee,
		Hitscan,
		Projectile,
		Special,
	}
	public enum ArmAnimType
	{
		Handgun = 0,
		Rocket = 1,
		Special = 2,
	}

	public Item.Type ammoType;
	public Pawn owner;

	[Header("Attacking")]
	public AttackType attackType;
	public Projectile projectile;
	public float attackCooldown = 0.2f;
	public AudioClip attackSound;

	[Header("Effects")]
	public Transform muzzleFlashPrefab;

	[Header("Other")]
	public Transform barrel;

	[Header("Animation")]
	public ArmAnimType armAnimType;

	public void Attack()
	{
		owner.attackCooldown = attackCooldown;

		// Effects/Sounds
		if (attackSound)
		{
			var soundO = Sound.PlayClipAt(attackSound, transform.position);
			soundO.transform.SetParent(transform);
		}

		// Attack behaviour
		switch (attackType)
		{
			case AttackType.Melee:
				// Maybe next jam?
				break;
			case AttackType.Hitscan:

				break;
			case AttackType.Projectile:
				var projO = Instantiate(projectile, barrel.position, barrel.rotation);
				projO.direction = owner.aimDirection;
				Physics.IgnoreCollision(projO.GetComponent<Collider>(), owner.body);
				break;
			case AttackType.Special:
				// Uh..
				break;
			default:
				break;
		}
	}

	void UseAmmo(int amount)
	{

	}
}
