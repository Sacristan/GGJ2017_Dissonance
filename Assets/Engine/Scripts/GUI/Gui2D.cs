using UnityEngine;
using System.Collections;

public static class Gui2D
{
	public static Texture2D pixelTex;

	public static void MakePixelTex()
	{
		pixelTex = new Texture2D(1, 1);
		pixelTex.SetPixel(0, 0, new Color(1, 1, 1, 1));
		pixelTex.Apply();
	}
	public static void DrawRect(Rect rect, Color color)
	{
		if (!pixelTex)
		{
			MakePixelTex();
			return;
		}

		GUI.color = color;
		GUI.DrawTexture(rect, pixelTex);
		GUI.color = new Color(1, 1, 1, 1);
	}

	public static float MouseY()
	{
		return Screen.height - Input.mousePosition.y;
	}
	public static Vector2 MousePosition()
	{
		float x = Input.mousePosition.x;
		float y = Screen.height - Input.mousePosition.y;
		return new Vector2(x, y);
	}
}
