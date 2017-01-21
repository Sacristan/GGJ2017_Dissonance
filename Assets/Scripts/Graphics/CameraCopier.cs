using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCopier : MonoBehaviour
{
	public Camera referenceCamera;
	internal Camera cam;

	void Awake()
	{
		cam = GetComponent<Camera>();
	}

	void LateUpdate()
	{
		cam.fieldOfView = referenceCamera.fieldOfView;
	}
}
