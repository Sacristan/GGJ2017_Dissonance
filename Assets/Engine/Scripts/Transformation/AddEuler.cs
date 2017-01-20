using UnityEngine;
using System.Collections;

public class AddEuler : MonoBehaviour
{
	public enum UpdateType
	{
		Update,
		LateUpdate,
		FixedUpdate,
	}

	public bool local;
	public Vector3 rotation;

	public UpdateType updateType = UpdateType.LateUpdate;
	public bool useUnscaledTime;

	private float dt;

	void Update()
	{
		if (updateType == UpdateType.Update)
		{
			RotateObject();
		}
	}

	void LateUpdate()
	{
		if (updateType == UpdateType.LateUpdate)
		{
			RotateObject();
		}
	}

	void FixedUpdate()
	{
		if (updateType == UpdateType.FixedUpdate)
		{
			RotateObject();
		}
	}

	void RotateObject()
	{
		if (local)
		{
			LocalRotation();
		}
		else
		{
			GlobalRotation();
		}

		if (useUnscaledTime)
		{
			dt = Time.unscaledDeltaTime;
		}
		else
		{
			dt = Time.deltaTime;
		}
	}

	void LocalRotation()
	{
		Vector3 newRot = transform.localEulerAngles;
		newRot += rotation * dt;
		transform.localEulerAngles = newRot;
	}
	void GlobalRotation()
	{
		Vector3 newRot = transform.eulerAngles;
		newRot += rotation * dt;
		transform.eulerAngles = newRot;
	}
}
