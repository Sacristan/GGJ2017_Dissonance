using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour
{
	internal AudioSource source;

	internal float volume = 1;
	private float fade = 1;

	public static GameMusic current;

	void Start()
	{
		SetVolume(volume);
		current = this;
	}

	public void SetVolume(float amount)
	{
		volume = amount;
	}

	void Update()
	{
		source.volume = volume * fade * Settings.musicVolume;
	}

	public void StartFade(float duration)
	{
		fade = 0;
		StartCoroutine(Fade(1, duration));
	}
	public void FadeIn(float duration)
	{
		StartCoroutine(Fade(1, duration));
	}
	public void FadeOut(float duration)
	{
		StartCoroutine(Fade(0, duration));
	}

	public void Stop()
	{
		Destroy(gameObject);
	}

	IEnumerator Fade(float goal, float duration)
	{
		float t = 0;
		float origFade = fade;

		while (true)
		{
			t += Time.deltaTime/duration;

			fade = Mathf.Lerp(origFade, goal, t);

			if (t >= 1)
			{
				if (goal == 0)
				{
					Destroy(gameObject);
				}

				break;
			}

			yield return new WaitForEndOfFrame();
		}
	}
}
