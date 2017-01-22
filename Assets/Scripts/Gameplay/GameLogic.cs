using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	internal bool gameOver;
	internal int score;

	void Awake()
	{
		The.gameLogic = this;
	}

	public void AddScore(int amount)
	{
		score += amount;
		The.gameUI.UpdateScoreGraphics();
	}

	public void GameOver()
	{
		The.gameUI.GameOver();
		gameOver = true;
	}
}
