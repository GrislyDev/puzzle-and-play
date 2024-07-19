using UnityEngine;
using UnityEngine.UI;

public class OpenPopupWindow : MonoBehaviour
{
	[SerializeField] private Transform _transform;
	[SerializeField] private Button _openButton;
	[SerializeField] private Button _closeButton;
	[SerializeField] private PuzzleTimer _timer;

	private void Awake()
	{
		_openButton.onClick.AddListener(OpenButtonHandler);
		_closeButton.onClick.AddListener(CloseButtonHandler);
		gameObject.SetActive(false);
	}

	private void OpenButtonHandler()
	{
		if (_timer != null)
			_timer.IsTimerActive = false;

		Time.timeScale = 0;
		_transform.gameObject.SetActive(true);
	}
	private void CloseButtonHandler()
	{
		if(_timer != null)
			_timer.IsTimerActive = true;

		Time.timeScale = 1;
		_transform.gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		Time.timeScale = 1;
		_openButton.onClick.RemoveAllListeners();
		_closeButton.onClick.RemoveAllListeners();
	}
}
