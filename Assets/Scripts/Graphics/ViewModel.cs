using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModel : MonoBehaviour
{
	public Vector3 offset = new Vector3(0, -0.2f, 0);

	private float yaw;
	private float pitch;
	private Vector3 position;

	// Bobbing
	internal float bobAmount;
	private Vector3 bobOffset;

	void Start()
	{
		yaw = The.gameCamera.transform.eulerAngles.y;
		pitch = The.gameCamera.transform.eulerAngles.x;
		position = The.gameCamera.transform.position;
	}

	void Update()
	{
		const float rotationStiffness = 25;
		yaw = Mathf.LerpAngle(yaw, The.gameCamera.yaw, rotationStiffness * Time.deltaTime);
		pitch = Mathf.LerpAngle(pitch, The.gameCamera.pitch, rotationStiffness * Time.deltaTime);

		const float positionStiffness = 75;
		position = Vector3.Lerp(position, The.gameCamera.transform.position + The.gameCamera.transform.rotation * offset, positionStiffness * Time.deltaTime) + bobOffset;
	}
	void LateUpdate()
	{
		transform.position = position;
		transform.rotation = Quaternion.Euler(pitch, yaw, The.gameCamera.roll);	
	}
}
