using DG.Tweening;
using GrislyTools;
using System.Collections;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
	#region Singleton
	public static GlobalSettings Instance
	{
		get
		{
			return _instance;
		}
	}
	private static GlobalSettings _instance;
	#endregion

	public static SoundController SoundController => Instance._soundController;
	public static SceneLoader SceneLoader => Instance._sceneLoader;

	[SerializeField] private SoundController _soundController;
	[SerializeField] private SceneLoader _sceneLoader;
	[SerializeField] private AudioClip _musicClip;
	[SerializeField] private float _volume;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}

		_instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		Application.targetFrameRate = 60;
		DOTween.defaultAutoKill = true;

		StartCoroutine(InitAsync());
	}
	private IEnumerator InitAsync()
	{
		if (!PlayerPrefs.HasKey("Loaded"))
		{
			// settings
			yield return new WaitUntil(() => DataManager.Data.GetValue("Music", out bool m));
			yield return new WaitUntil(() => DataManager.Data.GetValue("Sound", out bool s));
			yield return new WaitUntil(() => DataManager.Data.GetValue("Taptic", out bool t));

			DataManager.Data.Save();
			PlayerPrefs.SetString("Loaded", "true");
		}

		_soundController.Initialize(10);
		_soundController.PlayMusic(_musicClip, _volume);

		_sceneLoader.LoadScene("Lobby");
		_sceneLoader.AllowSceneActivation();
	}
}
