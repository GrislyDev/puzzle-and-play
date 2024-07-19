using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class LoadingBar : MonoBehaviour
{
	[SerializeField] private Image _fillImage;
	[SerializeField] private TextMeshProUGUI _text;
	[SerializeField] private Slider _slider;

	private Tween _tween;
	private Vector3 _endPos = new Vector3(0, 0, -360F);
	private float _animDuration = 1f;

	private void Start()
	{
		_slider.onValueChanged.AddListener(OnSliderValueChanged);
	}
	private void OnSliderValueChanged(float value)
	{
		_text.text = Mathf.RoundToInt(value).ToString() + "%";

		if (value >= 99)
			_tween?.Kill();
	}
	private void OnDestroy()
	{
		KillAnim();
		_slider.onValueChanged?.RemoveListener(OnSliderValueChanged);
	}
	public void StartLoadingAnim()
	{
		_tween?.Kill();
		_tween = _fillImage.transform.DORotate(_endPos, _animDuration, RotateMode.FastBeyond360)
			.SetLoops(-1, LoopType.Restart)
			.SetEase(Ease.Flash);
	}
	public void KillAnim()
	{
		_tween?.Kill();
	}
}
