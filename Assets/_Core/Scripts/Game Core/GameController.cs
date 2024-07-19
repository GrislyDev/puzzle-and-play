using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private PuzzleModel _puzzleModel;
	[SerializeField] private PuzzleTimer _timer;
	[SerializeField] private PopupsGroup _popupsGroup;

	private float _animDuration = 3;
	private float _scale = 1f;

	private void Awake()
	{
		_timer.OnTimerEnd += OnTimerEndHandler;
		_puzzleModel.OnPuzzleSolved += OnPuzzleSolvedHandler;
	}

	private void Start()
	{
		StartCoroutine(StartGame());
	}
	private void OnTimerEndHandler()
	{
		// do lose popup
		_popupsGroup.LostPopup.StartPopup(_animDuration, _scale);
	}
	private void OnPuzzleSolvedHandler()
	{
		// do win popup
		_timer.IsTimerActive = false;
		_popupsGroup.WonPopup.StartPopup(_animDuration, _scale);

	}
	private void OnDestroy()
	{
		_timer.OnTimerEnd -= OnTimerEndHandler;
		_puzzleModel.OnPuzzleSolved -= OnPuzzleSolvedHandler;
	}
	private IEnumerator StartGame()
	{
		_timer.IsTimerActive = false;
		var loadingDelay = new WaitForSeconds(1f);

		yield return new WaitForEndOfFrame();
		yield return loadingDelay;

		_timer.IsTimerActive = true;
	}
}
