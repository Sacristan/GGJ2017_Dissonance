using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	public AudioClip track;

	internal bool gameOver;
	internal int score;

	void Awake()
	{
		The.gameLogic = this;
		StartCoroutine(MusicDelay());
	}

	public void AddScore(int amount)
	{
		score += amount;
		The.gameUI.UpdateScoreGraphics();
	}

	public void GameOver()
	{
		Cursor.lockState = CursorLockMode.None;
		The.gameUI.GameOver();
		gameOver = true;
	}

	IEnumerator MusicDelay()
	{
		yield return new WaitForSeconds(3);
		Sound.PlayTrack(track, false);
	}
}
