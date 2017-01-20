using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Pawn
{

	private Vector2 inputVec;

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
			velocity.y = 20;
		}

		if (inputVec.magnitude > 1)
		{
			inputVec = inputVec.normalized;
		}

		velocity += The.gameCamera.transform.rotation * new Vector3(inputVec.x, 0, inputVec.y) * maxSpeed * Time.deltaTime;
	}
	public override void FixedUpdate()
	{
		base.FixedUpdate();
	}
}
