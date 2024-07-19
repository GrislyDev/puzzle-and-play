using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using GrislyTools;


public class SceneLoader : MonoBehaviour
{
	[SerializeField] private Canvas _mainCanvas;
	[SerializeField] private Camera _camera;
	[SerializeField] private Slider _loadSlider;
	[SerializeField] private LoadingBar _loadingBar;
	[SerializeField] private float _minLoadDuration;
	[SerializeField] private float _maxLoadDuration;

	private Tween _tween;
	private Coroutine _coroutine;
	private AsyncOperation _loadingOperation;
	private bool _loading;

	public void LoadScene(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			Debug.LogError("Scene name is null or empty");
			return;
		}

		if (_loading)
			return;

		_loading = true;

		DataManager.Data.SetValue("CurrentScene", sceneName);

		_tween?.Kill();
		if (_coroutine != null)
		{
			StopCoroutine(_coroutine);
			_coroutine = null;
		}

		_loadSlider.value = 0;

		// Mute music when loading
		GlobalSettings.SoundController.PauseMusic();

		_loadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		_loadingOperation.allowSceneActivation = false;

		_mainCanvas.gameObject.SetActive(true);
		_camera.gameObject.SetActive(true);

		float loadDuration = Random.Range(_minLoadDuration, _maxLoadDuration);

		_loadingBar.StartLoadingAnim();

		_coroutine = StartCoroutine(LoadSceneRoutine());
		_tween = _loadSlider.DOValue(_loadSlider.maxValue, loadDuration).OnComplete(() =>
		{
			_loadingBar.KillAnim();
		});
	}

	private IEnumerator LoadSceneRoutine()
	{
		while (!_loadingOperation.isDone)
		{
			yield return null;
		}

		_camera.gameObject.SetActive(false);
		_mainCanvas.gameObject.SetActive(false);

		// Unmute music if possible
		GlobalSettings.SoundController.UnpauseMusic();

		_loading = false;
	}

	public void UnloadScene(string sceneName)
	{
		if (SceneManager.GetSceneByName(sceneName).isLoaded)
		{
			_camera.gameObject.SetActive(true);
			SceneManager.UnloadSceneAsync(sceneName);
		}
	}

	public void ReloadScene()
	{
		DataManager.Data.GetValue("CurrentScene", out string scene);
		StartCoroutine(UnloadAndReloadScene(scene));
	}

	public void AllowSceneActivation()
	{
		StartCoroutine(WaitForSliderAndActivateScene());
	}

	private IEnumerator WaitForSliderAndActivateScene()
	{
		// Wait until the slider reaches its maximum value
		while (_loadSlider.value != _loadSlider.maxValue)
		{
			yield return null;
		}

		// Allow scene activation
		_loadingOperation.allowSceneActivation = true;
	}

	private IEnumerator UnloadAndReloadScene(string sceneName)
	{
		if (SceneManager.GetSceneByName(sceneName).isLoaded)
		{
			AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneName);

			while (!unloadOperation.isDone)
			{
				yield return null;
			}

			AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			_loadingOperation = loadOperation;
			_loadingOperation.allowSceneActivation = false;

			StartCoroutine(LoadSceneRoutine());
			AllowSceneActivation();
		}
	}

	private void OnDestroy()
	{
		_tween?.Kill();
	}
}
