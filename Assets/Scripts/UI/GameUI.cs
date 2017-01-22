using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[Header("Hud")]
	public Text healthText;
	public Text ammoText;
	public Text scoreText;

	[Header("Screens")]
	public GameObject gameOverPage;

	void Awake()
	{
		The.gameUI = this;
	}

	void Start()
	{
		UpdateHealthGraphics();
		UpdateAmmoGraphics();
		UpdateScoreGraphics();
	}

	public void UpdateHealthGraphics()
	{
		healthText.text = "Health: " + Mathf.Ceil(The.player.health);
	}
	public void UpdateAmmoGraphics()
	{
		ammoText.text = "Ammo: " + The.player.currentAmmo;
	}
	public void UpdateScoreGraphics()
	{
		scoreText.text = "Score: " + The.gameLogic.score;
	}

	public void GameOver()
	{
		gameOverPage.SetActive(true);
	}
}
