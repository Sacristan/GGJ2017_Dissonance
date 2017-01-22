using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	internal float yaw;
	internal float pitch;
	internal float roll;

	private Vector3 offset = new Vector3(0, 1.5f, 0);
	internal Vector3 shakeOffset;
	private float shakeAmount;

	void Awake()
	{
		The.gameCamera = this;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (Cursor.lockState == CursorLockMode.Locked && !The.gameLogic.gameOver)
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

		if (!The.gameLogic.gameOver && !Console.isEnabled)
		{
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

		// Camera Shaking
		if (shakeAmount > 0)
		{
			shakeAmount -= Time.deltaTime;

			shakeOffset = Random.insideUnitSphere * 0.5f * shakeAmount;

			if (shakeAmount <= 0)
			{
				shakeAmount = 0;
				shakeOffset = Vector3.zero;
			}
		}
	}

	public void Shake(float amount)
	{
		shakeAmount = amount;
	}

	void LateUpdate()
	{
		transform.localPosition = offset + shakeOffset;
		transform.rotation = Quaternion.Euler(pitch, yaw, roll);
	}
}
