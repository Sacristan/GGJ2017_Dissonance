using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazySun : MonoBehaviour
{
	public Transform sunGraphics;
	public float twitchRange = 0.1f;
	public AudioSource screamSource;

    //[SerializeField]
    //private Transform target;

    [SerializeField]
    private float risingDuration = 3;

    [SerializeField]
    private AnimationCurve risingCurve;

	private Vector3 offset;

	private void Awake()
    {
		StartCoroutine(RiseSequence());
	}

	IEnumerator RiseSequence()
	{
		float t = 0;


		while(true)
		{
			t += Time.deltaTime / risingDuration;

			offset = Vector3.Lerp(new Vector3(0, -3, 0), Vector3.zero, risingCurve.Evaluate(t));
			screamSource.volume = risingCurve.Evaluate(t);

			if (t >= 1)
			{
				break;
			}

			yield return null;
		}

		//yield return new WaitForSeconds(1);

		t = 0;
		while (true)
		{
			t += Time.deltaTime;

			screamSource.volume = 1-risingCurve.Evaluate(t);

			if (t >= 1)
			{
				break;
			}

			yield return null;
		}
	}

    void LateUpdate()
	{
		//sunGraphics.rotation = The.gameCamera.transform.rotation;
		sunGraphics.rotation = Quaternion.LookRotation(transform.localPosition);
		sunGraphics.localPosition = Random.insideUnitSphere * twitchRange + offset;
	}
}
