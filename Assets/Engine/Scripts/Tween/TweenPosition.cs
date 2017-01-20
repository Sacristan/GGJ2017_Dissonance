using UnityEngine;
using System.Collections;

public class TweenPosition : Tween
{
	public Vector3 positionStart;
	public Vector3 positionEnd;
	
	private Vector3 position;
	
	public override void TweenAwake()
    {
		position = transform.position;
	}
	
	public override void Tweening()
    {
        base.Tweening();
		
		position = Vector3.Lerp(positionStart, positionEnd, tweenCurve.Evaluate(curveTime));
	}
	
	public override void ApplyTweenTransform()
    {
        base.ApplyTweenTransform();
		
		if (simulationSpace == Engine.WorldSpaceType.Global)
		{
			transform.position = position;
		}
		else // Local
		{
			if (!useRectTransform)
			{
				transform.localPosition = position;
			}
			else // This is used for GUI stuff
			{
				GetComponent<RectTransform>().anchoredPosition3D = position;
			}
		}
	}
	
	void OnDrawGizmos()
	{
		DrawPositionTweenGizmo(positionStart, positionEnd);
	}
}