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
	public float health = 100;
	public float maxHealth = 100;

	[Header("Physics")]
	public float maxSpeed = 5;
	public float velocityDamping = 5;
	internal CharacterController body;
	internal Vector3 velocity;

	[Header("Audio")]
	public AudioClip[] painSounds;
	public AudioClip[] deathSounds;
	public AudioClip[] jumpSounds;

	// Some private stuff
	internal float attackCooldown;

	public virtual void Awake()
	{
		body = GetComponent<CharacterController>();
	}
	public virtual void Update()
	{
		// Damping
		velocity.x -= velocity.x * velocityDamping * Time.deltaTime;
		velocity.z -= velocity.z * velocityDamping * Time.deltaTime;

		// Move the character
		body.Move(velocity * Time.deltaTime);
		if (body.isGrounded)
		{
			velocity.y = -5;
		}
		else
		{
			velocity += Physics.gravity * Time.deltaTime;
		}

		// Combat
		if (attackCooldown > 0)
		{
			attackCooldown -= Time.deltaTime;
		}
	}

	public virtual void Jump()
	{
	}
	public virtual void Attack()
	{
	}

	void LateUpdate()
	{
		Vector3 newRot = transform.eulerAngles;
		newRot.y = The.gameCamera.transform.eulerAngles.y;
		transform.eulerAngles = newRot;
	}
}
