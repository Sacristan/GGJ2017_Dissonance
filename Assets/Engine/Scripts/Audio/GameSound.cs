using UnityEngine;
using System.Collections;

public class GameSound : MonoBehaviour
{
	public bool isAmbient;

	internal AudioSource source;
	public float distanceVolume = 1;

	[Range(0, 1)]
	public float volume = 1;

	void Start()
	{
		if (source == null)
		{
			source = GetComponent<AudioSource>();
		}

		if (!source)
		{
			enabled = false;
		}

		StartCoroutine(DestructionTimer());
	}

	void Update()
	{
		if (isAmbient)
		{
			float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
			distanceVolume = 1 - (dist - source.minDistance) / (source.maxDistance - source.minDistance);
			distanceVolume = Mathf.Clamp01(distanceVolume);
			//distanceVolume = 1 - Vector3.Distance(Camera.main.transform.position, transform.position) / source.maxDistance;
		}

		source.volume = volume * distanceVolume * Settings.soundVolume;
	}

	IEnumerator DestructionTimer()
	{
		float t = 0;

		while (true)
		{
			if (!source.clip)
			{
				break;
			}

			if (!source.loop)
			{
				t += Time.unscaledDeltaTime;

				if (t > source.clip.length)
				{
					break;
				}
			}

			yield return new WaitForEndOfFrame();
		}

		Destroy(gameObject);
	}
}
