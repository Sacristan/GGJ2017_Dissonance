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
	public float velocityDamping = 5;
	internal CharacterController body;
	internal Vector3 velocity;
	internal bool grounded;

	public virtual void Awake()
	{
		body = GetComponent<CharacterController>();
	}
	public virtual void Update()
	{
		// Check for ground
		/*Collider groundColliders = Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), -Vector3.up, 0.5f, );
		if (groundColliders.Length > 0)
		{
			foreach (var item in groundColliders)
			{
				if (item.gameObject != gameObject)
				{
					grounded = true;
				}
				else
				{
					grounded = false;
				}
			}
		}
		else
		{
			grounded = false;
		}*/

		// Damping
		velocity.x -= velocity.x * velocityDamping * Time.deltaTime;
		velocity.z -= velocity.z * velocityDamping * Time.deltaTime;

		// Move the character
		velocity += Physics.gravity * Time.deltaTime;
		body.Move(velocity * Time.deltaTime);
	}
	public virtual void FixedUpdate()
	{

	}

	void LateUpdate()
	{
		Vector3 newRot = transform.eulerAngles;
		newRot.y = The.gameCamera.transform.eulerAngles.y;
		transform.eulerAngles = newRot;
	}
}
