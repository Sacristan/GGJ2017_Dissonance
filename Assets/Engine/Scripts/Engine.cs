using UnityEngine;

public static class Engine
{
	// General stuff
	public static bool showFPS;

	// Enums
	public enum ToggleType
	{
		Activate,
		Deactivate,
		Toggle,
	}
	public enum WorldSpaceType
	{
		Local,
		Global,
	}
	public enum Direction
	{
		Up,
		Down,
		Left,
		Right,
		In,
		Out,
		Forward,
		Backward,
	};
	public enum ScaleType
	{
		Stretch,
		Fit,
		Fill,
		Tile
	}
	public enum UpdateType
	{
		OnAwake,
		OnUpdate,
		OnScreenChange
	}

	// Cursor
	public static void HideCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	public static void UnhideCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	// Math
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360) { angle += 360; }
		if (angle > 360) { angle -= 360; }
		return Mathf.Clamp(angle, min, max);
	}

	// Screen
	public static int lastScreenWidth = 1;
	public static int lastScreenHeight = 1;
	public static ScreenOrientation lastScreenOrientation;
	public static bool screenSizeChanged = true;

	public static float GetWorldScreenHeight()
	{
		return (float)(Camera.main.orthographicSize * 2.0f);
	}
	public static float GetWorldScreenWidth()
	{
		return GetWorldScreenHeight() / Screen.height * Screen.width;
	}
	public static bool ScreenSizeChanged()
	{
		bool changed = false;

		changed = lastScreenWidth != Screen.width || lastScreenHeight != Screen.height || lastScreenOrientation != Screen.orientation;
		UpdateLastScreenSize();

		return changed;
	}
	public static void UpdateLastScreenSize()
	{
		lastScreenWidth = Screen.width;
		lastScreenHeight = Screen.height;
		lastScreenOrientation = Screen.orientation;
	}
}
