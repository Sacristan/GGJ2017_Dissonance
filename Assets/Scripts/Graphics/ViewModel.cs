using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModel : MonoBehaviour
{
	private float yaw;
	private float pitch;

	void Start()
	{
		yaw = The.gameCamera.transform.eulerAngles.y;
		pitch = The.gameCamera.transform.eulerAngles.x;
	}

	void LateUpdate()
	{
		const float stiffness = 25;
		yaw = Mathf.LerpAngle(yaw, The.gameCamera.yaw, stiffness * Time.deltaTime);
		pitch = Mathf.LerpAngle(pitch, The.gameCamera.pitch, stiffness * Time.deltaTime);

		transform.rotation = Quaternion.Euler(pitch, yaw, The.gameCamera.roll);
	}
}
