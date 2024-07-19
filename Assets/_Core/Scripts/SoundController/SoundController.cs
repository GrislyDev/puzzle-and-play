using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioListener))]
public class SoundController : MonoBehaviour
{
	private AudioSource _backgroundSource;
	private List<AudioSource> _SFX_Sources;

	public void Initialize(int sourceCount)
	{
		_SFX_Sources = new List<AudioSource>();

		for (int i = 0; i < sourceCount; i++)
		{
			_SFX_Sources.Add(gameObject.AddComponent<AudioSource>());
		}

		_backgroundSource = gameObject.AddComponent<AudioSource>();
	}

	public void PlayMusic(AudioClip music, float volume)
	{
		_backgroundSource.clip = music;
		_backgroundSource.volume = volume;
		_backgroundSource.loop = true;
		_backgroundSource.Play();
	}
	public void StopMusic() => _backgroundSource.Stop();
	public void PauseMusic() => _backgroundSource.Pause();
	public void UnpauseMusic() => _backgroundSource.UnPause();
	public void MuteMusic() => _backgroundSource.mute = true;
	public void UnmuteMusic() => _backgroundSource.mute = false;
	public void PlaySFX(AudioClip sfx, float volume, bool taptic = false)
	{
		var source = _SFX_Sources.FirstOrDefault(source => !source.isPlaying);

		if (source == null)
			Debug.Log("No available AudioSource to play SFX");

		source.clip = sfx;
		source.volume = volume;
		source.Play();

		if (taptic)
			Taptic.Light();
	}
	public void MuteAllSFXSources()
	{
		foreach (var source in _SFX_Sources)
		{
			source.mute = true;
		}
	}
	public void UnmuteAllSFXSources()
	{
		foreach (var source in _SFX_Sources)
		{
			source.mute = false;
		}
	}
}