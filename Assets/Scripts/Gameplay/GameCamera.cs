using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	internal float yaw;
	internal float pitch;
	internal float roll;

	void Awake()
	{
		The.gameCamera = this;
	}

	void Update()
	{
		yaw += Input.GetAxis("Mouse X") * Settings.mouseSensitivity * Time.deltaTime;
		pitch -= Input.GetAxis("Mouse Y") * Settings.mouseSensitivity  * Time.deltaTime;

		// Limit pitch
		const float pitchLimit = 89;
		if (pitch > pitchLimit)
		{
			pitch = pitchLimit;
		}
		if (pitch < -pitchLimit)
		{
			pitch = -pitchLimit;
		}
	}

	void LateUpdate()
	{
		transform.rotation = Quaternion.Euler(pitch, yaw, roll);
	}
}
