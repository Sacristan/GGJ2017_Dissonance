using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
	public float time = 1;
	public bool useUnscaledTime;

	private float t = 0;

	void Update()
	{
		float dt = Time.deltaTime;

		if (useUnscaledTime)
		{
			dt = Time.unscaledDeltaTime;
		}

		t += dt;

		if (t > time)
		{
			Destroy(gameObject);
		}
	}
}
