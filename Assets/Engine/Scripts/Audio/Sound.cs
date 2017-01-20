using UnityEngine;
using System.Collections;

public static class Sound
{
	// Sound effects
	public static GameSound PlayClip(AudioClip clip, float vol = 1)
	{
		return PlayClipAt(clip, Vector3.zero, vol, 0);
	}
	public static GameSound PlayClip(AudioClip[] clip, float vol = 1)
	{
		var randomClip = clip[Random.Range(0, clip.Length)];
		return PlayClipAt(randomClip, Vector3.zero, vol, 0);
	}
	public static GameSound PlayClipAt(AudioClip clip, Vector3 pos, float vol = 1, float spacBl = 1, bool useTimeScale = true)
	{
		GameObject tempGO = null;
		AudioSource aSource = null;
		GameSound newGameSound = null;

		if (clip != null)
		{
			tempGO = new GameObject("TempAudio");

			tempGO.transform.position = pos;
			aSource = tempGO.AddComponent<AudioSource>();
			aSource.clip = clip;
			aSource.volume = vol * Settings.soundVolume;
			//aSource.volume = Settings.soundVolume;
			aSource.spatialBlend = spacBl;

			aSource.Play();

			newGameSound = tempGO.AddComponent<GameSound>();
			newGameSound.source = aSource;
			newGameSound.volume = vol;
			//newGameSound.useTimeScale = useTimeScale;

			//GameObject.Destroy(tempGO, clip.length);
		}
		else
		{
			Debug.Log("Audio clip was not played, because it does not exist!");
		}

		return newGameSound;
	}
	public static GameSound PlayClipAt(AudioClip[] clip, Vector3 pos, float vol = 1, float spacBl = 1)
	{
		var randomClip = clip[Random.Range(0, clip.Length)];
		return PlayClipAt(randomClip, pos, vol, spacBl);
	}

	// Music
	public static void StopAllMusic()
	{
		if (GameMusic.current != null)
		{
			GameMusic.current.Stop();
			GameMusic.current = null;
		}
	}
	public static void StopAllMusicFade(float duration = 1)
	{
		if (GameMusic.current != null)
		{
			GameMusic.current.FadeOut(duration);
			GameMusic.current = null;
		}
	}
	public static GameMusic PlayTrackFade(AudioClip track, float duration = 1, bool loop = true)
	{
		if (GameMusic.current != null)
		{
			GameMusic.current.FadeOut(duration);
		}

		var o = PlayTrack(track, loop);
		o.FadeIn(duration);

		return o;
	}
	public static GameMusic PlayTrack(AudioClip track, bool loop = true)
	{
		GameObject musicGO = null;
		AudioSource aSource = null;
		GameMusic newGameMusic = null;

		if (track != null)
		{
			if (GameMusic.current != null)
			{
				GameMusic.current.Stop();
			}

			musicGO = new GameObject("MusicTrack");
			aSource = musicGO.AddComponent<AudioSource>();
			aSource.clip = track;
			aSource.loop = loop;
			aSource.spatialBlend = 0;
			aSource.reverbZoneMix = 0;
			aSource.Play();
			 
			newGameMusic = musicGO.AddComponent<GameMusic>();
			newGameMusic.source = aSource;
			newGameMusic.SetVolume(1);

			// Don't destroy 
			Object.DontDestroyOnLoad(musicGO);

			// Destroy after time, if not looping
			if (!loop)
			{
				GameObject.Destroy(musicGO, track.length);
			}
		}
		else
		{
			Debug.LogError("Trying to play a non-existant music track");
		}

		return newGameMusic;
	}
}
