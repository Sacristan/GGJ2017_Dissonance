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

	public void Attack()
	{

	}

	void UseAmmo(int amount)
	{

	}
}
