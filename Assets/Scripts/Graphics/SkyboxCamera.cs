using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCamera : MonoBehaviour
{
	private Camera cam;

	void Awake()
	{
		cam = GetComponent<Camera>();
	}

	void LateUpdate()
	{
		cam.fieldOfView = Camera.main.fieldOfView;
		transform.rotation = Camera.main.transform.rotation;	
	}
}
