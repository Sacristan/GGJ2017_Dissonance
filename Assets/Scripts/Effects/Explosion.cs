using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	public float radius = 5;
	public float damageMultiplier = 5;
	public AudioClip soundEffect;

	void Start()
	{
		// Splash damage
		Collider[] surroundingColliders = Physics.OverlapSphere(transform.position, radius);

		foreach (var item in surroundingColliders)
		{
			float distance = Vector3.Distance(item.transform.position, transform.position);
			float damageAmount = (radius - distance) * damageMultiplier;

			// Dealing damage to a pawn
			var pawnHit = item.GetComponent<Pawn>();
			if (pawnHit)
			{
				pawnHit.ApplyDamage(damageAmount);
				pawnHit.velocity += (pawnHit.transform.position - transform.position).normalized * damageAmount;
				break;
			}
		}

		// Camera shake
		float cameraDistance = Vector3.Distance(The.gameCamera.transform.position, transform.position);

		if (cameraDistance < radius * 2)
		{
			The.gameCamera.Shake(((radius * 2) - cameraDistance) * 0.1f);
		}

		// Sound Effect
		if (soundEffect)
		{
			var soundO = Sound.PlayClipAt(soundEffect, transform.position);
			soundO.source.minDistance = 20;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
