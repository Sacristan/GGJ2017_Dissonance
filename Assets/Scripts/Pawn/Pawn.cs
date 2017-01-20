using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
	public enum Gender
	{
		Other, male, Female
	};
	public enum Age
	{
		Child, Teen, Adult, Elder
	};
	public enum Race
	{
		Other, Human, Monster, Boss
	};
	public enum State
	{
		Neutral, Friendly, Hostile
	};
	public enum Emotion
	{
		Neutral, Happy, Sad, Angry, Depressed, Scared
	};
	public enum Mind
	{
		None, Player, CPU
	};
	public enum PhysicsState
	{
		None, Ground, Fall, Water, Space
	};
	public enum PhysicsType
	{
		None, CC, Rigidbody
	};

	public enum DamageType
	{
		None,
		Generic,
		Blunt,
		Spikes,
		Blade,
		Projectile,
		Explosion,
		Fall,
		Suffication,
		Fire,
		Lava,
	};

	[Header("Main")]
	public Gender gender;
	public Age age;

	[Header("Physics")]
	public float maxSpeed = 5;
	internal CharacterController body;

	public virtual void Awake()
	{
		body = GetComponent<CharacterController>();
	}
	public virtual void Update()
	{

	}
	public virtual void FixedUpdate()
	{

	}
}
