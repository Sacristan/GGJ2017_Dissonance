using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetTransform : MonoBehaviour
{
	public bool executeInEditMode;

	[Header("Position")]
	public bool overridePosition;
	public bool useLocalPosition;
	public Vector3 position;

	[Header("Rotation")]
	public bool overrideRotation;
	public bool useLocalRotation;
	public Vector3 rotation;

	[Header("Scale")]
	public bool overrideScale;
	public Vector3 scale = Vector3.one;

	void LateUpdate()
	{
		if (!executeInEditMode && !Application.isPlaying)
			return;

		// Position
		if (overridePosition)
		{
			if (useLocalPosition)
			{
				transform.localPosition = position;
			}
			else
			{
				transform.position = position;
			}
		}
		// Rotation
		if (overrideRotation)
		{
			if (useLocalRotation)
			{
				transform.localEulerAngles = rotation;
			}
			else
			{
				transform.eulerAngles = rotation;
			}
		}

		// Scale
		if (overrideScale)
		{
			transform.localScale = scale;
		}
	}
}
