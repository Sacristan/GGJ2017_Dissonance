using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazySun : MonoBehaviour
{
	public Transform sunGraphics;
	public float twitchRange = 0.1f;

	void LateUpdate()
	{
		//sunGraphics.rotation = The.gameCamera.transform.rotation;
		sunGraphics.rotation = Quaternion.LookRotation(transform.localPosition);
		sunGraphics.localPosition = Random.insideUnitSphere * twitchRange;
	}
}
