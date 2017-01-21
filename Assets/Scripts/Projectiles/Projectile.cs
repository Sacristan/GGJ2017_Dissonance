using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[Header("Physics")]
	public float speed = 10;
	public Vector3 direction;

	internal Collider coll;

	void Awake()
	{
		coll = GetComponent<Collider>();
	}

	void Update()
	{
		transform.position += direction * speed * Time.deltaTime;
	}
}
