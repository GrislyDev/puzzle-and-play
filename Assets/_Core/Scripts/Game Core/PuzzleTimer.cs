using TMPro;
using UnityEngine;

public class PuzzleTimer : MonoBehaviour
{
	public bool IsTimerActive = true;

	public event System.Action OnTimerEnd;
	public event System.Action OnNearToEnd;

	[SerializeField] private TextMeshProUGUI _totalTimeText;
	[SerializeField] private TextMeshProUGUI _remainingTimeText;
	[SerializeField] private int _totalTime;

	private float _timer;
	private float _nearToEndTime = 15;

	private void Awake()
	{
		_totalTimeText.text = $"{_totalTime.ToString()}c";
		_remainingTimeText.text = _totalTime.ToString();
	}
	private void Update()
	{
		DecreaseTime();
	}
	private void DecreaseTime()
	{
		if (IsTimerActive)
		{
			_timer += Time.deltaTime;

			if (_timer >= 1)
			{
				_totalTime -= (int)_timer;
				_remainingTimeText.text = _totalTime.ToString();
				_timer = 0;
			}

			if (_totalTime == _nearToEndTime && _timer == 0)
			{
				OnNearToEnd?.Invoke();
			}

			if (_totalTime <= 0)
			{
				OnTimerEnd?.Invoke();
				IsTimerActive = false;
			}
		}
	}
}
