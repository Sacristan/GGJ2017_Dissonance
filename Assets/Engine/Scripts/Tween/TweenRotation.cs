using UnityEngine;
using System.Collections;

public class TweenRotation : Tween
{
	public Vector3 rotationStart;
	public Vector3 rotationEnd;
	public bool tweenEulers;
	
	private Quaternion rotation;
	private Vector3 eulers;
	
	public override void TweenAwake()
    {
        rotation = transform.rotation;
		if (tweenEulers)
		{
			if (simulationSpace == Engine.WorldSpaceType.Global)
			{
				eulers = transform.eulerAngles;
			}
			else // Local
			{
				eulers = transform.localEulerAngles;
			}
		}
	}
	
	public override void Tweening()
    {
        base.Tweening();
		
		if (tweenEulers)
		{
			eulers = Vector3.Lerp(rotationStart, rotationEnd, tweenCurve.Evaluate(curveTime));
			rotation = Quaternion.Euler(eulers);
		}
		else // Use smooth Quaternion tweening, instead
		{
			rotation = Quaternion.Lerp(Quaternion.Euler(rotationStart), Quaternion.Euler(rotationEnd), tweenCurve.Evaluate(curveTime));
		}
	}
	
	public override void ApplyTweenTransform()
    {
        base.ApplyTweenTransform();
		
		if (simulationSpace == Engine.WorldSpaceType.Global)
		{
			transform.rotation = rotation;
		}
		else // Local
		{
			transform.localRotation = rotation;
		}
	}
	
	void OnDrawGizmos()
	{
		
	}
}
