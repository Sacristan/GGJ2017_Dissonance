using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacristan.Messaging;

public class Player : Pawn, IDamageable
{
	[Header("Animation")]
	public Animator armAnimator;
	public Transform leftWeaponAnchor;
	public Transform rightWeaponAnchor;

	[Header("Equipment")]
	public List<Weapon> weapons = new List<Weapon>();
	public List<Ability> abilities = new List<Ability>();
	public float mana;
	public int shotgunAmmo;
	public int rocketAmmo;
	public int plasmaAmmo;

	internal int currentAmmo = 10;
	internal Weapon currentWeapon;

	// Other stuff
	private Vector2 inputVec;

	#region MonoBehaviour
	public override void Awake()
	{
		base.Awake();

		The.player = this;
		SwitchWeapon(0);
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
		if (Input.GetMouseButton(0))
		{
			Attack();
		}

		// Switching weapons
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SwitchWeapon(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwitchWeapon(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SwitchWeapon(2);
		}
		
		// Apply movement input to velocity
		Quaternion camRot = Quaternion.Euler(0, The.gameCamera.yaw, 0);
		velocity += camRot * new Vector3(inputVec.x, 0, inputVec.y) * maxSpeed * Time.deltaTime;
	}

	public override void Jump()
	{
		if (body.isGrounded)
		{
			velocity.y = 10;
			if (jumpSounds.Length > 0)
			{
				Sound.PlayClip(jumpSounds);
			}
		}
	}

	#region CombatLogic
	public override void Attack()
	{
		base.Attack();

		if (attackCooldown <= 0)
		{
			armAnimator.SetTrigger("Attack");
			currentWeapon.Attack();
			The.gameUI.UpdateAmmoGraphics();
		}
	}
	public void AddWeapon()
	{

	}
	public void SwitchWeapon(int targetWeapon)
	{
		if (targetWeapon >= weapons.Count)
		{
			return;
		}

		if (currentWeapon)
		{
			Destroy(currentWeapon.gameObject);
		}

		armAnimator.SetTrigger("Draw");
		attackCooldown = 1;

		currentWeapon = Instantiate(weapons[targetWeapon]);
		currentWeapon.owner = this;

		currentWeapon.transform.SetParent(rightWeaponAnchor);
		currentWeapon.transform.localPosition = Vector3.zero;
		currentWeapon.transform.localRotation = Quaternion.identity;
		currentWeapon.transform.localScale = Vector3.one;
	}
	public void AddAbility()
	{

	}
	public void AddAmmo()
	{

	}
	#endregion

	#region  DamageLogic
	public void ApplyDamage(float damage)
	{
		Sacristan.Logger.Log (string.Format("{0} received {1} damage", gameObject.name, damage));
        Messenger<float>.Broadcast(Messages.ReceivedDamagePlayer, damage);
    }

    public void Die()
    {
		The.gameLogic.GameOver();
        Messenger.Broadcast(Messages.DiedPlayer);
    }
	#endregion
}
