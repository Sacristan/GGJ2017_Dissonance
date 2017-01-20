using UnityEngine;
using System.Collections;

public class AddEulerFps : MonoBehaviour
{
	public bool local;
	public Vector3 rotation;
	public bool useUnscaledTime;
	public int fps = 60;

	private float dt;
	private float lockTime = 1;

	void LateUpdate()
	{
		if (fps <= 0)
		{
			fps = 1;
		}

		if (useUnscaledTime)
		{
			dt = Time.unscaledDeltaTime;
		}
		else
		{
			dt = Time.deltaTime;
		}

		lockTime -= dt * fps;

		if (lockTime <= 0)
		{
			lockTime = 1;

			if (local)
			{
				LocalRotation();
			}
			else
			{
				GlobalRotation();
			}
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
