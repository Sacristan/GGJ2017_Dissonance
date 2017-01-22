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

	[Header("Other")]
	public Image fadeImage;

	void Awake()
	{
		The.gameUI = this;
	}

	void Start()
	{
		UpdateHealthGraphics();
		UpdateAmmoGraphics();
		UpdateScoreGraphics();

		StartCoroutine(FadeIn());
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

	public IEnumerator FadeIn()
	{
		fadeImage.gameObject.SetActive(true);
		float t = 0;

		while (true)
		{
			t += 0.5f * Time.deltaTime;

			fadeImage.color = Color.Lerp(Color.black, new Color(0,0,0,0), t);

			if (t >= 1)
			{
				fadeImage.gameObject.SetActive(false);
				break;
			}

			yield return null;
		}
	}
}
