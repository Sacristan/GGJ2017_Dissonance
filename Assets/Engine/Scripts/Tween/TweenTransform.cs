using UnityEngine;
using System.Collections;

public class TweenTransform : Tween
{
	public Transform transformStart;
	public Transform transformEnd;
	
	private Vector3 position;
	private Quaternion rotation;
	private Vector3 scale;
	
	public override void TweenAwake()
    {
       position = transform.position;
	   rotation = transform.rotation;
       scale = transform.localScale;
	}
	
	public override void Tweening()
    {
        base.Tweening();
		
		position = Vector3.Lerp(transformStart.position, transformEnd.position, tweenCurve.Evaluate(curveTime));
		rotation = Quaternion.Lerp(transformStart.rotation, transformEnd.rotation, tweenCurve.Evaluate(curveTime));
		scale = Vector3.Lerp(transformStart.localScale, transformEnd.localScale, tweenCurve.Evaluate(curveTime));
	}
	
	public override void ApplyTweenTransform()
    {
        base.ApplyTweenTransform();
		
		transform.position = position;
		transform.rotation = rotation;
		
		// We can only change local scale
		transform.localScale = scale;
	}
	
	void OnDrawGizmos()
	{
		DrawPositionTweenGizmo(transformStart.position, transformEnd.position);
		DrawScaleTweenGizmo(transformStart.position, transformEnd.position, transformStart.localScale, transformEnd.localScale);
	}
}
