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
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (Cursor.lockState == CursorLockMode.Locked)
		{
			yaw += Input.GetAxis("Mouse X") * Settings.mouseSensitivity * Time.deltaTime;
			pitch -= Input.GetAxis("Mouse Y") * Settings.mouseSensitivity * Time.deltaTime;
		}
			
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

		// Limiting Cursor
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}

	void LateUpdate()
	{
		transform.rotation = Quaternion.Euler(pitch, yaw, roll);
	}
}
