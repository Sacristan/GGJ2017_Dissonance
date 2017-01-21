using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Pawn, IDamageable
{
	private Vector2 inputVec;

	#region MonoBehaviour
	public override void Awake()
	{
		base.Awake();

		The.player = this;
	}

	public override void Update()
	{
		base.Update();

		// Input Vector
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

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}

		if (inputVec.magnitude > 1)
		{
			inputVec = inputVec.normalized;
		}

		Quaternion camRot = Quaternion.Euler(0, The.gameCamera.yaw, 0);
		velocity += camRot * new Vector3(inputVec.x, 0, inputVec.y) * maxSpeed * Time.deltaTime;
	}
	public override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	#endregion

	void Jump()
	{
		if (grounded)
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
