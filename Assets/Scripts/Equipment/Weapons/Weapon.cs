using System.Collections;
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

	public Item.Type ammoType;
	public Pawn owner;

	[Header("Attacking")]
	public Projectile projectile;
	public float attackCooldown = 0.2f;
	public AudioClip attackSound;

	public void Attack()
	{
		owner.attackCooldown = attackCooldown;
	}

	void UseAmmo(int amount)
	{

	}
}
