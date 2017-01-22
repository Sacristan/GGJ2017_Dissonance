using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModel : MonoBehaviour
{
	public Vector3 offset = new Vector3(0, -0.2f, 0);
	public Transform rightHand;

	private float yaw;
	private float pitch;
	private float roll;
	private Vector3 position;

	private Quaternion rightHandRotation;

	// Bobbing
	internal float bobAmount;
	private Vector3 bobOffset;
	private float bob;

	void Awake()
	{
		The.viewModel = this;
	}
	void Start()
	{
		yaw = The.gameCamera.transform.eulerAngles.y;
		pitch = The.gameCamera.transform.eulerAngles.x;
		position = The.gameCamera.transform.position;
	}

	void Update()
	{
		Vector3 vel2D = new Vector3(The.player.velocity.x, 0, The.player.velocity.z);

		const float rotationStiffness = 25;
		yaw = Mathf.LerpAngle(yaw, The.gameCamera.yaw, rotationStiffness * Time.deltaTime);
		pitch = Mathf.LerpAngle(pitch, The.gameCamera.pitch, rotationStiffness * Time.deltaTime);
		roll = Mathf.LerpAngle(roll, -The.gameCamera.transform.InverseTransformDirection(vel2D).x * 0.5f, rotationStiffness * Time.deltaTime);

		rightHandRotation = Quaternion.Euler(0, -transform.localEulerAngles.y, -transform.localEulerAngles.x);

		float bobAmountTarget = 0;
		if (The.player.body.isGrounded)
		{
			bobAmountTarget = vel2D.magnitude;
		}
		else
		{
			bobAmountTarget = 0;
		}
		bobAmount = Mathf.Lerp(bobAmount, bobAmountTarget, 10 * Time.deltaTime);

		bob += bobAmount * 20 * Time.deltaTime;

		bobOffset = The.gameCamera.transform.InverseTransformDirection(new Vector3(Mathf.Cos(Mathf.Deg2Rad * bob) * 1.5f, Mathf.Sin(Mathf.Deg2Rad * bob * 2), 0)) * bobAmount * 0.001f;

		const float positionStiffness = 20;
		float jumpVel = 0;
		if (!The.player.body.isGrounded)
		{
			jumpVel = Mathf.Max(-0.2f, Mathf.Min(0.2f, -The.player.velocity.y * 0.01f))	;
		}
		position = Vector3.Lerp(position, offset + new Vector3(0, jumpVel, 0), positionStiffness * Time.deltaTime) + bobOffset - The.gameCamera.transform.rotation * The.gameCamera.shakeOffset * 0.1f;
	}

	void LateUpdate()
	{
		transform.localPosition = position;
		transform.rotation = Quaternion.Euler(pitch, yaw, roll);

		rightHand.rotation *= rightHandRotation;
	}
}
