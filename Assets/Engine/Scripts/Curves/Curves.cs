using UnityEngine;

public static class Curves
{
	public static AnimationCurve CreateStraightCurve()
	{
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 1));
		curve.AddKey(new Keyframe(1, 1));
		return curve;
	}

	public static AnimationCurve CreateLinearCurve()
	{
		float tan45 = Mathf.Tan(Mathf.Deg2Rad * 45);

		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, tan45, tan45));
		curve.AddKey(new Keyframe(1, 1, tan45, tan45));
		return curve;
	}

	public static AnimationCurve CreateSmoothCurve()
	{
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey(new Keyframe(0, 0, 0, 0));
		curve.AddKey(new Keyframe(1, 1, 0, 0));
		return curve;
	}
}
