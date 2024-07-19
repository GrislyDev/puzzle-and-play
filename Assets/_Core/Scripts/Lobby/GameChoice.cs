using DG.Tweening;
using GrislyTools;
using UnityEngine;
using UnityEngine.UI;

public class GameChoice : MonoBehaviour
{
	[SerializeField] private Button _prevButton;
	[SerializeField] private Button _nextButton;
	[SerializeField] private Button _playButton;
	[SerializeField] private Transform _gamesTransform;
	[SerializeField] float _transformOffset;

	private const float FIRST_GAME = 0;
	private const float LAST_GAME = 2;
	private string[] _games = {"15Puzzle3", "15Puzzle4", "15Puzzle5" };

	private Tween _tween;
	private int _currentGame = 0;
	private float _duration = 1f;

	private void Start()
	{ 
		_prevButton.onClick.AddListener(OnPrevClicked);
		_nextButton.onClick.AddListener(OnNextClicked);
		_playButton.onClick.AddListener(OnPlayClicked);
	}

	private void OnPrevClicked()
	{
		if (_currentGame == FIRST_GAME)
			return;

		_currentGame--;

		_tween.Kill();
		float pos = _gamesTransform.localPosition.x + _transformOffset;
		_tween = _gamesTransform.DOLocalMoveX(pos, _duration)
			.OnStart(() => SetButtonsInteractable(false))
			.OnComplete(() => SetButtonsInteractable(true));
	}
	private void OnNextClicked()
	{
		if (_currentGame == LAST_GAME)
			return;

		_currentGame++;

		_tween?.Kill();
		float pos = _gamesTransform.localPosition.x - _transformOffset;
		_tween = _gamesTransform.DOLocalMoveX(pos, _duration)
			.OnStart(() => SetButtonsInteractable(false))
			.OnComplete(() => SetButtonsInteractable(true));
		}
	private void SetButtonsInteractable(bool enable)
	{
		_nextButton.interactable = enable;
		_prevButton.interactable = enable;
		_playButton.interactable = enable;
	}
	private void OnPlayClicked()
	{
		DataManager.Data.GetValue("CurrentScene", out string scene);
		GlobalSettings.SceneLoader.UnloadScene(scene);
		GlobalSettings.SceneLoader.LoadScene(_games[_currentGame]);
		GlobalSettings.SceneLoader.AllowSceneActivation();
	}
	private void OnDestroy()
	{
		_prevButton.onClick.RemoveAllListeners();
		_nextButton.onClick.RemoveAllListeners();
		_playButton.onClick.RemoveAllListeners();
	}
}
