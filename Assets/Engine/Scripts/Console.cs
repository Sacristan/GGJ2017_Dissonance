using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour
{
	public static string version = "2.0a";

	public static bool gameHasStarted;
	public static bool isEnabled;

	public Font font;

	// Graphics
	public static GUIStyle conGui;
	private float textSize = 16;
	private float height = 160;
	public static float backgroundTransparency = 0.9f;

	// some colors and preset words	
	//public string colGood = "<color=green>";
	public static string colorBad = "<color=red><b>";
	public static string colorVariable = "<color=brown><b>";
	public static string endFormat = "</b></color>";

	public static string wordEnabled = "<color=green><b>enabled</b></color>";
	public static string wordDisabled = "<color=red><b>disabled</b></color>";

	// Input
	public static string inputTxt = "";
	private bool typeLineVisible = true;

	// Console controls
	private Vector2 preClickPos;
	private Vector2 preClickDiff;
	private bool resizing;
	private bool scrolling;

	// Fps
	private float deltaTime;
	private float fps;

	// History
	public static List<string> logHistory;
	internal string tempConsoleOutput;
	public static Console instance;

	void Awake()
	{
		// Check if the console already exists
		instance = this;

		// Do some startup stuff
		if (!gameHasStarted)
		{
			gameHasStarted = true;

			// Setup the GUIStyle
			conGui = new GUIStyle();
			conGui.font = font;
			conGui.normal.textColor = Color.white;

			// Setup history lists
			logHistory = new List<string>();

			// Draw some startup ascii art for fun
			DrawStartupASCIIArt();

			// Limit fps to 60
			Application.targetFrameRate = 60;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			isEnabled = !isEnabled;

			if (isEnabled)
			{
				StartCoroutine(BlinkTypeLine());
			}
		}

		if (isEnabled)
		{
			foreach (char c in Input.inputString)
			{
				// Backspace - Remove the last character
				if (c == "\b"[0])
				{
					if (inputTxt.Length != 0)
					{
						inputTxt = inputTxt.Substring(0, inputTxt.Length - 1);
					}
				}
				else if (c == "\n"[0] || c == "\r"[0]) // "\n" for Mac, "\r" for windows.
				{
					ExecuteCommand(inputTxt);
				}
				else if (c != "`"[0]) // Write text
				{
					inputTxt += c;
				}
			}

			// Some control stuff
			float scrollAmount = Input.GetAxis("Mouse ScrollWheel");

			// Text resizing
			if (Input.GetKey(KeyCode.LeftControl))
			{
				textSize += scrollAmount * 8;

				if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
				{
					textSize = 16;
				}

				textSize = Mathf.Clamp(textSize, 8, 64);

				conGui.fontSize = (int)textSize;
			}

			// Resizing with mouse
			if (Input.GetMouseButtonDown(0))
			{
				preClickPos = Gui2D.MousePosition();

				// Resizing
				if (!resizing && preClickPos.y > height - textSize && preClickPos.y < height)
				{
					resizing = true;
					preClickDiff = new Vector2(0, height - preClickPos.y);
				}

				// Scrolling

				// Closing
				if (preClickPos.x > Screen.width - textSize && preClickPos.y < textSize)
				{
					Disable();
				}
			}
			if (Input.GetMouseButtonUp(0))
			{
				if (resizing)
				{
					resizing = false;
				}
			}
			if (resizing)
			{
				height = Gui2D.MouseY() + preClickDiff.y;
			}

			// Resizing with keyboard
			if (Input.GetKeyDown(KeyCode.PageDown))
			{
				height += textSize;
			}
			if (Input.GetKeyDown(KeyCode.PageUp))
			{
				height -= textSize;
			}

			float oldHeight = height;
			height = Mathf.Round(oldHeight / textSize) * textSize;

			height = Mathf.Clamp(height, textSize * 2, Screen.height);
		}

		// Calculate FPS
		if (Engine.showFPS)
		{
			deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
			fps = 1.0f / deltaTime;
		}

		// Screenshots
		if (Input.GetKeyDown(KeyCode.F8))
		{
			int shotsTaken = PlayerPrefs.GetInt("ShotsTaken", 0);
			shotsTaken++;
			PlayerPrefs.SetInt("ShotsTaken", shotsTaken);
			string filename = Application.persistentDataPath + "/ScreenShots/Shot_" + shotsTaken + ".png";

			ScreenCapture.CaptureScreenshot(filename);
			Log("Screenshot " + shotsTaken + " saved");
		}
	}

	IEnumerator BlinkTypeLine()
	{
		float t = 0;

		while (isEnabled)
		{
			t += 4 * Time.unscaledDeltaTime;

			if (t >= 1)
			{
				t = 0;
				typeLineVisible = !typeLineVisible;
			}

			yield return new WaitForEndOfFrame();
		}
	}

	public void Enable()
	{
		isEnabled = true;
	}
	public void Disable()
	{
		isEnabled = false;
	}

	void ClearConsoleHistory()
	{
		logHistory.Clear();
	}

	public virtual void OnGUI()
	{
		if (isEnabled)
		{
			// We want this to be on top as much as possible!
			GUI.depth = -99999999;

			// Necessary rects
			Rect inputFieldRect = new Rect(0, height - textSize, Screen.width, textSize);
			Rect closeButtonRect = new Rect(Screen.width - textSize, 0, textSize, textSize);

			// Background
			Gui2D.DrawRect(new Rect(0, 0, Screen.width, height), new Color(0, 0, 0, backgroundTransparency));

			// History
			conGui.alignment = TextAnchor.UpperLeft;
			int historyCount = logHistory.Count;
			for (int i = 0; i < historyCount; i++)
			{
				GUI.Label(new Rect(0, height - textSize - historyCount * textSize + i * textSize, Screen.width, textSize), logHistory[i], conGui);
			}

			// Grabbing for resizing
			if (resizing)
			{
				Gui2D.DrawRect(inputFieldRect, new Color(1, 1, 1, 0.5f));
			}

			// Input field
			string typeLine = "";
			if (typeLineVisible)
			{
				typeLine = "_";
			}
			GUI.Label(inputFieldRect, "> " + inputTxt + typeLine, conGui);

			// Close button
			conGui.alignment = TextAnchor.MiddleCenter;
			Gui2D.DrawRect(closeButtonRect, new Color(0.75f, 0, 0, 1));
			GUI.Label(closeButtonRect, "X", conGui);
		}

		if (Engine.showFPS)
		{
			float fpsY = 0;
			if (isEnabled)
			{
				fpsY = height;
			}

			Rect fpsRect = new Rect(0, fpsY, 20, textSize);

			Gui2D.DrawRect(fpsRect, new Color(0, 0, 0, backgroundTransparency));
			conGui.alignment = TextAnchor.UpperLeft;
			GUI.Label(fpsRect, Mathf.Ceil(fps).ToString(), conGui);
		}
	}

	public static void Log(string txtLog)
	{
		logHistory.Add(txtLog);
	}
	public static void Log(int intLog)
	{
		Log(intLog.ToString());
	}
	public static void Log(float floatLog)
	{
		Log(floatLog.ToString());
	}

	//======================================================
	// Executing console commands
	void ExecuteCommand(string txt)
	{
		if (txt == "")
		{
			return;
		}

		// Check if our command does something
		CheckForCommands(txt);
		string consoleOutput = FormatCommandOutput(txt, tempConsoleOutput);
		if (consoleOutput != "")
		{
			Log(consoleOutput);
		}

		tempConsoleOutput = "";
		inputTxt = "";
	}
	string FormatCommandOutput(string currentInput, string commandOutput)
	{
		string returnableString = "";

		if (commandOutput == "")
		{
			returnableString = colorBad + "'" + currentInput + "'" + " is not recognized as an internal or external command" + endFormat;
		}
		else if (commandOutput != "null")
		{
			returnableString = "<color=#FFFFFF32>" + currentInput + "</color> - " + commandOutput;
		}

		return returnableString;
	}
	public virtual void CheckForCommands(string txt)
	{
		// Available commands (This can be extended)
		if (txt == "cls")
		{
			ClearConsoleHistory();
			tempConsoleOutput = "null";
		}
		else if (txt == "/quit" || txt == "quit" || txt == "exit")
		{
			tempConsoleOutput = "Quitting the game...";
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
			Application.OpenURL(webplayerQuitURL);
#else
			Application.Quit();
#endif
		}
		else if (txt == "toggle fps" || txt == "fps")
		{
			Engine.showFPS = !Engine.showFPS;
			if (Engine.showFPS)
			{
				tempConsoleOutput = "FPS counter " + wordEnabled;
			}
			else
			{
				tempConsoleOutput = "FPS counter " + wordDisabled;
			}
		}
		else if (txt == "showfps 0" || txt == "fps 0" || txt == "hidefps")
		{
			Engine.showFPS = false;
			tempConsoleOutput = "FPS counter " + wordDisabled;
		}
		else if (txt == "showfps 1" || txt == "fps 1" || txt == "showfps")
		{
			Engine.showFPS = true;
			tempConsoleOutput = "FPS counter " + wordEnabled;
		}
		else if (txt.StartsWith("max fps "))
		{
			string newTxt = txt.Remove(0, 8);
			Application.targetFrameRate = int.Parse(newTxt);
			tempConsoleOutput = "Max FPS set to " + newTxt;
		}
		else if (txt == "reset" || txt == "restart")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			tempConsoleOutput = "Map " + colorVariable + SceneManager.GetActiveScene().name + endFormat + " restarted";
			Time.timeScale = 1;
		}
		else if (txt == "list scenes" || txt == "list all scenes")
		{
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Log(SceneManager.GetSceneAt(i).name);
			}
			tempConsoleOutput = "All scenes listed";
		}
		else if (txt.StartsWith("time scale ") || txt.StartsWith("time speed "))
		{
			string newTimeScaleString = txt.Remove(0, 11);

			Time.timeScale = float.Parse(newTimeScaleString);
			tempConsoleOutput = "Time scale set to " + colorVariable + newTimeScaleString + endFormat;
		}
		else if (txt == "title")
		{
			DrawStartupASCIIArt();
			tempConsoleOutput = "null";
		}

		// Console customization
		else if (txt.StartsWith("background alpha "))
		{
			string newTxt = txt.Remove(0, 17);
			backgroundTransparency = float.Parse(newTxt);
			tempConsoleOutput = "null";
		}
	}

	public string TextFromBool(bool sb)
	{
		if (sb)
		{
			return wordEnabled;
		}
		else
		{
			return wordDisabled;
		}
	}

	//======================================================
	// Fun stuff
	public virtual void DrawStartupASCIIArt()
	{
		Log("Clover Console " + "V." + version);
		Log(colorBad + "2016" + endFormat);

		Log("");
	}
}
