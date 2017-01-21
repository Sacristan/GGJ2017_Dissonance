using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Pawn, IDamageable
{
	[Header("Animation")]
	public Animator armAnimator;
	public Transform leftWeaponAnchor;
	public Transform rightWeaponAnchor;

	[Header("Equipment")]
	public List<Weapon> weapon = new List<Weapon>();
	public List<Ability> ability = new List<Ability>();
	public float mana;
	public int shotgunAmmo;
	public int rocketAmmo;
	public int plasmaAmmo;

	internal int currentAmmo = 10;

	// Other stuff
	private Vector2 inputVec;

	#region MonoBehaviour
	public override void Awake()
	{
		base.Awake();

		The.player = this;
	}

	public override void Update()
	{
		PlayerControls();
		base.Update();
	}

	#endregion

	void PlayerControls()
	{
		// Movement
		if (Input.GetKey(KeyCode.A))
		{
			inputVec.x = -1;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			inputVec.x = 1;
		}
		else
		{
			inputVec.x = 0;
		}

		if (Input.GetKey(KeyCode.S))
		{
			inputVec.y = -1;
		}
		else if (Input.GetKey(KeyCode.W))
		{
			inputVec.y = 1;
		}
		else
		{
			inputVec.y = 0;
		}

		if (inputVec.magnitude > 1)
		{
			inputVec = inputVec.normalized;
		}
		
		// Jumping
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}

		// Combat
		if (Input.GetMouseButtonDown(0))
		{
			Attack();
		}
		
		// Apply movement input to velocity
		Quaternion camRot = Quaternion.Euler(0, The.gameCamera.yaw, 0);
		velocity += camRot * new Vector3(inputVec.x, 0, inputVec.y) * maxSpeed * Time.deltaTime;
	}

	public override void Attack()
	{
		base.Attack();


	}

	public void AddWeapon()
	{

	}
	public void AddAbility()
	{

	}
	public void AddAmmo()
	{

	}

	void Jump()
	{
		if (body.isGrounded)
		{
			velocity.y = 10;
		}
	}

	#region DamageLogic
	public void ApplyDamage(float damage)
	{
		Sacristan.Logger.Log (string.Format("Received {0} damage", damage));
	}
	#endregion
}
