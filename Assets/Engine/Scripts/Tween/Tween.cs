using UnityEngine;
using System.Collections;

public class Tween : MonoBehaviour
{
	// Some enums
	public enum TweenPlayType
	{
		Once,
		PingPong,
		Loop
	};
	public enum PlayOn
	{
		Manual,
		OnAwake,
		OnEnable
	};
	
	// Some playback settings
	public Engine.WorldSpaceType simulationSpace;
	public bool useRectTransform;
	
	public TweenPlayType playType;
	public float duration = 1;
	public float waitPeriod = 0;
	public bool randomizeTime;
	public PlayOn playOn;
	
	// This is what it says.
	public AnimationCurve tweenCurve = Curves.CreateLinearCurve();
	
	// Just what it says - some little customization for the gizmos, just in case the simulation is really tiny or too big
	public bool DrawGizmos = true;
	public float GizmoSize = 0.1f;
	
	
	private float time = 0.0f;
	private bool timeFlip;
	
	public bool playing;
	[HideInInspector]
	public float curveTime = 0.0f;
	
	
	void Awake()
	{
		if (playOn == PlayOn.OnAwake)
		{
			ResetPlaybackStats();
			SetCurveTime();
			//Tweening();
			ApplyTweenTransform();
			
			Invoke("PlayBegin", waitPeriod);
		}
	}
	void OnEnable()
	{		
		if (playOn == PlayOn.OnEnable)
		{
			ResetPlaybackStats();
			SetCurveTime();
			//Tweening();
			ApplyTweenTransform();
			
			Invoke("PlayBegin", waitPeriod);
		}
	}
	
	public virtual void TweenAwake()
	{
		
	}
	
	void Update()
	{
		if (playing)
		{
			// Time
			time += Time.deltaTime/duration;
			
			SetCurveTime();
			
			// Looping, ending, etc
			if (time >= 1)
			{
				if (playType == TweenPlayType.Once)
				{
					Stop();
				}
				if (playType == TweenPlayType.Loop)
				{
					time -= 1; // Just reset the timer!
				}
				if (playType == TweenPlayType.PingPong)
				{
					time -= 1; // Reset the timer!
					FlipPlayback(); // Now lets flip the curve!
				}
			}
			
			// Execute tweening!
			Tweening();
		}
	}
	
	void LateUpdate()
	{
		if (playing)
		{
			ApplyTweenTransform();
		}
	}
	
	void SetCurveTime()
	{
		if (timeFlip)
		{
			curveTime = (1-time);
		}
		else
		{
			curveTime = time;
		}
	}
	
	public virtual void Tweening()
	{
		
	}
	public virtual void ApplyTweenTransform()
	{
		
	}
	
	// Playback
	public void FlipPlayback()
	{
		timeFlip = !timeFlip;
	}
	public void SetNormalPlayback()
	{
		timeFlip = false;
	}
	public void SetReversePlayback()
	{
		timeFlip = true;
	}
	public void PlayBegin()
	{
		if (randomizeTime)
		{
			time = Random.value;
		}
		
		ResetPlaybackStats();
		playing = true;
	}
	public void Play()
	{
		ResetPlaybackStats();
		playing = true;
	}
	public void Stop()
	{
		playing = false;
		timeFlip = false;
		ResetPlaybackStats();
	}
	public void Resume()
	{
		playing = true;
	}
	public void Pause()
	{
		playing = false;
	}
	
	void ResetPlaybackStats()
	{
		time = 0.0f;
		curveTime = 0.0f;
	}
	
	// Gizmos
	public void DrawPositionTweenGizmo(Vector3 PosStart, Vector3 PosEnd)
	{
		if (!DrawGizmos)
			return;
		
		Vector3 GizmoPosStart;
		Vector3 GizmoPosEnd;
		
		if (simulationSpace == Engine.WorldSpaceType.Global)
		{
			GizmoPosStart = PosStart;
			GizmoPosEnd = PosEnd;
		}
		else
		{
			GizmoPosStart = transform.parent.position + transform.parent.rotation * PosStart;
			GizmoPosEnd = transform.parent.position + transform.parent.rotation * PosEnd;
		}
		
		// Start
		Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GizmoPosStart, GizmoSize);
		
		// End
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(GizmoPosEnd, GizmoSize);
		
		Gizmos.DrawLine(GizmoPosStart, GizmoPosEnd); // Line between the points
	}
	public void DrawRotationTweenGizmo()
	{
		if (!DrawGizmos)
			return;
	}
	public void DrawScaleTweenGizmo(Vector3 PosStart, Vector3 PosEnd, Vector3 ScStart, Vector3 ScEnd)
	{
		if (!DrawGizmos)
			return;
		
		// Start
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(PosStart, ScStart);
		
		// End
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(PosEnd, ScEnd);
	}
}
