using UnityEngine;
using System.Collections;

public class TweenScale : Tween
{
	public Vector3 scaleStart = new Vector3(1,1,1);
	public Vector3 scaleEnd = new Vector3(1,1,1);
	private Vector3 scale;
	
	public override void TweenAwake()
    {
        scale = transform.localScale;
	}
	
	public override void Tweening()
    {
        base.Tweening();
		
		scale = Vector3.Lerp(scaleStart, scaleEnd, tweenCurve.Evaluate(curveTime));
	}
	public override void ApplyTweenTransform()
    {
        base.ApplyTweenTransform();
		
		transform.localScale = scale;
	}
	
	void OnDrawGizmos()
	{
		DrawScaleTweenGizmo(transform.position, transform.position, scaleStart, scaleEnd);
	}
}
