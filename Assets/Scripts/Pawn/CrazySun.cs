using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazySun : MonoBehaviour
{
	public Transform sunGraphics;
	public float twitchRange = 0.1f;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed = 0.5f;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }

    void LateUpdate()
	{
		//sunGraphics.rotation = The.gameCamera.transform.rotation;
		sunGraphics.rotation = Quaternion.LookRotation(transform.localPosition);
		sunGraphics.localPosition = Random.insideUnitSphere * twitchRange;
	}
}
